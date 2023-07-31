using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RptavailBoatCapacity
    {
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatStatus { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string BoatNum { get; set; }
        public string BoatName { get; set; }
        public string BoatStatusName { get; set; }
        public string MaxTripsPerDay { get; set; }
        public string TripStartTime { get; set; }
        public string RevenueCount { get; set; }
        public string RevenueAmount { get; set; }
        public string TripTotalAmount { get; set; }
        public string BoatId { get; set; }




    }
    public class RptavailBoatCapacityString
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RptavailBoatCapacityResList
    {
        public List<RptavailBoatCapacity> Response { get; set; }
        public int StatusCode { get; set; }
    }

}