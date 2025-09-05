using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ArrivalBaggage
    {
        public int ArrivalBaggageId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string TranspotationMode { get; set; }
        public string Assistance { get; set; }
    }
}