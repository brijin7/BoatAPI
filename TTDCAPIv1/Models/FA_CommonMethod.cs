using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_CommonMethod
    {
        public string QueryType { get; set; }
        public string ServiceType { get; set; }
        public string CorpId { get; set; }
        public string BranchCode { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }
        public string Input4 { get; set; }
        public string Input5 { get; set; }
    }
    public class FA_CommonMethodRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_CommonMethodList
    {
        public List<FA_CommonMethod> Response { get; set; }
        public int StatusCode { get; set; }
    }
}