using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrHobby : BaseEntity
    {
        [Key]
        public int HobbyId { get; set; }
        public string Hobby { get; set; }

        //public int CourseMemberId { get; set; }
        //public virtual CourseMember CourseMembers { get; set; }
    }
}