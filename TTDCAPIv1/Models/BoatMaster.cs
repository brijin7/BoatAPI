using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatMaster
    {
        public string QueryType { get; set; }
        public string BoatId
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

        public string BoatNum
        {
            get;
            set;
        }
        public string BoatName
        {
            get;
            set;
        }
        public string BoatTypeId { get; set; }
        public string BoatTypeName { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatSeaterName { get; set; }
        public string BoatOwner { get; set; }
        public string BoatStatusName { get; set; }
        public string PayTypeName { get; set; }
        public string PaymentModel { get; set; }
        public string PaymentModelName { get; set; }
        public string PaymentPercent { get; set; }
        public string PaymentAmount { get; set; }
        public string BoatStatus { get; set; }
        public string CreatedBy { get; set; }
        public string BoatNature { get; set; }
        public string Normal { get; set; }
        public string Premium { get; set; }
        public string Express { get; set; }
        public string Total { get; set; }
    }

    public class BoatMasterList
    {
        public List<BoatMaster> Response
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
    public class BoatMasterString
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

    public class BoatMstr
    {
        public string BoatId
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

        public string BoatNum
        {
            get;
            set;
        }
        public string BoatName
        {
            get;
            set;
        }
        public string BoatTypeId { get; set; }
        public string BoatTypeName { get; set; }
        public string BoatSeaterId { get; set; }
        public string BoatSeaterName { get; set; }
        public string BoatStatus { get; set; }

    }

    public class BoatMsrList
    {
        public List<BoatMstr> Response
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
    public class BoatMsrString
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