using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    /******************************************ROWER DROPDOWN************************************************/
    public class Rower
    {
        public string BoatHouseId { get; set; }
        public string BoatTypeId { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string BoatSeaterId { get; set; }
        public string TripDate { get; set; }
    }

    public class RowerRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RowerList
    {
        public List<Rower> Response { get; set; }
        public int StatusCode { get; set; }
    }

    /******************************************GET CUSTOMER DETAILS************************************************/
    public class TripCustomer
    {
        public string BoatId { get; set; }
        public string BoatNumber { get; set; }
        public string RowerId { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string BookingId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }

        public string BoatHouseId { get; set; }
        public string BoatReferenceNo { get; set; }

        public string InitNetAmount { get; set; }
        public string BoatDeposit { get; set; }

        public string InitBillAmount { get; set; }

        public string ActualBillAmount { get; set; }

        public string PaymentType { get; set; }
        public string CustomerAddress { get; set; }

        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string PremiumStatus { get; set; }
    }

    public class TripCustomerRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class TripCustomerList
    {
        public List<TripCustomer> Response { get; set; }
        public int StatusCode { get; set; }
    }

    /******************************************UPDATE TRIPSHEET************************************************/
    public class UpdateSheet
    {
        public string QueryType { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BHTripStartTime { get; set; }
        public string BHTripEndTime { get; set; }
        public string ActualNetAmount { get; set; }
        public string DepRefundAmount { get; set; }
        public string BookingId { get; set; }
        public string RowerId { get; set; }

        public string BoatHouseId { get; set; }
        public string BoatId { get; set; }
        public string BoatNum { get; set; }
        public string CreatedBy { get; set; }

        public string BoatName { get; set; }
        public string RowerName { get; set; }

        public string ActualOfferAmount { get; set; }

        public string ActualBoatCharge { get; set; }

        public string ActualRowerCharge { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string BookingPin { get; set; }


        public string TripStartTime { get; set; }

        public string TripEndTime { get; set; }

        public string ActualNetAmountExtn { get; set; }

        public string ActualBoatChargeExtn { get; set; }

        public string ActualRowerChargeExtn { get; set; }

        public string ActualOfferAmountExtn { get; set; }

        public string ActualTaxExtn { get; set; }

        public string RePaymentType { get; set; }
        public string GSTNumber { get; set; }
        public string SeaterType { get; set; }
        public string BoatType { get; set; }
        public string NetAmount { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatHouseName { get; set; }
        public string TotalNetAmount { get; set; }
        public string TotalDefundAmount { get; set; }
        public string CashOnHand { get; set; }
        public string DepRefundDate { get; set; }
        public string TravelDifference { get; set; }

        public string CustomerMobile { get; set; }

        public string RowNumber { get; set; }

        public string CountStart { get; set; }       


    }

    public class UpdateSheetRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class UpdateSheetList
    {
        public List<UpdateSheet> Response { get; set; }
        public int StatusCode { get; set; }
    }


    /******************************************BOAT CHARGE DETAILS************************************************/
    public class BoatCharges
    {
        public string QueryType { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BookingId { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string BoatMinTime { get; set; }
        public string BoatMinCharge { get; set; }
        public string BoatExtnTime { get; set; }
        public string RowerExtnCharge { get; set; }
        public string BoatGraceTime { get; set; }
        public string Totaltime { get; set; }
        public string ExtensionChargePerMin { get; set; }
        public string TimeDiff { get; set; }
        public string ExtraTime { get; set; }
        public string ExtraExtCharge { get; set; }
        public string TotalNetAmount { get; set; }

        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string ActualNetAmt { get; set; }
        public string RowerMinCharge { get; set; }
        public string PremiumStatus { get; set; }

        public string OfferAmount { get; set; }

        public string BoatNumber { get; set; }
        public string BoatId { get; set; }
        public string RowerId { get; set; }


        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }


        public string InitNetAmount { get; set; }
        public string BoatDeposit { get; set; }

        public string InitBillAmount { get; set; }

        public string ActualBillAmount { get; set; }

        public string PaymentType { get; set; }
        public string CustomerAddress { get; set; }

        public string BookingDate { get; set; }

        public string BookingPin { get; set; }

        public string InitBoatCharge { get; set; }

        public string InitRowerCharge { get; set; }

        public string InitOfferAmount { get; set; }

        public string PaymentTypeId { get; set; }

        public string MinutesTaken { get; set; }

        public string ExtraMinutes { get; set; }

        public string Deposit { get; set; }
        public string TotalDeduction { get; set; }
        public string BoatDeduction { get; set; }
        public string Rowerdeduction { get; set; }
        public string TaxDeduction { get; set; }

        public string RefundAmount { get; set; }
        public string TimeExtension { get; set; }
        public string ExtensionMsg { get; set; }
        public string ExtensionSlabId { get; set; }

        public string CollectedBalance { get; set; }

    }

    public class BoatChargesRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BoatChargesList
    {
        public List<BoatCharges> Response { get; set; }
        public int StatusCode { get; set; }
    }



}