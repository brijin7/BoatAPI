using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RowerMaster
    {
        public string QueryType { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string MobileNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string AadharId { get; set; }
        public string MailId { get; set; }
        public string PhotoLink { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string RowerType { get; set; }
        public string RowerTypeName { get; set; }
        public string DriverCategory { get; set; }
        public string RowNumber { get; set; }
        public string Search { get; set; }
        public string CountStart { get; set; }
    }

    public class RowerMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RowerMasterList
    {
        public List<RowerMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RowerSettlement
    {
        public string QueryType { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string TripDate { get; set; }
        public string TripCount { get; set; }
        public string TotalCharge { get; set; }
        public string PaidCharge { get; set; }
        public string BalanceCharge { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CreatedBy { get; set; }
    }

    public class RowerSettlementStr
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class RowerSettlementList
    {
        public List<RowerSettlement> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class SettlementAmount
    {
        public string BookingId { get; set; }
        public string BookingPin { get; set; }
        public string BoatReferenceNo { get; set; }
        public string CustomerMobile { get; set; }
        public string TripStartTime { get; set; }
        public string RowerId { get; set; }
        public string TripDate { get; set; }
        public string SettlementAmt { get; set; }
        public string SettlementId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string NoOfTrips { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SettlementDate { get; set; }
        public string RowerName { get; set; }
        public string BookingDate { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string ActualRowerCharge { get; set; }
        public string TripEndTime { get; set; }
        public string TravelledMinutes { get; set; }
    }
    public class SettlementAmountStr
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class SettlementAmountList
    {
        public List<SettlementAmount> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BH_RowerBoatAssign
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string SeaterId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
        public string SeaterName { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string MultipleTripRights { get; set; }
    }

    public class BH_RowerBoatAssignRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BH_RowerBoatAssignList
    {
        public List<BH_RowerBoatAssign> Response { get; set; }
        public int StatusCode { get; set; }
    }
}