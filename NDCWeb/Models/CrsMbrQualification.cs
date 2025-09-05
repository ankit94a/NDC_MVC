using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrQualification : BaseEntity
    {
        [Key]
        public int QualificationId { get; set; }
        public string Course { get; set; }
        public string QualificationType { get; set; }
        public string Year { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }

        //public int CourseMemberId { get; set; }
        //public virtual CourseMember CourseMembers { get; set; }
    }
}