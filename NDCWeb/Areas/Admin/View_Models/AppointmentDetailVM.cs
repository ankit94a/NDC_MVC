using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class AppointmentDetailVM : BaseEntityVM
    {
        [Required(ErrorMessage = "Please Enter Appointment Id")]
        [Display(Name = "Appointment Id")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Please Enter Appointment")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Appointment")]
        public string Appointment { get; set; }

        [Required(ErrorMessage = "Please Enter Organisation")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Organisation")]
        public string Organisation { get; set; }
    }
    public class AppointmentDetailIndxVM : AppointmentDetailVM
    {
    }
    public class AppointmentDetailCrtVM: AppointmentDetailVM
    {
    }
    public class AppointmentDetailUpVM : AppointmentDetailVM
    {
    }
}