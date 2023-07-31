using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class PrintingInstruction
    {
        public string QueryType { get; set; }
        public string UniqueId { get; set; }
        public string ServiceType { get; set; }
        public string ServiceName { get; set; }
        public string InstructionDtl { get; set; }
        public string BoatHouseId { get; set; }
        public string BoatHouseName { get; set; }
        public string ActiveStatus { get; set; }
        public string Createdby { get; set; }
        public string CreatedDate { get; set; }
        public string Updatedby { get; set; }
        public string UpdatedDate { get; set; }
    }
    public class PrintingInstructionres
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class PrintingInstructionList
    {
        public List<PrintingInstruction> Response { get; set; }
        public int StatusCode { get; set; }
    }

}