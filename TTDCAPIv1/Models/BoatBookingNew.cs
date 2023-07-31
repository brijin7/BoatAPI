using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatBookingNew
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public int BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerGSTNo { get; set; }

        public string[] BoatPremiumStatus { get; set; }
        public string[] TaxAmount { get; set; }
        public string[] OthTaxAmount { get; set; }

        public int NoOfPass { get; set; }
        public int NoOfChild { get; set; }
        public int NoOfInfant { get; set; }
        public int OfferId { get; set; }
        public decimal InitBillAmount { get; set; }
        public int PaymentType { get; set; }
        public decimal ActualBillAmount { get; set; }
        public string Status { get; set; }
        public string BookingMedia { get; set; }
        public string BookingPin { get; set; }

        public string CustomerEmailId { get; set; }
        public string BoatReferenceNo { get; set; }
        public decimal CollectedAmount { get; set; }
        public decimal BalanceAmount { get; set; }

        //Book Detail Details
        public string[] BoatTypeId { get; set; }
        public string[] BoatSeaterId { get; set; }
        public string[] BookingDuration { get; set; }
        public string[] InitBoatCharge { get; set; }
        public string[] InitRowerCharge { get; set; }
        public string[] BoatDeposit { get; set; }
        public string[] TaxDetails { get; set; }
        public string[] InitOfferAmount { get; set; }
        public string[] InitNetAmount { get; set; }
        public string[] CreatedBy { get; set; }

        ////Other Service Booking
        public string[] OthServiceStatus { get; set; }
        public string[] OthServiceId { get; set; }
        public string[] OthChargePerItem { get; set; }
        public string[] OthNoOfItems { get; set; }
        public string[] OthTaxDetails { get; set; }
        public string[] OthNetAmount { get; set; }

        public string BFDInitBoatCharge { get; set; }
        public string BFDInitNetAmount { get; set; }
        public string BFDGST { get; set; }

        public string[] BookingTimeSlotId { get; set; }
        public string[] BookingBlockId { get; set; }

        public int[] Countslotids { get; set; }

        public string BranchId { get; set; }
        public string ModuleType { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }

        public string Total { get; set; }
        public string Count { get; set; }

        public string BoatTypeName { get; set; }
        public string BoatSeaterName { get; set; }
        public string ExpectedTime { get; set; }
        public string BoatImageLink { get; set; }

        public string UserId { get; set; }
        public string PaymentTypeId { get; set; }
        public string CancelledDate { get; set; }
        public string PremiumStatus { get; set; }

        public string[] CGSTTaxAmount { get; set; }
        public string[] SGSTTaxAmount { get; set; }
        public string[] CGSTOthTaxAmount { get; set; }

        public string[] SGSTOthTaxAmount { get; set; }
        public string[] BFDInitBoatCharges { get; set; }
        public string[] BFDInitNetAmounts { get; set; }
        public string[] BFDGSTs { get; set; }

    }

    public class BoatBookingNewList
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

    public class BoatBookingNewStr
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

    public class BoatBookingResponse
    {
        public List<string> Response { get; set; }
        public int StatusCode { get; set; }
    }

}
