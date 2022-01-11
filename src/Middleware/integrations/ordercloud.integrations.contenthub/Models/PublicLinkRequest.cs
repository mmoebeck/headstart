using System;
using System.Collections.Generic;
using System.Text;

namespace ordercloud.integrations.contenthub.Models
{
    public class PublicLinkRequest
    {
        public PublicLinkProperties properties { get; set; }
        public bool is_root_taxonomy_item { get; set; }
        public bool is_path_root { get; set; }
        public object identifier { get; set; }
        public bool inherits_security { get; set; }
        public Entitydefinition entitydefinition { get; set; }
        public Relations relations { get; set; }
    }

    public class PublicLinkProperties
    {
        public string RelativeUrl { get; set; }
        public string Resource { get; set; }
        public object ResourceType { get; set; }
        public object VersionHash { get; set; }
        public object IsDisabled { get; set; }
        public object ExpirationDate { get; set; }
        public object Status { get; set; }
        public object Progress { get; set; }
        public object FileKey { get; set; }
        public Conversionconfiguration ConversionConfiguration { get; set; }
        public object PublishStatus { get; set; }
        public object PublishStatusDetails { get; set; }
    }

    public class Conversionconfiguration
    {
    }

    public class Entitydefinition
    {
        public string href { get; set; }
    }

    public class Relations
    {
        public Assettopubliclink AssetToPublicLink { get; set; }
    }

    public class Assettopubliclink
    {
        public List<Parent> parents { get; set; }
    }

    public class Parent
    {
        public string href { get; set; }
    }
}
