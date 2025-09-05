using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class SiteFeedback : BaseEntity
    {
        [Key]
        public int FeedbackId { get; set; }
        public string DepartmentSubject { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Comment { get; set; }
        public bool Approved { get; set; }
    }
}