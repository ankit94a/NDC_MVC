using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class AlumniFeedback : BaseEntity
    {
        [Key]
        public int FeedbackId { get; set; }
        public string DepartmentSubject { get; set; }
        public string Comment { get; set; }
    }
}