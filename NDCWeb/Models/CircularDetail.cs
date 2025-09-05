using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CircularDetail : BaseEntity
    {
        [Key]
        public int CircularDetailId { get; set; }
        public int DesignationId { get; set; }
        public virtual Community Communitys { get; set; }
        public bool Show { get; set; }
        public int CircularId { get; set; }
        public virtual Circular Circulars { get; set; }
    }
}