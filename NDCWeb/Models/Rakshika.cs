using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Rakshika : BaseEntity
    {
        [Key]
        public int RakshikaId { get; set; }
        public string SpouseName { get; set; }
        public string CourseMemberName { get; set; }
        public string SpouseNickName { get; set; }
        public DateTime DOBirth { get; set; }
        public string Qualification { get; set; }
    }
}