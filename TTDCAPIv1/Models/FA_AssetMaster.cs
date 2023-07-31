using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_AssetMaster
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string AssetCode { get; set; }
        public string OldAssetCode { get; set; }
        public string AssetSl { get; set; }
        public string AssetName { get; set; }
        public string AssetShName { get; set; }
        public string AssetDesc { get; set; }
        public string BranchCode { get; set; }

        public string AssetTag { get; set; }
        public string AssetType { get; set; }
        public string AssetGroup { get; set; }

        public string LocationId { get; set; }
        public string DeptId { get; set; }
        public string VendorId { get; set; }
        public string ManufId { get; set; }
        public string CustodianId { get; set; }

        public string InstallationDt { get; set; }
        public string DepStartDt { get; set; }
        public string ProductSlNo { get; set; }
        public string ProductQty { get; set; }
        public string PurchaseRefNo { get; set; }
        public string PurchaseRefDt { get; set; }
        public string InvoiceRefNo { get; set; }
        public string InvoiceRefDt { get; set; }
        public string OtherRefNo { get; set; }
        public string OtherRefDt { get; set; }

        public string ProductCost { get; set; }
        public string WarrantyRef { get; set; }
        public string WarrantyStart { get; set; }
        public string WarrantyEnd { get; set; }
        public string ResidualPer { get; set; }
        public string ResidualValue { get; set; }

        public string OrginalQty { get; set; }
        public string QtySlNo { get; set; }
        public string OtherInfo { get; set; }
        public string WriteOffFlag { get; set; }
        public string NoDepFlag { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string StockSI { get; set; }
        public string StockName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }

    public class AssetDtl
    {
        public string CorpId { get; set; }
        public string AssetCode { get; set; }
        public string AssetSl { get; set; }
        public string AssetName { get; set; }
        public string AssetShName { get; set; }

        public string BranchId { get; set; }
      
        public string AssetDesc { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string AssetTag { get; set; }
        public string AssetType { get; set; }
        public string AssetTypeName { get; set; }
        public string AssetGroup { get; set; }
        public string GroupName { get; set; }

        public string LocationId { get; set; }
        public string DeptId { get; set; }
        public string VendorId { get; set; }
        public string ManufId { get; set; }
        public string CustodianId { get; set; }

        public string InstallationDt { get; set; }
        public string DepStartDt { get; set; }
        public string ProductSlNo { get; set; }
        public string ProductQty { get; set; }
        public string PurchaseRefNo { get; set; }

        public string PurchaseRefDt { get; set; }
        public string InvoiceRefNo { get; set; }
        public string InvoiceRefDt { get; set; }
        public string OtherRefNo { get; set; }
        public string OtherRefDt { get; set; }

        public string ProductCost { get; set; }
        public string WarrantyRef { get; set; }
        public string WarrantyStart { get; set; }
        public string WarrantyEnd { get; set; }
        public string ResidualPer { get; set; }

        public string ResidualValue { get; set; }
        public string ScrapDocNo { get; set; }
        public string ScrapDocDt { get; set; }
        public string DispDocNo { get; set; }
        public string DispDocDt { get; set; }

        public string DispValue { get; set; }
        public string OrginalQty { get; set; }
        public string QtySlNo { get; set; }
        public string OtherInfo { get; set; }
        public string WriteOffFlag { get; set; }

        public string NoDepFlag { get; set; }
        public string AssetStatus { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string UniqueId { get; set; }
        public string ApprovalStatus { get; set; }
        public string WriteOffFlagDesc { get; set; }
        public string NoDepFlagDesc { get; set; }
        public string DepTotal { get; set; }
        public string GrossTotal { get; set; }

        public string AssetClgValue { get; set; }
    }
    public class AssetDtlList
    {
        public List<AssetDtl> Response
        {

            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
    public class AssetDtlString
    {
        public string Response
        {
            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }

    public class FA_AssetOB
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string BranchCode { get; set; }
        public string AssetGroup { get; set; }
        public string FinYear { get; set; }
        public string AssetType { get; set; }
        public string AssetName { get; set; }
        public string GrossBlockOB { get; set; }
        public string NetBlockOB { get; set; }
        public string AssetCode { get; set; }
        public string DepBlockOB { get; set; }
        public string UniqueId { get; set; }
        public string CreatedBy { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
    }

    public class FA_AssetOBList
    {
        public List<FA_AssetOB> Response
        {

            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
    public class FA_AssetOBString
    {
        public string Response
        {
            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
    

    public class FA_AssetOBRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_DepreciationList
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string FinYear { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string AssetGroupCode { get; set; }
        public string AssetGroupName { get; set; }
        public string AssetType { get; set; }
        public string AssetTypeName { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string DepRate { get; set; }
        public string DepCurrent { get; set; }
        public string AssetResValue { get; set; }
    }
    public class FA_DepreciationListlst
    {
        public List<FA_DepreciationList> Response
        {

            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
    public class FA_DepreciationListRes
    {
        public string Response
        {
            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }


    public class FA_DepreciationDelete
    {
        public string QueryType { get; set; }
        public string UserId { get; set; }
        public string BranchCode { get; set; }
        public string UserName { get; set; }
        public string FinancialYear { get; set; }
        public string BranchName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class FA_DepreciationDeleteList
    {
        public List<FA_DepreciationDelete> Response
        {

            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }
    public class FA_DepreciationDeleteString
    {
        public string Response
        {
            get;
            set;
        }
        public int StatusCode
        {
            get;
            set;
        }
    }


    public class FA_DepreciationDeleteRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

}