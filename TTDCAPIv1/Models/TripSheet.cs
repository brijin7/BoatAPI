using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class TripSheet
    {
        public string QueryType { get; set; }
        public string BillQRCode { get; set; }
        public string BookingId { get; set; }
        public string BookingDuration { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatTypeName { get; set; }
        public string BoatSeaterId{ get; set; }
        public string BoatSeaterName { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string RowerId { get; set; }
        public string BoatId { get; set; }
        public string BoatNum { get; set; }
        public string BoatName { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string CreatedBy { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BookingPin { get; set; }
        public string PremiumStatus { get; set; }
        public string BookingDate { get; set; }
        public string BoatType { get; set; }
        public string RowerName { get; set; }
        public string InitNetAmount { get; set; }
        public string Deposit { get; set; }
        public string CustomerMobile { get; set; }
        public string TravelDuration { get; set; }
        public string TravelDifference { get; set; }
        public string RefundDuration { get; set; }

        public string CollectedAmount { get; set; }
        public string BillAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string RStatus { get; set; }
        public string RePaymentType { get; set; }
        public string RefunderName { get; set; }
        public string DepRefundAmount { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string ExpectedBoatId { get; set; }
        public string ExpectedBoatNum { get; set; }
        public string SeaterType { get; set; }
        public string ExpectedTime { get; set; }
        public string CountStart { get; set; }
        public string BookingIdORPin { get; set; }
        public string RowNumber { get; set; }
        public string DepositRefundBy { get; set; }
        public string DepRefundDate { get; set; }
    }
    public class TripSheetRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class TripSheetList
    {
        public List<TripSheet> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class GridTripSheet
    {
        public string BookingId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoatCharge { get; set; }
        public string InitRowerCharge { get; set; }
        public string ActualRowerCharge { get; set; }
        public string BoatNumber { get; set; }
        public string BoatId { get; set; }
        public string BoatName { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string BillQRCode { get; set; }
    }

    public class GridTripSheetRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class GridTripSheetList
    {
        public List<GridTripSheet> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class AbstractTripSheetSummaryReport
    {
        public string QueryType { get; set; }
        public string BoatHouseId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

    }
    public class AbstractTripSheetSummaryReportRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class AbstractTripSheetSummaryReportList
    {
        public List<AbstractTripSheetSummaryReport> Response { get; set; }
        public int StatusCode { get; set; }
    }




    public class IndividualScanTripSheet
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BoatId { get; set; }
        public string BoatNum { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string ActualBoatId { get; set; }

        public string BoatHouseName { get; set; }
        public string BookingPin { get; set; }
        public string ExpectedTime { get; set; }
        public string BookingSerial { get; set; }
        public string UserId { get; set; }
        public string BoardingTime { get; set; }
        public string PremiumStatus { get; set; }
        public string BookingDuration { get; set; }
        public string BookingMedia { get; set; }
        public string SelfDrive { get; set; }
        public string RowerCharge { get; set; }
        public string BarcodePin { get; set; }
        public string TraveledTime { get; set; }
        public string SSUserBy { get; set; }
        public string SDUserBy { get; set; }
        public string BoatDeposit { get; set; }
        public string DefaultEndTime { get; set; }
        public string BDtlPremiumStatus { get; set; }
    }


    public class IndividualTripSheetWeb

    {
        public string QueryType { get; set; }
        public string[] BookingId { get; set; }
        public int[] BoatReferenceNo { get; set; }
        public string[] BoatId { get; set; }
        public string BoatNum { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string RowerId { get; set; }
        public string[] RowerIds { get; set; }
        public string RowerName { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string ActualBoatId { get; set; }

        public string BoatHouseName { get; set; }
        public string BookingPin { get; set; }
        public string ExpectedTime { get; set; }
        public string BookingSerial { get; set; }
        public string UserId { get; set; }
        public string BoardingTime { get; set; }
        public string PremiumStatus { get; set; }
        public string BookingDuration { get; set; }
        public string BookingMedia { get; set; }
        public string SelfDrive { get; set; }
        public string RowerCharge { get; set; }
        public string BarcodePin { get; set; }
        public string TraveledTime { get; set; }
        public string SSUserBy { get; set; }
        public string SDUserBy { get; set; }
        public string BoatDeposit { get; set; }
        public string DefaultEndTime { get; set; }
    }

    public class IndividualScanTripSheetString
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class IndividualScanTripSheetResponse
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class IndividualScanTripSheetList
    {
        public List<IndividualScanTripSheet> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class IndividualTripSheet
    {
        public string QueryType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatTypeId { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string RowerId { get; set; }
        public string BoatSeaterId { get; set; }
        public string TripUser { get; set; }
    }

    public class IndividualTripSheetList
    {
        public DataTable DataTable { get; set; }
        public int StatusCode { get; set; }
        public string ResponseMsg { get; set; }
    }

   
    public class IndividualTripSheetWebList
    {
        public List<BoatBookingNew> Response
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

    public class IndividualTripSheetWebStr
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

    public class IndividualTripSheetWebResponse
    {
        public List<string> Response { get; set; }
        public int StatusCode { get; set; }
    }
}