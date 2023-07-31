using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BookingBoardingPass
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BoardingPassNo { get; set; }
        public string ActualBoatNum { get; set; }
        public string ExpectedTime { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string expectime { get; set; }
        public string BoatReferenceNum { get; set; }
        public string BookingPin { get; set; }
        public string BoatType { get; set; }
        public string SeatType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string OldBoatNum { get; set; }
        public string OldExpectedTime { get; set; }
        public string NewBoatNum { get; set; }
        public string NewExpectedTime { get; set; }
        public string ActualBoatId { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string NewRowerId { get; set; }
        public string NewTripStartTime { get; set; }
        public string NewTripEndTime { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string NewBoatId { get; set; }
        public string OldBoatId { get; set; }
        public string OldRowerId { get; set; }
        public string OldTripStartTime { get; set; }
        public string OldTripEndTime { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string BoatDuration { get; set; }
        public string TravelDuration { get; set; }

        public string BoatDeposit { get; set; }
        public string OldDepRefundAmount { get; set; }
        public string NewDepRefundAmount { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CountStart { get; set; }
        
    }


    public class BookingBoardingPassList
    {
        public List<BookingBoardingPass> Response
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
    public class BookingBoardingPassRes
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