using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class TripFeedback
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string BoatType { get; set; }
        public string BoatTypeId { get; set; }
        public string SeaterId { get; set; }
        public string Ratings { get; set; }
        public string Comments { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string MobileNo { get; set; }
        public string ActiveStatus { get; set; }
    }
}