using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Speaker : BaseEntity
    {
        [Key]
        public int SpeakerId { get; set; }
        public string FullName { get; set; }
        //public string NickName { get; set; }
        public Nullable<DateTime> SpeachDate { get; set; }
        public string Email { get; set; }
        public string AlternateEmail { get; set; }
        public string MobileNo { get; set; }
        public string Telephone { get; set; }
        public string CurrentAddress { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public int TopicId { get; set; }
        public virtual TopicMaster Topics { get; set; }
        //public int SubjectId { get; set; }
        //public virtual SubjectMaster Subjects { get; set; }

    }
}