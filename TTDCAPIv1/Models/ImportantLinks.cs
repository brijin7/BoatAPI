using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ImportantLinks
    {
        public string QueryType { get; set; }
        public string LinkId { get; set; }
        public string LinkType { get; set; }
        public string LinkTypeName { get; set; }
        public string LinkName { get; set; }
        public string LinkURL { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }

    }
    public class ImportantLinksRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class ImportantLinksList
    {
        public List<ImportantLinks> Response { get; set; }
        public int StatusCode { get; set; }
    }
}