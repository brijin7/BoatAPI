using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_ManufacturerMaster
    {
        public string QueryType { get; set; }
        public string ManufId { get; set; }
        public string ManufName { get; set; }
        public string CorpId { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }

    public class FA_ManufacturerMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_ManufacturerMasterList
    {
        public List<FA_ManufacturerMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}