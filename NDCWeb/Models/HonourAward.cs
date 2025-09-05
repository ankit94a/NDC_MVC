using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class HonourAward
    {
        [Key]
        public int HonourId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Year { get; set; }
        public string Decoration { get; set; }

        public int CourseMemberId { get; set; }
        public virtual CourseMember CourseMembers { get; set; }
    }
}