using System;
using System.ComponentModel.DataAnnotations;

namespace NDCWeb.Models
{
    public class BaseEntity
    {
        [ScaffoldColumn(false)]
        public DateTime? CreatedAt { get; set; }

        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? LastUpdatedAt { get; set; }

        [ScaffoldColumn(false)]
        public string LastUpdatedBy { get; set; }
    }
    public class BaseEntityVM
    {
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? LastUpdatedAt { get; set; }

        public string LastUpdatedBy { get; set; }
    }
}