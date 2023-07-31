using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class RowerChargers
    {
        public string BookingId { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BookingDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string ActualRowerCharge { get; set; }
        public string BookingPin { get; set; }

        public string BoatHouseId { get; set; }
    }
    public class RowerChargersList
    {
        public List<RowerChargers> Response
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
    public class RowerChargersString
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

    public class BoatingReport
    {
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string BookingDate { get; set; }
        public string BoatTypeId { get; set; }
        public string Account { get; set; }
        public string Amount { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string TaxableAmount { get; set; }
        public string Category { get; set; }
        public string PaymentType { get; set; }

        public string CreatedBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatType { get; set; }
        public string BoatSeater { get; set; }
        public string Count { get; set; }
        public string ServiceName { get; set; }
        public string BoatCharge { get; set; }
        public string BoatDeposit { get; set; }
    }


    public class BoatingReportres
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BoatingReportList
    {
        public List<BoatingReport> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BarCodes
    {
        public string BarCodeselected { get; set; }
        public string Count { get; set; }
        public string BookingDate { get; set; }
        public string Hour { get; set; }
        public string TotalCount { get; set; }
        public string Day { get; set; }
        public string Week { get; set; }
        public string MONTH_NAME { get; set; }
        public string InputCount { get; set; }
        public string Year { get; set; }
        public string Date { get; set; }
        public string BoatHouseId { get; set; }
    }
    public class BarCodesRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class BarChartsList
    {
        public List<BarCodes> Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class RowerChargersV2
    {
        public string BookingId { get; set; }
        public string BoatReferenceNo { get; set; }
        public string BookingDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BoatTypeId { get; set; }
        public string BoatType { get; set; }
        public string BoatSeaterId { get; set; }
        public string SeaterType { get; set; }
        public string RowerId { get; set; }
        public string RowerName { get; set; }
        public string ActualRowerCharge { get; set; }
        public string BookingPin { get; set; }

        public string BoatHouseId { get; set; }
        public string CountStart { get; set; }
        public string RowNumber { get; set; }
        public string SearchBy { get; set; }
    }
    public class RowerChargersV2List
    {
        public List<RowerChargersV2> Response
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
    public class RowerChargersV2String
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