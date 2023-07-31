using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OtherTicketBookedDtl
    {


        public string BookingId { get; set; }

        public string BookingType { get; set; }

        public string BoatHouseName { get; set; }

        public string BookingDate { get; set; }

        public string ItemCharge { get; set; }

        public string NoOfItems { get; set; }

        public string ServiceFare { get; set; }

        public string TaxAmount { get; set; }

        public string NetAmount { get; set; }

        public string BoatHouseId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string CustomerMobileNo { get; set; }
        public string UserId { get; set; }

        public string Status { get; set; }

        public string CountStart { get; set; }

        public string RowNumber { get; set; }

        public string Input1 { get; set; }        

        public string Search { get; set; }

    }
    public class OtherTicketBookedDtlLst
    {
        public List<OtherTicketBookedDtl> Response
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
    public class OtherTicketBookedDtlStr
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