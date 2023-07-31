using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RptBookingOthers
    {
        public string BookingId { get; set; }
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string BookingType { get; set; }
        public string BookingDate { get; set; }
        public string NetAmount { get; set; }
        public string BoatHouseId { get; set; }
        public string ChargePerItem { get; set; }
        public string NoOfItems { get; set; }
        public string GetTaxAmountDetails { get; set; }
        public string ServiceFare { get; set; }
        public string TaxAmount { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class RptBookingOthersList
    {
        public List<RptBookingOthers> Response
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
    public class RptBookingOthersString
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