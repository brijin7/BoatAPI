using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_GroupMaster
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string GroupCategory { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FA_GroupMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_GroupMasterList
    {
        public List<FA_GroupMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}