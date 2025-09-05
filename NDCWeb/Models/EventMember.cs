using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class EventMember : BaseEntity
    {
        [Key]
        public int EventMemberId { get; set; }
        public string AttendType { get; set; }
        public string AttendSelf { get; set; }
        public string AttendSpouse { get; set; }

        //public bool SecyPermited { get; set; }

        public string DietPrefSelf { get; set; }
        public string DietPrefSpouse { get; set; }
        public string LiquorPref { get; set; }
        public string Remarks { get; set; }

        public int EventId { get; set; }
        public virtual Event Events { get; set; }
    }
}