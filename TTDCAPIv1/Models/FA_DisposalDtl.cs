using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{

    public class FA_DisposalDtl
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string BranchCode { get; set; }
        public string DispTransNo { get; set; }
        public string DispDocNo { get; set; }
        public string DispDocDt { get; set; }
        public string TransSlNo { get; set; }
        public string AssetCode { get; set; }
        public string DispDate { get; set; }
        public string DispValue { get; set; }
        public string Createdby { get; set; }
        public string ActiveStatus { get; set; }
        public string AssetGroup { get; set; }
        public string AssetType { get; set; }

        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string UniqueId { get; set; }
        public string ApprovalStatus { get; set; }
    }
    public class FA_DisposalDtlRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_DisposalDtlList
    {
        public List<FA_DisposalDtl> Response { get; set; }
        public int StatusCode { get; set; }
    }


}