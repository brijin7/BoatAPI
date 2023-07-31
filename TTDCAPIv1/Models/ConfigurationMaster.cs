using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ConfigurationMaster
    {
        public string QueryType { get; set; }
        public string TypeId { get; set; }
        public string ConfigId { get; set; }
        public string TypeName { get; set; }
        public string ConfigName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }



        public string BoatHouseId { get; set; }
    }

    public class ConfigurationMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ConfigurationMasterList
    {
        public List<ConfigurationMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}