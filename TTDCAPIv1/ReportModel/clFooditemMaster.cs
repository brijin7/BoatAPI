using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.ReportModel
{
    public class clFooditemMaster
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServiceTotalAmount { get; set; }
        public int BoatHouseId { get; set; }
        public string CategoryId { get; set; }

    }
    public class clBookingRestaurant
    {
        public int NoOfItems { get; set; }
        public decimal ChargePerItem { get; set; }
        public string BookingId { get; set; }
        public int BoatHouseId { get; set; }
      
        public int ServiceId { get; set; }

    }
    public class clFoodCategory
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BoatHouseId { get; set; }

    } 
    public class TaxCal
    {
        public string BookingId { get; set; }
        public int BoatHouseId { get; set; }
        public int ServiceId { get; set; }
        public decimal TaxAmt { get; set; }
    }
    public class CategoryReport
    {
        public int Sno { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ItemRate { get; set; }
        public int Quantity { get; set; }
        public decimal Charge { get; set; }

        public decimal TaxAmount { get; set; }
        public decimal Total  { get; set; }
        public decimal OrderValue { get; set; }
        public string BookingId { get; set; }
        public int BoatHouseId { get; set; }
    }
}