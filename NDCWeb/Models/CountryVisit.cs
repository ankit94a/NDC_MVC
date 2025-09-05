using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CountryVisit : BaseEntity
    {
        [Key]
        public int CountryVisitId { get; set; }
        public string Country { get; set; }
        public string Visit { get; set; }
        public string Duration { get; set; }
        public string Purpose { get; set; }
        //public string Month { get; set; }

        //public int CourseMemberId { get; set; }
        //public virtual CourseMember CourseMembers { get; set; }
    }
}