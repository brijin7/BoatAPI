using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class PublicOnlineBooking
    {
        public string QueryType { get; set; }
        public string TransactionNo { get; set; }
        public string BookingDate { get; set; }
        public string BookingPin { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string BookingType { get; set; }
        public string PremiumStatus { get; set; }
        public string[] BoatPremiumStatus { get; set; }
        public string NoOfPass { get; set; }
        public string NoOfChild { get; set; }
        public string NoOfInfant { get; set; }
        public string OfferId { get; set; }
        public string InitBillAmount { get; set; }
        public string PaymentType { get; set; }
        public string ActualBillAmount { get; set; }
        public string Status { get; set; }

        public string BookingMedia { get; set; }
        public string BankReferenceNo { get; set; }
        public string OrderStatus { get; set; }
        public string OrderId { get; set; }
        public string TrackingId { get; set; }



        //Book Detail Details
        public string[] BoatTypeId { get; set; }
        public string[] BoatSeaterId { get; set; }
        public string[] BookingDuration { get; set; }
        public string[] InitBoatCharge { get; set; }
        public string[] InitRowerCharge { get; set; }
        public string[] BoatDeposit { get; set; }
        public string[] TaxDetails { get; set; }
        public string[] TaxAmount { get; set; }
        public string[] InitOfferAmount { get; set; }
        public string[] InitNetAmount { get; set; }
        public string CreatedBy { get; set; }


        public string[] OthServiceStatus { get; set; }
        public string[] OthServiceId { get; set; }
        public string[] OthChargePerItem { get; set; }
        public string[] OthNoOfItems { get; set; }
        public string[] OthTaxDetails { get; set; }
        public string[] OthTaxAmount { get; set; }
        public string[] OthNetAmount { get; set; }


        public string BRSBookingId { get; set; }
        public string BRSNewDate { get; set; }
        public string BRSCharge { get; set; }
        public string BRSCGST { get; set; }
        public string BRSSGST { get; set; }
        public string BRSRuleId { get; set; }

        public string BFDInitBoatCharge { get; set; }
        public string BFDInitNetAmount { get; set; }
        public string BFDGST { get; set; }
        public string EntryType { get; set; }
        public string ModuleType { get; set; }
        public int[] BookingBlockId { get; set; }
        public string[] BookingTimeSlotId { get; set; }
        public string CustomerGSTNo { get; set; }
        public string[] CGSTTaxAmount { get; set; }
        public string[] SGSTTaxAmount { get; set; }
        public string[] CGSTOthTaxAmount { get; set; }
        public string[] SGSTOthTaxAmount { get; set; }
    }

    public class PublicOnlineBookingList
    {
        public List<PublicOnlineBooking> Response
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

    public class PublicOnlineBookingStatus
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