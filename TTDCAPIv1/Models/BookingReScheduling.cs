using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    //public class BookingReScheduling
    //{
    //    public string QueryType { get; set; }
    //    public string BookingId { get; set; }
    //    public string BoatHouseId { get; set; }
    //    public string BoatHouseName { get; set; }
    //    public string BookingDate { get; set; }
    //    public string CustomerMobile { get; set; }
    //    public string MobileNo { get; set; }
    //    public string CustomerAddress { get; set; }
    //    public string NoOfPass { get; set; }
    //    public string City { get; set; }
    //    public string BookingMedia { get; set; }
    //    public string CreatedBy { get; set; }

    //    public string ActivityId { get; set; }
    //    public string RescheduleCharge { get; set; }
    //    public string sBookingDate { get; set; }
    //    public string BoatReferenceNo { get; set; }

    //    public string FromDate { get; set; }
    //    public string ToDate { get; set; }

    //    public string NetAmount { get; set; }
    //    public string DepositAmount { get; set; }

    //    public string ChargeType { get; set; }

    //    public string RescheduledTotalcharge { get; set; }
    //    public string CGST { get; set; }

    //    public string SGST { get; set; }
    //    public string RescheduledCharge { get; set; }

    //    public string PaymentType { get; set; }

    //    public string Hour { get; set; }
    //    public string Minute { get; set; }

    //    public string BookingPin { get; set; }
    //    public string PaymentTypeName { get; set; }

    //    public string UserId { get; set; }



    //}
    //public class BookingReSchedulingRes
    //{
    //    public string Response { get; set; }
    //    public int StatusCode { get; set; }
    //}
    //public class BookingReSchedulingList
    //{
    //    public List<BookingReScheduling> Response { get; set; }
    //    public int StatusCode { get; set; }
    //}

    public class BookingReScheduling
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string[] BookingDate { get; set; }
        public string CustomerMobile { get; set; }
        public string MobileNo { get; set; }
        public string CustomerAddress { get; set; }
        public string NoOfPass { get; set; }
        public string City { get; set; }
        public string BookingMedia { get; set; }
        public string CreatedBy { get; set; }

        public string ActivityId { get; set; }
        public string RescheduleCharge { get; set; }
        public string sBookingDate { get; set; }
        public string BoatReferenceNo { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string NetAmount { get; set; }
        public string DepositAmount { get; set; }

        public string ChargeType { get; set; }

        public string RescheduledTotalcharge { get; set; }
        public string CGST { get; set; }

        public string SGST { get; set; }
        public string RescheduledCharge { get; set; }

        public string PaymentType { get; set; }

        public string Hour { get; set; }
        public string Minute { get; set; }

        public string[] BookingPin { get; set; }

        public string PaymentTypeName { get; set; }

        public string UserId { get; set; }

        public int[] SlotId { get; set; }

        public string ChargeAmount { get; set; }

        public string RescheduleType { get; set; }

        public string stgBookingPin { get; set; }

        public string RescheduleReason { get; set; }

        public string BHShortCode { get; set; }

        public string RescheduleOldDate { get; set; }

        public string RescheduleNewDate { get; set; }  
        public string OthersRescheduleDate { get; set; }  
    }
    public class BookingReSchedulingRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BookingReSchedulingResponse
    {
        public List<string> Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BookingReSchedulingList
    {
        public List<BookingReScheduling> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BookingReSchedulingGrid
    {
        public string BookingId { get; set; }
        public string BookingOldDate { get; set; }
        public string BookingNewDate { get; set; }
        public string RescheduledTotalCharge { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string RescheduledCharge { get; set; }
        public string PaymentType { get; set; }
        public string PaymentName { get; set; }
        public string BookingMedia { get; set; }

        public int BoatHouseId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CreatedDate { get; set; }

        public string UserId { get; set; }
    }
    public class BookingReSchedulingResGrid
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BookingReSchedulingListGrid
    {
        public List<BookingReSchedulingGrid> Response { get; set; }
        public int StatusCode { get; set; }
    }


    public class BookingHeaderDetails
    {
        public string BookingId { get; set; }       
        public string BookingDate { get; set; }
        public string BookingPin { get; set; }
        public string PremiumStatus { get; set; }

        public string InitBillAmount { get; set; }
        public string Payment { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string UserId { get; set; }
        public string PaymentType { get; set; }
    }
    public class BookingHeaderDetailsResGrid
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BookingHeaderDetailsListGrid
    {
        public List<BookingHeaderDetails> Response { get; set; }
        public int StatusCode { get; set; }
    }
}
