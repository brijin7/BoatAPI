using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class NewBoatBooked
    {
        public string BoatTypeId { get; set; }
        public string BoatType
        {
            get;
            set;
        }
        public string BoatSeaterId
        {
            get;
            set;
        }
        public string SeaterType
        {
            get;
            set;
        }

        public string NoOfSeats
        {
            get;
            set;
        }
        public string SelfDrive
        {
            get;
            set;
        }
        public string DepositType { get; set; }
        public string Deposit { get; set; }
        public string TimeExtension { get; set; }
        public string BoatMinTime { get; set; }
        public string BoatExtnTime { get; set; }
        public string BoatGraceTime { get; set; }
        public string MaxTripsPerDay { get; set; }
        public string NormalAvailable { get; set; }
        public string NormalExpTripTime { get; set; }
        public string NormalMaxFare { get; set; }
        public string PremiumAvailable { get; set; }
        public string PremiumExpTripTime { get; set; }
        public string PremiumMaxFare { get; set; }
        public string NormalWaitingTrip { get; set; }
        public string PremiumWaitingTrip { get; set; }
        public string BookedNormalOnline { get; set; }
        public string BookedPremiumOnline { get; set; }
        public string BookedNormalBoatHouse { get; set; }
        public string BookedPremiumBoatHouse { get; set; }
        public string TotalNormalTrip { get; set; }
        public string TotalPremiumTrip { get; set; }
        public string NormalTripCompleted { get; set; }
        public string PremiumTripCompleted { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatImageLink { get; set; }
        public string BookingDate { get; set; }

        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string PremiumStatus { get; set; }

        public string TotalNoofBoats { get; set; }

        public string TotalNoofTrips { get; set; }        

    }


    public class NewBoatBookedList
    {
        public List<NewBoatBooked> Response
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
    public class NewBoatBookedString
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