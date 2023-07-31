using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RETripEntryDtls
    {
        public string BookingId { get; set; }
        public string BookingPin { get; set; }
        public string BoatType { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatSeat { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoatNumber { get; set; }
        public string BoatReferenceNo { get; set; }
        public string Reason { get; set; }
        public string BoatId { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string BookedDate { get; set; }
        public string Rower { get; set; }
        public string RowerId { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string Querytype { get; set; }
        public string ReTripStartTime { get; set; }
        public string ReTripEndTime { get; set; }
    }
    public class RETripEntryDtlsString
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RETripEntryDtlsStringResList
    {
        public List<RETripEntryDtls> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class Refund
    {
        public string QueryType { get; set; }

        public string UniqueId { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string RequestedAmount { get; set; }

        public string RequestedBy { get; set; }

        public string RequestedDate { get; set; }

        public string PaidAmount { get; set; }

        public string PaidBy { get; set; }

        public string PaidDate { get; set; }

        public string RequestStatus { get; set; }

        public string PaymentStatus { get; set; }

        public string BoatHouseId { get; set; }

        public string BoatHouseName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatTypeName { get; set; }

    }
    public class RefundRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RefundList
    {
        public List<Refund> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RefundCounter
    {
        public string UserName { get; set; }
        public string BoatHouseId { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public string BoatType { get; set; }
        public string BoatTypeId { get; set; }
        public string Amount { get; set; }
        public string BookingDate { get; set; }
    }
    public class RefundCounterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RefundCounterList
    {
        public List<RefundCounter> Response { get; set; }
        public int StatusCode { get; set; }
    }
}