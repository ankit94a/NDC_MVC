using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class AsgmtAppointment : BaseEntity
    {
        [Key]
        public int AsgmtAppointmentId { get; set; }
        public string Appointment { get; set; }
        public string Organisation { get; set; }
        public string Duration { get; set; }
        public string Location { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        //public int CourseMemberId { get; set; }
        //public virtual CourseMember CourseMembers { get; set; }
    }
}