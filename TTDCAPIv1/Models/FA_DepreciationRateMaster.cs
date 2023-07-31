using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_DepreciationRateMaster
    {
        public string QueryType { get; set; }
        public string GroupCode { get; set; }
        public string RateLifeFlag { get; set; }

        public string AnnualRate { get; set; }
        public string LifeType { get; set; }
        public string LifePeriod { get; set; }
        public string CorpId { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FA_DepreciationRateMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_DepreciationRateMasterList
    {
        public List<FA_DepreciationRateMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}