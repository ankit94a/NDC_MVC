using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ArrivalAccompanied : BaseEntity
    {
        [Key]
        public int ArrivalMemId { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public string Relation { get; set; }
        public string Remarks { get; set; }

        public int ArrivalId { get; set; }
        public virtual ArrivalDetail ArrivalDetails { get; set; }
    }
}