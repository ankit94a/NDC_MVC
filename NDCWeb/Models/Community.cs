using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Community : BaseEntity
    {
        [Key]
        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string DesignationIdDescription { get; set; }
    }
}