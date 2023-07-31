using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class SMSService
    {
        public string ReferenceNo { get; set; }
        public string BookingId { get; set; }
        public string ServiceType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string MobileNo { get; set; }
        public string MediaType { get; set; }
        public string Remarks { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
    }
    public class SMSServiceList
    {
        public List<SMSService> Response
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
    public class SMSServiceRes
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
    public class BlockSMSService
    {
        public string MessageBlock { get; set; }
        public string CreatedBy { get; set; }
    }
   
    public class BlockSMSServiceRes
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