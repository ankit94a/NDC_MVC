using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class EventParticipant:BaseEntity
    {
        [Key]
        public int EventParticipantId { get; set; }
        public string ParticipateAs { get; set; }
        public bool AttendSelf { get; set; }
        public bool AttendSpouse { get; set; }
        public bool SecyPermited { get; set; }
        public bool DietaryPrefSelf { get; set; }
        public bool DietaryPrefSpouse { get; set; }
        public string LiquorPref { get; set; }
       public string Remarks { get; set; }
        public int EventId { get; set; }
        public virtual Event Events { get; set; }
    }
}