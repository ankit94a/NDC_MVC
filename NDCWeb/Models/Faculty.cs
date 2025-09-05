using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Faculty : BaseEntity
    {
        [Key]
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public string Designation { get; set; }
        public string Type { get; set; }
        public string StaffType { get; set; }
    }
}