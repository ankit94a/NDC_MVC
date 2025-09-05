using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class TelecommRequirementVM
    {
        [Key]
        [Required(ErrorMessage = "Telecomm Req Id Not Supplied")]
        [Display(Name = "Telecomm Req Id")]
        public int TelecommReqId { get; set; }
       
        [Required(ErrorMessage = "Quarter No Not Supplied")]
        [Display(Name = "Quarter No (Accommodation Form - Qtr Allotment. Post receipt activity is undertaken")]
        [RegularExpression(@"^[a-zA-Z0-9- ]*$", ErrorMessage = "Special chars not allowed")]
        public string HouseNo { get; set; }

        [Required(ErrorMessage = "Internet Requirment Not Supplied")]
        [Display(Name = "Do you require Internet at Residence")]
        public bool ReqInternet { get; set; }

        [Required(ErrorMessage = "Address where Internet is required")]
        [Display(Name = "Address where Internet is required")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string ResidentialComplex { get; set; }

        [Required(ErrorMessage = "Type of Connection Not Supplied")]
        [Display(Name = "Type of Connection required")]
        public string TypeOfConnection { get; set; }

        [Display(Name = "Remarks (if any)")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string Comments { get; set; }
    }
    public class TelecommRequirementIndexVM: TelecommRequirementVM
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
    public class TelecommRequirementCreateVM : TelecommRequirementVM
    {

    }
    public class TelecommRequirementUpdateVM : TelecommRequirementVM
    {

    }
}