using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_ConfigurationMaster
    {
        public string QueryType { get; set; }
        public string TypeId { get; set; }
        public string ConfigId { get; set; }
        public string TypeName { get; set; }
        public string ConfigName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FA_ConfigurationMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_ConfigurationMasterList
    {
        public List<FA_ConfigurationMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}