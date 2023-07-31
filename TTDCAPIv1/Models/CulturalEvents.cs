using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class CulturalEvents
    {
        public string EventId { get; set; }
        public string QueryType { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventImageLink { get; set; }
        public string EventType { get; set; }
        public string EventCity { get; set; }
        public string CreatedBy { get; set; }
        public string EventTypeName { get; set; }
        public string EventCityName { get; set; }
        public string CityName { get; set; }
        public string ActiveStatus { get; set; }
    }
    public class CulturalEventsRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class CulturalEventsList
    {
        public List<CulturalEvents> Response { get; set; }
        public int StatusCode { get; set; }
    }
}