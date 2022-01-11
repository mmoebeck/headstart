using System;
using System.Collections.Generic;
using System.Text;

namespace ordercloud.integrations.contenthub.Models
{
    public class EntityResponse
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public string[] cultures { get; set; }
        public Properties properties { get; set; }
        public Relations relations { get; set; }
        public Created_By created_by { get; set; }
        public DateTime created_on { get; set; }
        public Modified_By modified_by { get; set; }
        public DateTime modified_on { get; set; }
        public Entitydefinition entitydefinition { get; set; }
        public Copy copy { get; set; }
        public Permissions permissions { get; set; }
        public Lifecycle lifecycle { get; set; }
        public Saved_Selections saved_selections { get; set; }
        public Roles roles { get; set; }
        public Annotations annotations { get; set; }
        public bool is_root_taxonomy_item { get; set; }
        public bool is_path_root { get; set; }
        public bool inherits_security { get; set; }
        public bool is_system_owned { get; set; }
        public int version { get; set; }
        public Full full { get; set; }
        public Self self { get; set; }
        public string public_link { get; set; }
        public Renditions renditions { get; set; }
    }

    public class Properties
    {
        public string RelativeUrl { get; set; }
        public string Resource { get; set; }
        public string ResourceType { get; set; }
        public string VersionHash { get; set; }
        public bool IsDisabled { get; set; }
        public object ExpirationDate { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        public string FileKey { get; set; }
        public Conversionconfiguration ConversionConfiguration { get; set; }
        public object PublishStatus { get; set; }
        public object PublishStatusDetails { get; set; }
    }

    public class Filetopubliclink
    {
        public string href { get; set; }
    }

    public class Themetopubliclinks
    {
        public string href { get; set; }
    }

    public class Ordertopubliclink
    {
        public string href { get; set; }
    }

    public class Pagetopubliclink
    {
        public string href { get; set; }
    }

    public class Publiclinktowhereused
    {
        public string href { get; set; }
    }

    public class Publiclinktousage
    {
        public string href { get; set; }
    }

    public class Created_By
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Modified_By
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Copy
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Permissions
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Lifecycle
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Saved_Selections
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Roles
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Annotations
    {
        public string href { get; set; }
        public string title { get; set; }
    }

    public class Full
    {
        public string href { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Renditions
    {
    }
}
