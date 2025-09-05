using System;
using System.ComponentModel.DataAnnotations;

namespace NDCWeb.Models
{
    public class Visitor
    {
        [Key]
        public int VisitorId { get; set; }
        public int MenuId { get; set; }
        public string Slug { get; set; }
        public string IpAddress { get; set; }
        public DateTime VisitDate { get; set; }
        public string Remarks { get; set; }
    }
}