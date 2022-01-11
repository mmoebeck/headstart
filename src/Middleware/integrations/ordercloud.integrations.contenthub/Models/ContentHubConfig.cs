using System;
using System.Collections.Generic;
using System.Text;

namespace ordercloud.integrations.contenthub.Models
{
    public class ContentHubConfig
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
