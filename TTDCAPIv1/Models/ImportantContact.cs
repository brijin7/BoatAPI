using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ImportantContact
    {
        public string QueryType { get; set; }
        public int ContactId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int ContactTypeId { get; set; }
        public string ContactTypeName { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class ImportantContactRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ImportantContactList
    {
        public List<ImportantContact> Response { get; set; }
        public int StatusCode { get; set; }
    }
}