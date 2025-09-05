using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class MPhilPostGraduateVM
    {
        [Key]
        public int PostGraduateId { get; set; }
        public string RegnNo { get; set; }
        public string MonthYearPass { get; set; }
        public string PaperTitle { get; set; }
        public string AwardedIA { get; set; }
        public string AwardedUE { get; set; }
        public string MaxIA { get; set; }
        public string MaxUE { get; set; }

        public int MPhilId { get; set; }
        public virtual MPhilMember MPhilMembers { get; set; }
    }
    public class MPhilPostGraduateIndxVM : MPhilPostGraduateVM
    {

    }
}