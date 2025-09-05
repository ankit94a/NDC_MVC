using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrAppointment : BaseEntity
    {
        [Key]
        public int AppointmentId { get; set; }
        public string Designation { get; set; }
        public string Organisation { get; set; }
        public string Location { get; set; }

        public DateTime DOJoining { get; set; }
        public DateTime DOSeniority { get; set; }
        public string ServiceNo { get; set; }
        public string Service { get; set; }
        public string Branch { get; set; }

        #region Added as requested on 24/11/23
        //[Required(ErrorMessage = "Enter Holding Passport")]
        [Display(Name = "Are you working as DA/MA ?")]
        public string WorkingAsDAMA { get; set; }

        //[Required(ErrorMessage = "Enter Self Passport No")]
        [Display(Name = "Please mention details below.")]
        public string WorkingAsDAMADetails { get; set; }
        #endregion
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }
    }
}