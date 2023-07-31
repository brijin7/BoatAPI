using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class BoatBooking
    {
        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string BookingId { get; set; }
        public string BookingDate { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PremiumStatus { get; set; }
        public string NoOfPass { get; set; }
        public string NoOfChild { get; set; }
        public string NoOfInfant { get; set; }
        public string OfferId { get; set; }
        public string InitBillAmount { get; set; }
        public string PaymentType { get; set; }
        public string ActualBillAmount { get; set; }
        public string Status { get; set; }
        public string BookingMedia { get; set; }
        public string BookingPin { get; set; }
        public string CustomerGSTNo { get; set; }
        public string CustomerEmailId { get; set; }
        public string BoatReferenceNo { get; set; }
        public string CollectedAmount { get; set; }
        public string BalanceAmount { get; set; }

        //Book Detail Details
        public string BoatTypeId { get; set; }
        public string BoatSeaterId { get; set; }
        public string BookingDuration { get; set; }
        public string InitBoatCharge { get; set; }
        public string InitRowerCharge { get; set; }
        public string BoatDeposit { get; set; }
        public string TaxDetails { get; set; }
        public string InitOfferAmount { get; set; }
        public string InitNetAmount { get; set; }
        public string CreatedBy { get; set; }

        public string OthServiceStatus { get; set; }
        public string OthServiceId { get; set; }
        public string OthChargePerItem { get; set; }
        public string OthNoOfItems { get; set; }
        public string OthTaxDetails { get; set; }
        public string OthNetAmount { get; set; }

        public string UserId { get; set; }
        public string PaymentTypeId { get; set; }
        public string CancelledDate { get; set; }

        public string BFDInitBoatCharge { get; set; }
        public string BFDInitNetAmount { get; set; }
        public string BFDGST { get; set; }

        public string Fromdate { get; set; }
        public string Todate { get; set; }

        public string Total { get; set; }
        public string Count { get; set; }

        public string BoatTypeName { get; set; }
        public string BoatSeaterName { get; set; }
        public string ExpectedTime { get; set; }
        public string BoatImageLink { get; set; }

        public string BranchId { get; set; }
        public string ModuleType { get; set; }   
        
        public string BookingTimeSlotId { get; set; }
        public string BookingBlockId { get; set; }

        public string BoatCharges { get; set; }
        public string TimeSlot { get; set; }
        
    }

    public class BoatBookingList
    {
        public List<BoatBooking> Response
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
    public class BoatBookingStr
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

    public class OfflinePin
    {
        public string SerialNo { get; set; }
        public string UserId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BookingPin { get; set; }
        public string BookingMedia { get; set; }
        public string BookingId { get; set; }
        public string OthBookingId { get; set; }
        public string RestBookingId { get; set; }
    }

    public class OfflinePinList
    {
        public List<OfflinePin> Response
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


    public class BookedPin
    {
        public string SerialNo { get; set; }
        public string QueryType { get; set; }
        public string BookingDate { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string MailId { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BookingPin { get; set; }
        public string Name { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public string BookingType { get; set; }
        public string BookingMedia { get; set; }
    }

    public class BookedPinList
    {
        public List<BookedPin> Response
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

    public class BookedPinStr
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

    public class PrinterReg
    {
        public string UniqueId { get; set; }
        public string QueryType { get; set; }
        public string PrinterRegNo { get; set; }
        public string PrinterName { get; set; }
        public string ComputerName { get; set; }
        public string RegistrationStatus { get; set; }
        public string BoatHouseId { get; set; }
        public string PrinterMake { get; set; }
        public string AuthorisedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ActiveStatus { get; set; }
        public string PrinterStatus { get; set; }
        public string RegisteredDate { get; set; }

        public string PassCode { get; set; }

    }

    public class PrinterRegList
    {
        public List<PrinterReg> Response
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

    public class PrinterRegStr
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

    public class PrinterJob
    {

        public string LogNo { get; set; }
        public string PrinterRegNo { get; set; }
        public string BoatHouseId { get; set; }
        public string BookingId { get; set; }
        public string WithOthers { get; set; }
        public string ComputerName_Mac { get; set; }
        public string JobID { get; set; }
        public string Print_Date { get; set; }
        public string Job_Status { get; set; }
        public string Job_CreateDateTime { get; set; }
    }

    public class PrinterJobLst
    {
        public List<PrinterJob> Response
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

    public class PrinterJobStr
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

    public class PrinterPairs
    {
        public string QueryType { get; set; }
        public string PairLogId { get; set; }
        public string PrinterRegNo { get; set; }
        public string BoatHouseId { get; set; }
        public string ComputerName_Mac { get; set; }
        public string WebKey { get; set; }
        public string ApplicationKey { get; set; }
        public string IsPaired { get; set; }
        public string PairedOn { get; set; }
        public string PairedRemarks { get; set; }
        public string LogCreatedOn { get; set; }
        public string LogSystemIP { get; set; }
        public string PublicIP { get; set; }
    }

    public class PrinterPairslst
    {
        public List<PrinterPairs> Response
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
    public class PrinterPairsStr
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



    public class BillPrinter
    {
        public string QueryType { get; set; }
        public string LogNo { get; set; }
        public string PrinterRegNo { get; set; }
        public string BoatHouseId { get; set; }
        public string BookingId { get; set; }
        public string ComputerName { get; set; }
        public string Job_Status { get; set; }

    }

    public class BillPrinterlst
    {
        public List<BillPrinter> Response
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

    public class BillPrinterStr
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

    public class RePrints
    {
        public string QueryType { get; set; }
        public string BoatHouseId { get; set; }
        public string BookingId { get; set; }
        public string WebKey { get; set; }

    }

    public class RePrintslst
    {
        public List<RePrints> Response
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

    public class RePrintsStr
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

    public class CashReport
    {
        public string BoatHouseId
        {
            get;
            set;
        }

        public string FromDate
        {
            get;
            set;
        }
        public string CashflowTypes
        {
            get;
            set;
        }

        public string Particulars
        {
            get;
            set;
        }
        public string Amount
        {
            get;
            set;
        }
        public string RequestedBy
        {
            get;
            set;
        }
        
    }


    public class CashReportlst
    {
        public List<CashReport> Response
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
    public class CashReportStr
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





    //************************ BookingSlotSummaryDetails ************************//
    public class BookingSlotSummaryDetails
    {

        public string BookingDate
        {
            get;
            set;
        }

        public string SlotId
        {
            get;
            set;
        }
        public string BoatTypeId
        {
            get;
            set;
        }
        public string BoatSeaterId
        {
            get;
            set;
        }
        public string TripCount
        {
            get;
            set;
        }
        public string BoatCount
        {
            get;
            set;
        }

        public string BookingStatus
        {
            get;
            set;
        }

        public string[] BookingType
        {
            get;
            set;
        }

        public string BoatHouseId
        {
            get;
            
            set;
        }

        public string BookingUser
        {
            get;
            set;
        }


        public int[] SlotIds { get; set; }
        public int[] BoatTypeIds { get; set; }

        public int[] BoatSeaterIds { get; set; }
        public int[] TripCountIds { get; set; }
        public int[] BookingTypeIds { get; set; }
        
        public int[] BookingBlockId { get; set; }

    }
    public class BookingSlotSummaryDetailsRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BookingSlotSummaryDetailsResNew
    {
        public List<string> Response { get; set; }
        public int StatusCode { get; set; }
    }

    //public class BookingSlotSummaryDetailsResNew
    //{
    //    public DataTable Response { get; set; }
    //    public int StatusCode { get; set; }
    //}

    public class BookingSlotSummaryDetailsList
    {
        public List<AppCarousel> Response { get; set; }
        public int StatusCode { get; set; }
    }
}