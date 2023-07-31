using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_AssetTypeMaster
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string AssetType { get; set; }
        public string AssetTypeName { get; set; }
        public string CodePrefix { get; set; }
        public string GroupCode { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }

    public class FA_AssetTypeMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class FA_AssetTypeMasterList
    {
        public List<FA_AssetTypeMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}