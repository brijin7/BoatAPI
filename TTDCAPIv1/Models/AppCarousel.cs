using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTDCAPIv1.Models
{
    public class AppCarousel
    {
        public string QueryType { get; set; }
        public int CarouselId { get; set; }
        public int CorpID { get; set; }
        public string AppName { get; set; }
        public string Carousel { get; set; }
        public string Createdby { get; set; }
        public string CorpName { get; set; }
        public string ActiveStatus { get; set; }

    }
    public class AppCarouselRes
    {
        public string Response { get; set; }
        public int StatusCode { get; set; }
    }

    public class AppCarouselList
    {
        public List<AppCarousel> Response { get; set; }
        public int StatusCode { get; set; }
    }

}