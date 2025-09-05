using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class RankMasterVM : BaseEntityVM
    {
        [Required(ErrorMessage = "Please Enter Id")]
        [Display(Name = "Id")]
        public int RankId { get; set; }

        [Required(ErrorMessage = "Please Enter Rank")]
        [Display(Name = "Rank")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string RankName { get; set; }

        [Required(ErrorMessage = "Please Enter Seniority")]
        [Display(Name = "Seniority")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        public Nullable<decimal> Seniority { get; set; }

        [Required(ErrorMessage = "Select Service")]
        [Display(Name = "Service")]
        public string Service { get; set; }
    }
    public class RankMasterIndxVM : RankMasterVM
    {
    }
    public class RankMasterCrtVM : RankMasterVM
    {
        public bool ForParticipant { get; set; }
    }
    public class RankMasterUpVM : RankMasterVM
    {
        public bool ForParticipant { get; set; }
    }
}