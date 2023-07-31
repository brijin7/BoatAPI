using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class Challan
    {
        public string BookingDate { get; set; }
        public string RadioButtonValue { get; set; }
        public string BoatNetAmount { get; set; }
        public string OtherNetAmount { get; set; }
        public string RestNetAmount { get; set; }
        public string BookingId { get; set; }
        public string Total { get; set; }
        public string UserName { get; set; }
        public string BoatHouseId { get; set; }
        public string PayMode { get; set; }

        public string UserId { get; set; }
    }

    public class ChallanRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ChallanList
    {
        public List<Challan> Response { get; set; }
        public int StatusCode { get; set; }
    }
}