using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class ChangeBoatDetail
    {
        public string QueryType { get; set; }

        public string BookingId { get; set; }

        public string BookingDate { get; set; }

        public string BookingPin { get; set; }

        public string BoatReferenceNo { get; set; }
        public string PremiumStatus { get; set; }

        public string OldBoatTypeId { get; set; }

        public string OldBoatSeaterId { get; set; }

        public string OldBoatId { get; set; }

        public string OldBoatNum { get; set; }

        public string OldBoatCharge { get; set; }

        public string OldRowerCharge { get; set; }

        public string OldDeposit { get; set; }

        public string OldOfferAmount { get; set; }

        public string OldNetAmount { get; set; }

        public string OldTaxAmount { get; set; }

        public string NewBoatTypeId { get; set; }

        public string NewBoatSeaterId { get; set; }    

        public string NewBoatCharge { get; set; }

        public string NewRowerCharge { get; set; }

        public string NewDeposit { get; set; }

        public string NewNetAmount { get; set; }

        public string NewTaxAmount { get; set; }

        public string ExtraRefundAmount { get; set; }

        public string ExtraCharge { get; set; }

        public string BoatHouseId { get; set; }

        public string BoatHouseName { get; set; }

        public string UpdatedBy { get; set; }
        public string PaymentModeId { get; set; }

        public string PaymentModeType { get; set; }
    }
    public class ChangeBoatDetailRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class ChangeBoatDetailStringResList
    {
        public List<ChangeBoatDetail> Response { get; set; }
        public int StatusCode { get; set; }
    }
}
