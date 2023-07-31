using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatRateMaster
    {

        public string QueryType { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatTypeName { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatSeaterName { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoatImageLink { get; set; }
        public string SelfDrive { get; set; }
        public string DepositType { get; set; }
        public string DepositTypeName { get; set; }
        public string TimeExtension { get; set; }
        public string BoatMinTime { get; set; }
        public string BoatGraceTime { get; set; }
        public string Deposit { get; set; }

        public string BoatMinTotAmt { get; set; }
        public string BoatMinCharge { get; set; }
        public string RowerMinCharge { get; set; }
        public string BoatMinTaxAmt { get; set; }

        public string WEBoatMinTotAmt { get; set; }
        public string WEBoatMinCharge { get; set; }
        public string WERowerMinCharge { get; set; }
        public string WEBoatMinTaxAmt { get; set; }

        public string BoatPremTotAmt { get; set; }
        public string BoatPremMinCharge { get; set; }
        public string RowerPremMinCharge { get; set; }
        public string BoatPremTaxAmt { get; set; }
        public string IWDBoatMinTotAmt { get; set; }
        public string IWDBoatMinCharge { get; set; }
        public string IWDRowerMinCharge { get; set; }
        public string IWDBoatMinTaxAmt { get; set; }
        public string IWEBoatMinTotAmt { get; set; }
        public string IWEBoatMinCharge { get; set; }
        public string IWERowerMinCharge { get; set; }
        public string IWEBoatMinTaxAmt { get; set; }
        public string MaxTripsPerDay { get; set; }

        public string ChildApplicable { get; set; }
        public string NoofChildApplicable { get; set; }
        public string ChargePerChild { get; set; }
        public string ChargePerChildTotAmt { get; set; }
        public string ChargePerChildTaxAmt { get; set; }

        public string Createdby { get; set; }
        public string ActiveStatus { get; set; }
        public string Charges { get; set; }
        public string Amount { get; set; }
        public string GST { get; set; }
        public string UniqueId { get; set; }
        public string DisplayOrder { get; set; }
        public string PerHeadApplicable { get; set; }

    }

    public class BoatRateMasterList
    {
        public List<BoatRateMaster> Response
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
    public class BoatRateMasterString
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

    public class BoatRateExtnCharge
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatTypeName { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatSeaterName { get; set; }


        public string ExtensionType { get; set; }
        public string ExtensionTypeName { get; set; }
        public string ExtFromTime { get; set; }
        public string ExtToTime { get; set; }
        public string AmtType { get; set; }
        public string AmtTypeName { get; set; }
        public string AmtPer { get; set; }

        public string BoatExtnTotAmt { get; set; }
        public string RowerExtnCharge { get; set; }
        public string BoatExtnCharge { get; set; }
        public string BoatExtnTaxAmt { get; set; }

        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }

        public string Createdby { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class BoatRateExtnChargeList
    {
        public List<BoatRateExtnCharge> Response
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
    public class BoatRateExtnChargeRes
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



    public class BoatRateDetails
    {
        public string BoatHouseId { get; set; }
        public string BoatType { get; set; }
        public string BoatTypeId { get; set; }
        public string SeaterType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SelfDrive { get; set; }
        public string TimeExtension { get; set; }
        public string BoatMinTime { get; set; }
        public string BoatGraceTime { get; set; }
        public string Deposit { get; set; }
        public string BoatMinCharge { get; set; }
        public string RowerMinCharge { get; set; }
        public string BoatMinTaxAmt { get; set; }
        public string BoatMinTotAmt { get; set; }
        public string BoatPremMinCharge { get; set; }
        public string RowerPremMinCharge { get; set; }
        public string BoatPremTaxAmt { get; set; }
        public string BoatPremTotAmt { get; set; }
        public string ExtensionType { get; set; }
        public string ExtFromTime { get; set; }
        public string ExtToTime { get; set; }
        public string AmountType { get; set; }
        public string Percentage { get; set; }
        public string BoatExtnCharge { get; set; }
        public string RowerExtnCharge { get; set; }
        public string BoatExtnTaxAmt { get; set; }
        public string BoatExtnTotAmt { get; set; }

    }

    public class BoatRateDetailsList
    {
        public List<BoatRateDetails> Response
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
    public class BoatRateDetailsRes
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

}