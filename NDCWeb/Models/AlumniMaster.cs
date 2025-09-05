using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class AlumniMaster : BaseEntity
    {
        [Key]
        public int AluminiId { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string CourseSerNo { get; set; }
        public string CourseYear { get; set; }
        public string NdcEqvCourse { get; set; }
        public string YearDone { get; set; }

        public string ServiceRetd { get; set; }
        public string ServiceId { get; set; }
        public string ServiceNo { get; set; }
       
        public string Decoration { get; set; }
        public string PermanentAddress { get; set; }
        public string NdcCommunicationAddress { get; set; }
        public string Email { get; set; }
        public string LandlineNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public string Spouse { get; set; }
        public string AlumniPhoto { get; set; }
        public bool Verified { get; set; }
        public bool IsDelete { get; set; }
        public string Appointment { get; set; }
        public string Country { get; set; }
        public string ServiceRank { get; set; }
        public string OtherService { get; set; }
        public string IsProminent { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? VerifyDate { get; set; }
        public string Branch { get; set; }        
        public string OtherInfo { get; set; }
        public string UserId { get; set; }
        public int? InStepCourseId { get; set; }

        //public int? RankId { get; set; }
        //public virtual RankMaster Ranks { get; set; }
    }
}