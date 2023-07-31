using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OthServiceCat
    {
        public string BoatHouseId { get; set; }
        public string Category { get; set; }
        public string CategoryName { get; set; }
        public string TicketCount { get; set; }
        public string ServiceId { get; set; }
        public string BookingType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Amount { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string CategoryId { get; set; }
        public string ServiceName { get; set; }
        public string ChargePerItem { get; set; }
        public string NoOfItems { get; set; }
        public string ServiceFare { get; set; }
        public string TaxAmount { get; set; }
        public string NetAmount { get; set; }
        public string ItemAmount { get; set; }
        public string ItemFare { get; set; }
        public string BoatTypeId { get; set; }
        public string PaymentType { get; set; }
        public string CreatedBy { get; set; }
        public string TotalCount { get; set; }

    }
    public class OthServiceCatList
    {
        public List<OthServiceCat> Response
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
    public class OthServiceCatString
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