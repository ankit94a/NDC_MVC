using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ChildrenDetail
    {
        [Key]
        public int ChildId { get; set; }
        public string Name { get; set; }
        public DateTime DOBirth { get; set; }
        public string Occupation { get; set; }
        public bool LivingWithYou { get; set; }

        public string StayBySpouse { get; set; }
        public bool SpouseStay { get; set; }
    }
}