using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class AdminAccess
    {

        public string QueryType { get; set; }
        public string ServiceType { get; set; }
        public string UniqueId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string RoleName { get; set; }
        public string MBoatInfoDisplay { get; set; }

        //Newly Added
        public string BMKioskOtherService { get; set; }

        //Newly Added
        public string MMaster { get; set; }
        public string MBS { get; set; }
        public string MTMS { get; set; }
        public string MHMS { get; set; }
        public string MAccounts { get; set; }

        public string MComMaster { get; set; }
        public string MBhMaster { get; set; }
        public string MHotelMaster { get; set; }
        public string MTourMaster { get; set; }
        public string MAccessRights { get; set; }
        public string MOtherMaster { get; set; }

        public string BMaster { get; set; }
        public string BTransaction { get; set; }
        public string BBooking { get; set; }
        public string BReports { get; set; }
        public string BRestaurant { get; set; }
        public string BOtherService { get; set; }
        public string BDepositRefundOptions { get; set; }

        public string BMBooking { get; set; }
        public string BBoatingService { get; set; }
        public string BMBookingOthers { get; set; }
        public string BMBulkBooking { get; set; }
        public string BMOtherService { get; set; }
        public string BMBulkOtherService { get; set; }
        public string BMKioskBooking { get; set; }
        public string BMTripSheet { get; set; }
        public string BTripSheetOptions { get; set; }
        public string BMChangeTripSheet { get; set; }
        public string BMBoatReTripDetails { get; set; }
        public string BMChangeBoatDetails { get; set; }
        public string BMCancellation { get; set; }
        public string BMReSchedule { get; set; }


        public string TMMaterialPur { get; set; }
        public string TMMaterialIss { get; set; }
        public string TMTripSheetSettle { get; set; }
        public string TMRowerSettle { get; set; }
        public string TMStockEntryMaintance { get; set; }
        public string BMAdditionalService { get; set; }
        public string BAdditionalService { get; set; }

        public string RMBooking { get; set; }
        public string RMOtherSvc { get; set; }
        public string RMRestaurantService { get; set; }

        public string RMAbstractBoatBook { get; set; }
        public string RMAbstractOthSvc { get; set; }
        public string RMAbstractResSvc { get; set; }

        public string RMAvailBoatCapacity { get; set; }
        public string RMBoatwiseTrip { get; set; }
        public string RMTripSheetSettle { get; set; }

        public string RMRowerCharges { get; set; }
        public string RMBoatCancellation { get; set; }
        public string RMRowerSettle { get; set; }

        public string RMChallanRegister { get; set; }
        public string RMAbstractChallanRegister { get; set; }

        public string TMRefundCounter { get; set; }
        public string RMServiceWiseCollection { get; set; }

        public string OfflineRights { get; set; }

        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CreatedBy { get; set; }

        public string RMUserBookingReport { get; set; }
        public string RMTripWiseCollection { get; set; }
        public string RMBoatTypeRowerList { get; set; }

        //Newly Added
        public string TMReceiptBalanceRefund { get; set; }
        public string RMAdditionalTicket { get; set; }
        public string RMAbstractAdditionalTicket { get; set; }
        public string RMDepositStatus { get; set; }
        public string RMDiscountReport { get; set; }
        public string RMCashinHands { get; set; }
        public string RMExtendedBoatHouse { get; set; }
        public string RMPrintBoatBooking { get; set; }
        public string BGeneratingBoardingPass { get; set; }
        public string BGenerateManualTicket { get; set; }
        public string RMTripWiseDetails { get; set; }
        public string RMReceiptBalance { get; set; }
        public string RMRePrintReport { get; set; }
        public string RMQRCodeGeneration { get; set; }
        public string CountStart { get; set; }        

    }
    public class AdminAccessRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class AdminAccessList
    {
        public List<AdminAccess> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ModuleAccess
    {
        public string QueryType { get; set; }
        public string ServiceType { get; set; }
        public string UniqueId { get; set; }
        public string UserId { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string RoleName { get; set; }

        public string MMaster { get; set; }
        public string MBoating { get; set; }
        public string MTour { get; set; }
        public string MHotel { get; set; }
        public string MFixedAssets { get; set; }
        public string MRestaurant { get; set; }
        public string MBar { get; set; }

        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string CreatedBy { get; set; }
        
    }
}