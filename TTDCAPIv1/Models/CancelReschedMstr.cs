using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class CancelReschedMstr
    {
        public string QueryType
        {
            get;
            set;
        }
        public string ActivityId
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string BoatHouseId
        {
            get;
            set;
        }
        public string BoatHouseName
        {
            get;
            set;
        }
        public string ActivityType
        {
            get;
            set;
        }
        public string ChargeType
        {
            get;
            set;
        }
        public string Charges
        {
            get;
            set;
        }
        public string ApplicableBefore
        {
            get;
            set;
        }
        public string EffectiveFrom
        {
            get;
            set;
        }
        public string EffectiveTill
        {
            get;
            set;
        }
        public string MaxNoOfResched
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public string ActiveStatus
        {
            get;
            set;
        }
        public string BookingId
        {
            get;
            set;
        }
        public string InitBillAmount
        {
            get;
            set;
        }
        public string DeductedCharges
        {
            get;
            set;
        }
        public string TotalCharges
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }
        public string MobileNo
        {
            get;
            set;
        }
        public string BookingDate
        {
            get;
            set;
        }
        public string Comments
        {
            get;
            set;
        }
        public string Refund
        {
            get;
            set;
        }
        public string CancellationDate
        {
            get;
            set;
        }
        public string CategoryName { get; set; }
        public string CancelledMedia { get; set; }
        public string CancelledBy { get; set; }
        public string Cgst { get; set; }
        public string Sgst { get; set; }
        public string PaymentType { get; set; }
        public string InitNetAmount { get; set; }
        public string BoatCharge { get; set; }
        public string BookingIdList { get; set; }
        public string BoatReferenceNoList { get; set; }


        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BoatType { get; set; }
        public string BoatSeater { get; set; }
        public string CancelCharges { get; set; }
        public string CancelRefund { get; set; }
        public string BookingPin { get; set; }
        public string DepositAmount { get; set; }
        public string CancellationHours { get; set; }


        public string TotalAmount { get; set; }
        public string NetAmount { get; set; }
        public string ChargesAmount { get; set; }
        public string CancelBoatCharges { get; set; }
        public string CancellationType { get; set; }
        public string CancelOtherService { get; set; }
        public string OtherCancelledBy { get; set; }        
        public string CountStart { get; set; }
        public string RowNumber { get; set; }
    }

    public class CancelReschedMstrRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class CancelReschedMstrList
    {
        public List<CancelReschedMstr> Response { get; set; }
        public int StatusCode { get; set; }
    }
}