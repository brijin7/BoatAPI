using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class PaymentType
    {
        public string BoatHouseId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PaymentTypeId { get; set; }
        public string RevenueType { get; set; }
        public string BoatType { get; set; }
        public string BoatSeater { get; set; }
        public string BookingId { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BookingDate { get; set; }
        public string BoatCharge { get; set; }
        public string RowerCharge { get; set; }
        public string Deposit { get; set; }
        public string Tax { get; set; }


    }
    public class PaymentTyperes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class PaymentTypeList
    {
        public List<PaymentType> Response { get; set; }
        public int StatusCode { get; set; }
    }
}