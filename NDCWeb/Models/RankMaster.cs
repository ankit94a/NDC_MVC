using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class RankMaster : BaseEntity
    {
        [Key]
        public int RankId { get; set; }
        public string RankName { get; set; }
        public Nullable<decimal> Seniority { get; set; }
        public string Service { get; set; }
        public bool ForParticipant { get; set; }
    }
}