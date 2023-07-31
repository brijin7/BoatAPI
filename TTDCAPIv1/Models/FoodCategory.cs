using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FoodCategory
    {
        public string CreatedBy
        {
            get; set;
        }
        public string QueryType { get; set; }
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
        public string ActiveStatus
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

            
    }

    public class FoodCategoryList
    {
        public List<FoodCategory> Response
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
    public class FoodCategoryString
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