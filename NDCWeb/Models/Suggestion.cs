using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class Suggestion: BaseEntity
    {
        [Key]
        public int SuggestionId { get; set; }
        public string Description { get; set; }
        public string SuggestionType { get; set; }
        public string Reply { get; set; }
        public string Status { get; set; }
        public int StaffId { get; set; }
        public virtual StaffMaster Staffs { get; set; }
    }
}