using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{

    public class Circular : BaseEntity
    {
        public Circular()
        {
            iCircularMedias = new List<CircularMedia>();
        }

        [Key]
        public int CircularId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public virtual ICollection<CircularMedia> iCircularMedias { get; set; }
        public virtual ICollection<CircularDetail> CircularDetails { get; set; }
    }
}