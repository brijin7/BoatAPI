using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class FA_VendorMaster
    {

        public string QueryType { get; set; }
        public string CorpId { get; set; }
        public string VendorId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PointOfContact { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }
    public class FA_VendorMstrRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class FA_VendorMstrList
    {
        public List<FA_VendorMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}