using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_BranchMaster
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchType { get; set; }
        public string BranchRegion { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UserId { get; set; }
        public string EmpName { get; set; }
        public string OperatingStatus { get; set; }

        public string OperativeDate { get; set; }
    }
    public class FA_BranchMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_BranchMasterList
    {
        public List<FA_BranchMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}