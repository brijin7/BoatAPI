using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FoodItemMaster
    {
        public string QueryType
        {
            get;
            set;
        }
        public string CategoryId
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
        public string ServiceId
        {
            get;
            set;
        }
        public string ServiceName
        {
            get;
            set;
        }
        public string ShortName
        {
            get;
            set;
        }
        public string ServiceTotalAmount
        {
            get;
            set;
        }
        public string ChargePerItem
        {
            get;
            set;
        }
        public string ChargePerItemTax
        {
            get;
            set;
        }
        public string TaxID
        {
            get;
            set;
        }
        public string TaxName
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

        public string StockEntryMaintenance
        {
            get;
            set;
        }        
    }


    public class FoodItemMasterList
    {
        public List<FoodItemMaster> Response
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
    public class FoodItemMasterString
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


    public class RestStockEntry
    {
        public string QueryType { get; set; }
        public string StockId { get; set; }
        public string ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public string ItemNameId { get; set; }
        public string ItemName { get; set; }
        public string Date { get; set; }
        public string UOM { get; set; }
        public string Quantity { get; set; }
        public string Reference { get; set; }
        public string ActiveStatus { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CreatedBy { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CountStart { get; set; }
        public string RowNumber { get; set; }

    }
    public class RestStockEntryList
    {
        public List<RestStockEntry> Response
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
    public class RestStockEntryRes
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



    public class FoodItemMasterV2
    {

        public string QueryType
        {
            get;
            set;
        }
        public string CategoryId
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }
        public string ServiceId
        {
            get;
            set;
        }
        public string ServiceName
        {
            get;
            set;
        }
        public string ShortName
        {
            get;
            set;
        }
        public string ServiceTotalAmount
        {
            get;
            set;
        }
        public string ChargePerItem
        {
            get;
            set;
        }
        public string ChargePerItemTax
        {
            get;
            set;
        }
        public string TaxID
        {
            get;
            set;
        }
        public string TaxName
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

        public string StockEntryMaintenance
        {
            get;
            set;
        }

        public string CountStart
        {
            get;
            set;
        }

        public string RowNumber
        {
            get;
            set;
        }

    }


    public class FoodItemMasterListV2
    {
        public List<FoodItemMasterV2> Response
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
    public class FoodItemMasterStringV2
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


    public class RestStockEntryV2
    {
        public string QueryType { get; set; }
        public string StockId { get; set; }
        public string ItemCategoryId { get; set; }
        public string ItemCategory { get; set; }
        public string ItemNameId { get; set; }
        public string ItemName { get; set; }
        public string Date { get; set; }
        public string UOM { get; set; }
        public string Quantity { get; set; }
        public string Reference { get; set; }
        public string ActiveStatus { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string CreatedBy { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CountStart { get; set; }
        public string RowNumber { get; set; }

    }
    public class RestStockEntryListV2
    {
        public List<RestStockEntry> Response
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
    public class RestStockEntryResV2
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