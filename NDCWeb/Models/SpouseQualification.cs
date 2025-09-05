using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class SpouseQualification : BaseEntity
    {
        [Key]
        public int SpouseEduId { get; set; }
        public string ProfessionalEdu { get; set; }
        public string AcademicAchievement { get; set; }
        public string Division { get; set; }
        public string Institute { get; set; }

        public int SpouseId { get; set; }
        public virtual CrsMbrSpouse Spouses { get; set; }
    }
}