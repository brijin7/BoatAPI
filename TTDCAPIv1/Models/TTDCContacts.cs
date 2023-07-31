using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{

    public class TTDCContacts
    {
        public string QueryType { get; set; }
        public string ContactId { get; set; }
        public string ContactTypeId { get; set; }
        public string ContactName { get; set; }
        public string ContactTypeName { get; set; }
        public string Designation { get; set; }
        public string ContactInfo { get; set; }
        public string CreatedBy { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class TTDCContactsRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class TTDCContactsList
    {
        public List<TTDCContacts> Response { get; set; }
        public int StatusCode { get; set; }
    }

}