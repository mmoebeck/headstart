using Flurl.Http;
using Flurl.Http.Configuration;
using ordercloud.integrations.contenthub.Models;
using Stylelabs.M.Sdk.Contracts.Base;
using Stylelabs.M.Sdk.WebClient;
using Stylelabs.M.Sdk.WebClient.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ordercloud.integrations.contenthub
{
    public interface IContentHubService
    {
        Task<string> UploadAsset(string filename, string filepath, string base64file, string productID);
    }
    
    public class ContentHubService: IContentHubService
    {
        private readonly IFlurlClient _flurl;
        private readonly ContentHubConfig _config;

        public ContentHubService(ContentHubConfig config, IFlurlClientFactory flurlFactory)
        {
            _config = config;
            _flurl = flurlFactory.Get($"{_config.BaseUrl}" + "/api");
        }

        private IFlurlRequest Request(string resource, string token)
        {
            return _flurl.Request($"{resource}").WithHeader("X-Auth-Token", token);
        }

        private async Task<long> CreateAsset(IWebMClient MClient, string productID)
        {
            IEntity asset = await MClient.EntityFactory.CreateAsync("M.Asset");
            asset.SetPropertyValue("Title", productID);
            long id = await MClient.Entities.SaveAsync(asset);
            return id;
        }

        private async Task UploadImageAsync(string filename, string base64file, long assetID)
        {
            // Load Image - This is for demo.  Real String will come from OC Upload
            byte[] imageArray = Convert.FromBase64String(base64file);

            // Request URL
            var uploadRequest = await this.Request("v1.0/upload", _config.Token)
                .PostMultipartAsync(mp => mp
                    .AddString("fileName", "Tulip.jpg")
                    .AddString("fileSize", imageArray.Length.ToString())
                );

            var responseBody = uploadRequest.GetJsonAsync<RequestUploadResponse>();
            string azureURL = uploadRequest.Headers.FirstOrDefault("Location");

            // Load to Azure
            if (!String.IsNullOrEmpty(azureURL))
            {
                ByteArrayContent fileContent = new ByteArrayContent(imageArray);
                var uploadFile = await azureURL.WithHeaders(new { x_ms_blob_type = "BlockBlob", x_ms_blob_content_type = "image/jpeg", x_ms_meta_Filename = filename })
                    .PutAsync(fileContent);

                // Assign to Asset
                await this.Request("v1.0/upload/assets/" + assetID.ToString(), _config.Token)
                    .PostJsonAsync(responseBody.Result);
            }

        }

        private async Task<string> CreatePublicLink(long assetID)
        {
            Guid g = Guid.NewGuid();
            var parents = new List<Parent>() { };
            var parent = new Parent()
            {
                href = _config.BaseUrl + "/api/entities/" + assetID.ToString()
            };
            parents.Add(parent);

            PublicLinkRequest request = new PublicLinkRequest()
            {
                properties = new PublicLinkProperties()
                {
                    RelativeUrl = g.ToString().Replace("-", ""),
                    Resource = "downloadOriginal"
                },
                entitydefinition = new Entitydefinition()
                {
                    href = "https://moebecktest.stylelabsdemo.com/api/entitydefinitions/M.PublicLink"
                },
                relations = new Relations()
                {
                    AssetToPublicLink = new Assettopubliclink()
                    {
                        parents = parents
                    }
                }
            };

            // Generate new Public Link
            DefinitionResponse generatedLink = await this.Request("entitydefinitions/M.PublicLink/entities", _config.Token)
                .PostJsonAsync(request).ReceiveJson<DefinitionResponse>();

            EntityResponse entity = await this.Request("entities/" + generatedLink.id.ToString(), _config.Token)
                .SetQueryParams(new
                {
                    culture = "en-US"
                })
                .GetJsonAsync<EntityResponse>();

            return entity.public_link;
        }

        public async Task<string> UploadAsset(string filename, string filepath, string base64file, string productID)
        {
            Uri endpoint = new Uri(_config.BaseUrl);

            OAuthPasswordGrant oauth = new OAuthPasswordGrant
            {
                ClientId = _config.ClientId,
                ClientSecret = _config.ClientSecret,
                UserName = _config.Username,
                Password = _config.Password
            };

            // Create the Web SDK client
            IWebMClient MClient = MClientFactory.CreateMClient(endpoint, oauth);

            // Create Asset
            long assetID = await CreateAsset(MClient, productID);

            // Approve Asset
            await MClient.Assets.FinalLifeCycleManager.ApproveAsync(assetID);

            // Upload Image
            await UploadImageAsync(filename, base64file, assetID);

            // Create Public Link
            string publicLink = await CreatePublicLink(assetID);

            return publicLink;
        }
    }
}
