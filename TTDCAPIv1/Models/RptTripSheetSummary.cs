using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RptTripSheetSummary
    {
        public string BoatHouseId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string DepositType { get; set; }
        public string BoatTypeId { get; set; }

        public string BookingId { get; set; }

        public string BoatTypeName { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatSeaterName { get; set; }
        public string BookingDuration { get; set; }
        public string PaymentType { get; set; }
        public string PaymentTypeName { get; set; }
        public string ActualBillAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string BoatReferenceNo { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string DepRefundAmount { get; set; }
        public string DepRefundStatus { get; set; }

        public string BoatId { get; set; }

        public string BoatName { get; set; }
        public string NoOfTrips { get; set; }
        public string RefundAmt { get; set; }
        public string UserRole { get; set; }
        public string CollectionAmt { get; set; }
        public string UserId { get; set; }
        public string InitNetAmount { get; set; }
        public string UnClaimbedDeposit { get; set; }
        public string ExtendedCharge { get; set; }
        public string BoatHouseName { get; set; }
        public string RowerAmount { get; set; }
        public string RowerSettlement { get; set; }
        public string TravelDuration { get; set; }

    }

    public class RptTripSheetSummaryRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RptTripSheetSummaryList
    {
        public List<RptTripSheetSummary> Response { get; set; }
        public int StatusCode { get; set; }
    }


    public class ServiceWiseReportHistory
    {
        public string QueryType { get; set; }
        public string UserId { get; set; }
        public string ReferenceId { get; set; }
        public string Denomination { get; set; }
        public string DenominationCount { get; set; }
        public string DenominationAmount { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string UserName { get; set; }
        public string Services { get; set; }
        public string Category { get; set; }
        public string Types { get; set; }
        public string PaymentType { get; set; }
        public string BookingDate { get; set; }
        public string ServiceId { get; set; }
        public string CategoryId { get; set; }
        public string TypeId { get; set; }
        public string PaymentTypeId { get; set; }
        public string ServiceTotal { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UniqueId { get; set; }
        public string ToDate { get; set; }

    }

    public class ServiceWiseReportHistoryList
    {
        public List<ServiceWiseReportHistory> Response
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

    public class ServiceWiseReportHistoryRes
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

    /*******************TripFeedBack*********************/
    /// <summary>
    /// Created By : Jaya Suriya
    /// Created Date : 18-08-2021
    /// 
    /// </summary>
    public class TripFeedBackComments
    {
        public string BoatHouseId { get; set; }
        public string BoatTypeId { get; set; }
        public string SeaterTypeId { get; set; }
        public string AverageRating { get; set; }
        public string Ratings { get; set; }
        public string Comments { get; set; }
        public string FirstName { get; set; }
    }
    public class TripFeedBackCommentsList
    {
        public List<TripFeedBackComments> Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class TripFeedBackCommentsResponse
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
}