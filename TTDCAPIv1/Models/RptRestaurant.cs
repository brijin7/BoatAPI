using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RptRestaurant
    {

       public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string NoOfItems { get; set; }
        public string NetAmount { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string BoatHouseId { get; set; }

        public string Quantity { get; set; }
        public string Total { get; set; }

        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Charge { get; set; }
        public string TaxAmount { get; set; }
        public string BookingDate { get; set; }
        public string ChargePerItem { get; set; }
        public string BookingId { get; set; }
        public string ItemRate { get; set; }

    }
        public class RptRestaurantList
    {
            public List<RptRestaurant> Response
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
        public class RptRestaurantString
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