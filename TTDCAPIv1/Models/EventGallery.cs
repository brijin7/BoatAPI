using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class EventGallery
    {
        public int EventId { get; set; }
        public int GalleryId { get; set; }
        public string QueryType { get; set; }

        public string EventImageLink { get; set; }
        public string EventName { get; set; }

        public string ActiveStatus { get; set; }
        public string CreatedBy { get; set; }
    }

    public class EventGalleryRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class EventGalleryList
    {
        public List<EventGallery> Response { get; set; }
        public int StatusCode { get; set; }
    }
}