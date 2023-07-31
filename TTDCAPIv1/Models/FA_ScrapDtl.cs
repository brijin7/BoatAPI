using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_ScrapDtl
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string ServiceType { get; set; }
        public string Input1 { get; set; }
        public string BranchCode { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }
        public string Input4 { get; set; }
        public string Input5 { get; set; }
        public string ScrapTransNo { get; set; }
        public string ScrapDocNo { get; set; }
        public string ScrapDocDt { get; set; }
        public string TransSlNo { get; set; }
        public string AssetCode { get; set; }
        public string ScrapDate { get; set; }
        public string Reason { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string ScrapValue { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string UniqueId { get; set; }
        public string ApprovalStatus { get; set; }
    }
    public class FA_ScrapDtlRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_ScrapDtlList
    {
        public List<FA_ScrapDtl> Response { get; set; }
        public int StatusCode { get; set; }
    }

}