using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class Common
    {
        public string PaymentMode { get; set; }
        public string CardBankName { get; set; }
        public string DataAccept { get; set; }


        public string MasterCreditcard { get; set; }
        public string AmexCreditcard { get; set; }
        public string DebitCardAmount { get; set; }
        public string DebitCardBelow { get; set; }
        public string DebitCardAbove { get; set; }
        public string HDFCNetBank { get; set; }
        public string OthersNetBank { get; set; }
        public string CashCard { get; set; }
        public string GST { get; set; }
        public string BankCharges { get; set; }

    }
    public class CommonList
    {
        public List<Common> Response
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
    public class CommonRes
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

    public class AppVersion
    {
        public string AppType { get; set; }
        public string VersionNo { get; set; }
        public string CreatedBy { get; set; }
    }
    public class AppVersionRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class AppVersionList
    {
        public List<AppVersion> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class DeviceInformation
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string DeviceNo { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
        public string SeaterName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

    }


    public class DeviceInformationRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class DeviceInformationList
    {
        public List<DeviceInformation> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class DeviceLogin
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string DeviceNo { get; set; }
        public string Role { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string BoatBookingWithOtherservice { get; set; }
        public string OfflineRights { get; set; }
        public string BoatBooking { get; set; }
        public string OSBooking { get; set; }
        public string AdditionalTicketBooking { get; set; }
        public string RestaurantBooking { get; set; }
        public string KioskBooking { get; set; }
        public string DepositRefund { get; set; }
        public string TripSheet { get; set; }
    }
    public class DeviceLoginRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class DeviceLoginList
    {
        public List<DeviceLogin> Response { get; set; }
        public int StatusCode { get; set; }
    }
}