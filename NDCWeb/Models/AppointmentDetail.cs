using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class AppointmentDetail : BaseEntity
    {
        [Key]
        public int AppointmentId { get; set; }
        public string Appointment { get; set; }
        public string Organisation { get; set; }
    }
}