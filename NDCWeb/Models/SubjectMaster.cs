using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class SubjectMaster : BaseEntity
    {
        [Key]
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
    }
}