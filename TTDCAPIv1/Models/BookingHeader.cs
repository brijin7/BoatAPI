using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BookingHeader
    {
        public string BoatHouseId { get; set; }

       
        public string UserName { get; set; }
        public string NoOfPass { get; set; }
        public string InitNetAmount { get; set; }
    }

    public class BookingHeaderRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BookingHeaderList
    {
        public List<BookingHeader> Response { get; set; }
        public int StatusCode { get; set; }
    }
}