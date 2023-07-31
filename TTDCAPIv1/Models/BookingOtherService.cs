using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BookingOtherServices
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string ServiceId { get; set; }
        public string BookingType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BookingDate { get; set; }
        public string ChargePerItem { get; set; }
        public string NoOfItems { get; set; }       
        public string BookingMedia { get; set; }      
        public string TaxDetails { get; set; }
        public string NetAmount { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CreatedBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }



        public string ServiceName { get; set; }
        public string Count { get; set; }
        public string Amount { get; set; }
        public string PaymentType { get; set; }
        public string Total { get; set; }
        public string CGSTTaxAmount { get; set; }
        public string SGSTTaxAmount { get; set; }

    }
    public class BookingOtherServicesRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BookingOtherServicesList
    {
        public List<BookingOtherServices> Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class OtherTicket
    {
        public string BookingId { get; set; }

        public string ServiceId { get; set; }

        public string ServiceName { get; set; }

        public string BookingType { get; set; }

        public string BoatHouseId { get; set; }

        public string BoatHouseName { get; set; }

        public string BookingDate { get; set; }

        public string ChargePerItem { get; set; }

        public string NoOfItems { get; set; }

        public string ServiceFare { get; set; }      

        public string TaxAmount { get; set; }

        public string NetAmount { get; set; }

        public string BillQRCode { get; set; }

        public string CustomerMobile { get; set; }

        public string CustomerName { get; set; }

        public string CustomerAddress { get; set; }

        public string PaymentTypeName { get; set; }

        public string GSTNumber { get; set; }
        public string CheckDate { get; set; }
        public string PaymentType { get; set; }

        public string OrderId { get; set; }

        public string TrackingId { get; set; }

        public string BankReferenceNo { get; set; }

        public string BankName { get; set; }

        public string RescheduledDate { get; set; }
        

    }
    public class OtherTicketlist
    {
        public List<OtherTicket> Response
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
    public class OtherTicketStr
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

    public class BookingHeaderAbs
    {
        public string BoatHouseId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string NoCount { get; set; }
        public string TotalAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatType { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BoatCharge { get; set; }
        public string BoatDeposit { get; set; }
    }

    public class BookingHeaderAbsRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BookingHeaderAbsList
    {
        public List<BookingHeaderAbs> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class AbstractOtherService
    {
        public string BoatHouseId { get; set; }        
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string NoCount { get; set; }
        public string Amount { get; set; }
    }

    public class AbstractOtherServiceRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class AbstractOtherServiceList
    {
        public List<AbstractOtherService> Response { get; set; }
        public int StatusCode { get; set; }
    }
}