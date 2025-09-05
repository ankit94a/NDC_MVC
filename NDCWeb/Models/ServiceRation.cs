using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ServiceRation : BaseEntity
    {
        [Key]
        public int RationId { get; set; }
        public string PersonalNo { get; set; }
        public string Eater { get; set; }
        public string LRC { get; set; }
    }
}