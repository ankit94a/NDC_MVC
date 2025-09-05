using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Accomodation:BaseEntity
    {
        public Accomodation()
        {
            iAccomodationMedias = new List<AccomodationMedia>();
        }
        [Key]
        public int AccomodationId { get; set; }
        public string MaritalStatus { get; set; }
        public string AccomReq { get; set; }
        public string ArrangeType { get; set; }
        public Nullable<DateTime> AccomodationDate { get; set; }
        public Nullable<DateTime> DateOfseniority { get; set; }
        public string PriorityFirst { get; set; }
        public string PrioritySecond { get; set; }
        public string SpecialRequest { get; set; }
        //Added on 28 Nov 2023 by CP on order of GSO Systems
        public bool AnySpecialRequest { get; set; }
        public string SpecialRequestWithReason { get; set; }
        public string SignatureDoc { get; set; }
        public virtual ICollection<AccomodationMedia> iAccomodationMedias { get; set; }

    }
}