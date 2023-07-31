using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class TripSheetWeb
    {
        public string QueryType { get; set; }
        public string BookingId { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BoatId { get; set; }
        public string BoatNum { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatName { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string TripStartTime { get; set; }
        public string TripEndTime { get; set; }
        public string ActualBoatId { get; set; }

        public string BoatHouseName { get; set; }
        public string BookingPin { get; set; }
        public string ExpectedTime { get; set; }
        public string BookingSerial { get; set; }
        public string UserId { get; set; }
        public string BoardingTime { get; set; }
        public string PremiumStatus { get; set; }
        public string BookingDuration { get; set; }
        public string BookingMedia { get; set; }
        public string SelfDrive { get; set; }
        public string RowerCharge { get; set; }
        public string BarcodePin { get; set; }
        public string TraveledTime { get; set; }
        public string SSUserBy { get; set; }
        public string SDUserBy { get; set; }
        public string BoatDeposit { get; set; }
        public string DefaultEndTime { get; set; }
        public string BDtlPremiumStatus { get; set; }
        public string CountStart { get; set; }
        public string CountEnd { get; set; }
        public string RowNumber { get; set; }
    }
    public class TripSheetWebString
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class TripSheetWebList
    {
        public List<TripSheetWeb> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BoatTariffDetail
    {
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string WDBoatMinTotAmt { get; set; }

        public string WDRowerMinCharge { get; set; }
        public string WDBoatMinCharge { get; set; }
        public string WDBoatMinTaxAmt { get; set; }

        public string WEBoatMinTotAmt { get; set; }

        public string WERowerMinCharge { get; set; }
        public string WEBoatMinCharge { get; set; }
        public string WEBoatMinTaxAmt { get; set; }
        public string PerHeadApplicable { get; set; }
        public string IWDBoatMinTotAmt { get; set; }

        public string IWDRowerMinCharge { get; set; }
        public string IWDBoatMinCharge { get; set; }
        public string IWDBoatMinTaxAmt { get; set; }
        public string IWEBoatMinTotAmt { get; set; }
        public string IWERowerMinCharge { get; set; }
        public string IWEBoatMinCharge { get; set; }
        public string IWEBoatMinTaxAmt { get; set; }
        public string ECBoatPremTotAmt { get; set; }
        public string ECBoatPremMinCharge { get; set; }
        public string ECRowerPremMinCharge { get; set; }
        public string ECBoatPremTaxAmt { get; set; }
        public string DepositType { get; set; }
        public string Deposit { get; set; }              
    }

    public class BoatTariffDetailResponse
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class BoatTariffDetailList
    {
        public List<BoatTariffDetail> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class WeekDayClass
    {
        public string BoatHouseId { get; set; }
        public string WeekDays { get; set; }
        public string WeekDayDesc { get; set; }
        public string HolidayDate { get; set; }
        public string HolidayDesc { get; set; }       
    }

    public class WeekDayClassResponse
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }
    public class WeekDayClasslList
    {
        public List<WeekDayClass> Response { get; set; }
        public int StatusCode { get; set; }
    }
}