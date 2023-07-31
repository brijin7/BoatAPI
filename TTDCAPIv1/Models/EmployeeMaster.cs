using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class EmployeeMaster
    {
        public string QueryType { get; set; }
        public string UserId { get; set; }
        public string EmpId { get; set; }
        public string ConfigId { get; set; }
        public string EmpType { get; set; }
        public string EmpTypeName { get; set; }
        public string EmpName { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpDesignation { get; set; }
        public string EmpDesignationName { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string EmpMobileNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string EmpDOJ { get; set; }
        public string EmpAadharId { get; set; }
        public string EmpMailId { get; set; }
        public string EmpPhotoLink { get; set; }
        public string ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string MobileAppAccess { get; set; }
        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
        public string UserType { get; set; }

        public string MMaster { get; set; }
        public string MBoating { get; set; }
        public string MTour { get; set; }
        public string MHotel { get; set; }
        public string MFixedAssets { get; set; }

    }


    public class EmployeeMasterRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class EmployeeMasterList
    {
        public List<EmployeeMaster> Response { get; set; }
        public int StatusCode { get; set; }
    }
}