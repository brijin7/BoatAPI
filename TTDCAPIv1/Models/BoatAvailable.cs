using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatAvailable
    {
        public string BoatHouseId { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; } = "0";
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string NoOfSeats { get; set; }
        public string SelfDrive { get; set; }
        public string DepositType { get; set; }
        public string Deposit { get; set; }
        public string TimeExtension { get; set; }
        public string BoatMinTime { get; set; }
        public string BoatExtnTime { get; set; }
        public string BoatGraceTime { get; set; }
        public string PremiumStatus { get; set; }
        public string BoatMinCharge { get; set; }       
        public string BoatExtnCharge { get; set; }
        public string RowerMinCharge { get; set; }
        public string RowerExtnCharge { get; set; }
        public string BoatTotalCharge { get; set; }
        public string BoatExtnTotalCharge { get; set; }
        public string BoatTaxCharge { get; set; }
        public string BoatExtnTaxCharge { get; set; }
        public string MaxTripsPerDay { get; set; }
        public string BoatImageLink { get; set; }
        public string BookedTrips { get; set; }
        public string RemainTrips { get; set; }
        public string BookingDate { get; set; }
        public string BHShortCode { get; set; }
        public string ExpectedTimeSlot { get; set; }          
        public string NTBoatMinCharge { get; set; }
        public string NTRowerMinCharge { get; set; }
        public string NTBoatTotalCharge { get; set; }
        public string NTBoatTaxCharge { get; set; }
        public string ECBoatMinCharge { get; set; }
        public string ECRowerMinCharge { get; set; }
        public string ECBoatTotalCharge { get; set; }
        public string ECBoatTaxCharge { get; set; }
        public string Individual { get; set; }
        public string Normal { get; set; }
        public string Express { get; set; }
        public string AverageRating { get; set; }
        public string NoOfRatings { get; set; }
        public string TotalNoofBoats { get; set; }
        
    }

    public class BoatAvailableList
    {
        public List<BoatAvailable> Response { get; set; }        
        public int StatusCode { get; set; }         
    }
    public class BoatAvailableString
    {
        public string Response { get; set; }        
        public int StatusCode { get; set; }   
    }

    public class BoatTicket
    {
        public string BookingId { get; set; } 
        public string BookingDate { get; set; } 
        public string BoatHouseId { get; set; } 
        public string BoatHouseName { get; set; }
        public string CustomerId { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PremiumStatus { get; set; }
        public string NoOfPass { get; set; }
        public string NoOfChild { get; set; }
        public string NoofInfant { get; set; }
        public string OfferName { get; set; }
        public string PaymentTypeName { get; set; }
        public string InitBillAmount { get; set; }
        public string BoatType { get; set; }
        public string SeaterType { get; set; } 
        public string BookingDuration { get; set; }
        public string InitBoatCharge { get; set; }
        public string InitRowerCharge { get; set; }        
        public string BoatDeposit { get; set; }       
        public string TaxAmount { get; set; }       
        public string BoatReferenceNo { get; set; }
        public string InitOfferAmount { get; set; }
        public string InitNetAmount { get; set; }
        public string GSTNumber { get; set; }
        public string ActualBoatNum { get; set; }
        public string ExpectedTime { get; set; }
        public string BookingPin { get; set; }
        public string BookingSerial { get; set; }
        public string BookingMedia { get; set; }
        public string CheckDate { get; set; }
        public string TripFlag { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string BoatTypeSno { get; set; }
        public string BHShortCode { get; set; }
        public string OrderId { get; set; }
        public string TrackingId { get; set; }
        public string BankReferenceNo { get; set; }
        public string TimeSlot { get; set; }

        public string RescheduledDate { get; set; }
        public string RescheduledAmount { get; set; }
        public string BankName { get; set; }        

    }

    public class BoatTicketlist
    {
        public List<BoatTicket> Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BoatTicketStr
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class CreditBoatTicket
    {
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string PremiumStatus { get; set; }
        public string PaymentTypeName { get; set; }
        public string BoatType { get; set; }
        public string SeaterType { get; set; }
        public string BookingDuration { get; set; }
        public string ActualBoatCharge { get; set; }
        public string ActualRowerCharge { get; set; }
        public string ActualNetAmount { get; set; }
        public string GSTNumber { get; set; }
        public string BookingPin { get; set; }
        public string UniqueId { get; set; }
        public string BookingMedia { get; set; }
        public string BoatTypeSno { get; set; }
    }

    public class CreditBoatTicketlist
    {
        public List<CreditBoatTicket> Response { get; set; }        
        public int StatusCode { get; set; }
    }
    public class CreditBoatTicketStr
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    /// <summary>
    /// 2021-07-23 
    /// Vediyappan
    /// New API Developed.
    /// </summary>
    public class BoatSlotMaster
    {
        public string QueryType { get; set; }
        public string SlotId { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string SlotStartTime { get; set; }
        public string SlotEndTime { get; set; }
        public string SlotDuration { get; set; }      
        public string TotalTripCount { get; set; }
        public string BookedTripCount { get; set; }
        public string AvailableTripCount { get; set; }      
        public string BoatHouseId { get; set; }
        public string CheckInDate { get; set; }
        public string SlotType { get; set; }
        public string UserId { get; set; }
        public string AvailableBoatCount { get; set; }
        public string AvailableSeatCount { get; set; }
        public string BlockId { get; set; } = "0";
        public string ServiceType { get; set; }
        public string CreatedBy { get; set; }
        public string SlotIdold { get; set; }
        public string SlotTime { get; set; }
        public string BoatTypeName { get; set; }
        public string BoatSeaterName { get; set; }   
    }

    public class BoatSlotMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BoatSlotMasterString
    {
        public string Response { get; set; }

        public int StatusCode { get; set; }
    }

    public class BoatSlotMasterList
    {
        public List<BoatSlotMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}