using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatHouseMaster
    {
        public string QueryType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BoatLocnId { get; set; }
        public string BoatLocnName { get; set; }
        public string Address1 { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string BoatHouseManager { get; set; }
        public string BoatHouseManagerUserName { get; set; }
        public string BookingFrom { get; set; }
        public string BookingTo { get; set; }
        public string WorkingDays { get; set; }
        public string ActiveStatus { get; set; }
        public string MailId { get; set; }
        public string MaxChildAge { get; set; }
        public string MaxInfantAge { get; set; }
        public int TaxId { get; set; }
        public string CreatedBy { get; set; }
        public string GSTNumber { get; set; }
        public string TripStartAlertTime { get; set; }
        public string TripEndAlertTime { get; set; }
        public string RefundDuration { get; set; }
        public string ReprintTime { get; set; }
        public string AutoEndForNoDeposite { get; set; }
        public string QRcodeGenerate { get; set; }
        public string BHShortCode { get; set; }
        public string ExtensionPrint { get; set; }
        public string ExtnChargeStatus { get; set; }
        public string CorpId { get; set; }
    }
    public class BoatHouseMasterList
    {
        public List<BoatHouseMaster> Response
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
    public class BoatHouseMasterString
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
    public class getBoatHouseMaster
    {
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

        public string BookingTo
        {
            get;
            set;
        }

        public string BookingFrom
        {
            get;
            set;
        }
        public string ClosingBeforeTime
        {
            get;
            set;
        }
        public string ClassType
        {
            get;
            set;
        }

    }
    public class getBoatHouseMasterList
    {
        public List<getBoatHouseMaster> Response
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
    public class getBoatHouseMasterString
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

    public class WeekDaysMaster
    {
        public string QueryType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string WeekDays { get; set; }
        public string WeekDaysDesc { get; set; }
        public string HolidayDate { get; set; }
        public string HolidayDesc { get; set; }
        public string CreatedBy { get; set; }
    }

    public class WeekDaysMasterList
    {
        public List<WeekDaysMaster> Response { get; set; }       
        public int StatusCode { get; set; }        
    }

    public class WeekDaysMasterString
    {
        public string Response { get; set; }       
        public int StatusCode { get; set; }       
    }

    //****************************Imran**********************************//
    public class DisplayMaxCount
    {
        public string QueryType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string MaxCount { get; set; }
        public string CreatedBy { get; set; }
    }
    public class DisplayMaxCountRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class DisplayMaxCountList
    {
        public List<DisplayMaxCount> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LoginLog
    {
        public string QueryType { get; set; }
        public string UserName { get; set; }
        public string SystemIP { get; set; }
        public string SessionId { get; set; }
        public string PublicIP { get; set; }
        public string Browser { get; set; }
        public string BVersion { get; set; }
        public string Log { get; set; }
        public string UserId { get; set; }
    }
    public class LoginLogRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class LoginLogList
    {
        public List<LoginLog> Response { get; set; }
        public int StatusCode { get; set; }
    }

}
