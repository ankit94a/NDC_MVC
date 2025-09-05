using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrLanguage : BaseEntity
    {
        [Key]
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
        public string Qualification { get; set; }

        //public int CourseMemberId { get; set; }
        //public virtual CourseMember CourseMembers { get; set; }
    }
}