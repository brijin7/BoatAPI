using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class UserProfile
    {
        public string QueryType { get; set; }
        public string UserId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MailId { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public string EmpDOJ { get; set; }
        public string Aadhaar { get; set; }
        public string UserRoleId { get; set; }
        public string Photo { get; set; }
        public string PromoNotification { get; set; }

        public string Gender { get; set; }
        public string DOB { get; set; }
        public string MaritalStatus { get; set; }
        public string DOM { get; set; }

        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string SupportUser { get; set; }

        public string PaymentStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UniqueId { get; set; }
        
    }
    public class UserProfileList
    {
        public List<UserProfile> Response
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
    public class UserProfileRes
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