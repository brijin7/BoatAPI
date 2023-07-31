using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FoodMaster
    {
        public string QueryType
        {
            get;
            set;
        }
        public int FoodId
        {
            get;
            set;
        }
        public string FoodName
        {
            get;
            set;
        }
        public string FoodDescription
        {
            get;
            set;
        }

        public string FoodImageLink
        {
            get;
            set;
        }
        public string VegNonVeg
        {
            get;
            set;
        }
        public string FoodCity { get; set; }


        public string CreatedBy
        {
            get;
            set;
        }
        public string FoodType { get; set; }
        public string CityName { get; set; }
        public string ActiveStatus { get; set; }

    }

    public class FoodMasterList
    {
        public List<FoodMaster> Response
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
    public class FoodMasterString
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