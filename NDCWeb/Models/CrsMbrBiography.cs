using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class CrsMbrBiography : BaseEntity
    {
        [Key]
        public int BiographyId { get; set; }
        public string PenPicture { get; set; }
        public string FamilyBackground { get; set; }
        public string EarlySchooling { get; set; }
        public string AcademicAchievement { get; set; }
        public string PersonalValueSystem { get; set; }

    }
}