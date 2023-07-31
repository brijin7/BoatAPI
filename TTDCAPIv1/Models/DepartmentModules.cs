using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class DepartmentModules
    {
        public string Id { get; set; }
        public string TypeId { get; set; }
        public string Name { get; set; }
        public string DeptId { get; set; }
    }

    public class DepartmentModulesRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class DepartmentModulesList
    {
        public List<DepartmentModules> Response { get; set; }
        public int StatusCode { get; set; }
    }
}