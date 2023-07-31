using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class CompanyMaster
    {
        public string QueryType { get; set; }
        public string CorpID { get; set; }
        public string CorpName { get; set; }
        public string ShortName { get; set; }
        public string CorpLogo { get; set; }
        public string CorpLogo1 { get; set; }
        public string AppName { get; set; }
        public string CorpPhoto { get; set; }
        public string CorpPhoto1 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string MailId { get; set; }
        public string CountryName { get; set; }
    }
    public class CompanyMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class CompanyMasterList
    {
        public List<CompanyMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }


}