using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatTypeMaster
    {
        public string CreatedBy
        {
            get; set;
        }
        public string QueryType { get; set; }
        public string BoatTypeId
        {
            get;
            set;
        }
        public string BoatType
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
        public string ActiveStatus
        {
            get;
            set;
        }
    }
    public class BoatTypeMasterList
    {
        public List<BoatTypeMaster> Response
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
    public class BoatTypeMasterString
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
    public class LateRefund
    {
        public string QueryType { get; set; }
        public string ComplaintNo { get; set; }
        public string BookingId { get; set; }
        public string BoatHouseId { get; set; }
        public string BookingPin { get; set; }
        public string RefundDate { get; set; }
        public string CreatedBy { get; set; }
    }
    public class LateRefundList
    {
        public List<LateRefund> Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class LateRefundString
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
}