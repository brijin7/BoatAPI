using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class CommonAPIMethod
    {
        public string QueryType { get; set; }
        public string ServiceType { get; set; }
        public string CorpId { get; set; }
        public string BoatHouseId { get; set; }
        public string UserId { get; set; }
        public string BookingDate { get; set; }
        public string BookingPin { get; set; }
        public string BoatHouseName { get; set; }
        public string BookingId { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatStatusId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatStatus { get; set; }
        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Input3 { get; set; }
        public string Input4 { get; set; }
        public string Input5 { get; set; }
    }
    public class CommonAPIMethodres
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class CommonAPIMethodList
    {
        public List<CommonAPIMethod> Response { get; set; }
        public int StatusCode { get; set; }
    }
}