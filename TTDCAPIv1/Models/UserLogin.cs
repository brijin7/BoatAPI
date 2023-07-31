using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class UserLogin
    {
        public string UserId { get; set; }
        public string EmpId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string RewardUtilized { get; set; }
        public string PromoNotification { get; set; }
        public string ActiveStatus { get; set; }
        public string MobAppAccess { get; set; }
        public string UserType { get; set; }
        public string UserRoleId { get; set; }
        public string UserRole { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string OfflineRights { get; set; }
        public string QueryType { get; set; }
        public string SupportUser { get; set; }

        public string Designation { get; set; }
        public string DesignationId { get; set; }
        public string CorpId { get; set; }
    }

    public class UserLoginRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class UserLoginList
    {
        public List<UserLogin> Response { get; set; }
        public int StatusCode { get; set; }
    }
}