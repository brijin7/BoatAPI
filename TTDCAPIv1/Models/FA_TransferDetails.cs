using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_TransferDetails
    {
        public string QueryType { get; set; }

        public string CorpId { get; set; }
        public string BranchCode { get; set; }
        public string TransfTransNo { get; set; }
        public string TransfDocNo { get; set; }
        public string TransfDocDt { get; set; }
        public string ActiveStatus { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedDate { get; set; }
        public string AssetCode { get; set; }
        public string FromLocationId { get; set; }
        public string FromDeptId { get; set; }
        public string FromCustodianId { get; set; }
        public string ToBranchCode { get; set; }
        public string ToLocationId { get; set; }
        public string ToDeptId { get; set; }
        public string ToCustodianId { get; set; }
        public string EffectiveDate { get; set; }
        public string AckStatus { get; set; }
        public string AckBy { get; set; }
        public string AckDate { get; set; }
        public string AckRemarks { get; set; }
        public string CreatedBy { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string SlNo { get; set; }
        public string ToBranchId { get; set; }
        public string ToBranchName { get; set; }

    }

    public class FA_TransferDetailsRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_TransferDetailsList
    {
        public List<FA_TransferDetails> Response { get; set; }
        public int StatusCode { get; set; }
    }

}