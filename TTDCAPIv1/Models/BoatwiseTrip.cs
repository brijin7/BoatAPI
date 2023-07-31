using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatwiseTrip
    {
        public string BoatHouseId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatType { get; set; }
        public string BoatSeater { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BoatSelection { get; set; }
        public string BoatTypeId { get; set; }
        public string MaxTrips { get; set; }
        public string Trips { get; set; }
        public string Amount { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatName { get; set; }
        public string UserId { get; set; }
        public string BookingPin { get; set; }
    }
    public class BoatwiseTripres
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BoatwiseTripList
    {
        public List<BoatwiseTrip> Response { get; set; }
        public int StatusCode { get; set; }
    }
}