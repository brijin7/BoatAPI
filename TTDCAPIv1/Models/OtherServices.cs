using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class OtherServices
    {
        public string QueryType
        {
            get;
            set;
        }
        public string Category
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

        public string ServiceType
        {
            get;
            set;
        }

    }


    public class OtherServicesList
    {
        public List<OtherServices> Response
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
    public class OtherServicesString
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