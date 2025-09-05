using NDCWeb.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CourseRegister
    {
        [Key]
        public int CourseRegisterId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public DateTime DOBirth { get; set; }
        public DateTime? DOCommissioning { get; set; }
        public string SeniorityYear { get; set; }

        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string WhatsappNo { get; set; }

        public string Honour { get; set; }
        public string NICName { get; set; }
        public string ApptDesignation { get; set; }
        public string ApptOrganisation { get; set; }
        public string ApptLocation { get; set; }

        public bool Approved { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? VerifyDate { get; set; }
        public string UserId { get; set; }
        public string Branch { get; set; }
        public int RankId { get; set; }
        public virtual RankMaster Ranks { get; set; }

        public int? CourseId { get; set; }
        public virtual Course Courses { get; set; }
    }
}