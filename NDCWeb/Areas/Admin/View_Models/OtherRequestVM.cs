using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class OtherRequestVM
    {
        public int OtherRequestId { get; set; }

        [Required(ErrorMessage = "User Id Not Supplied")]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Mobile No. Not Supplied")]
        [Display(Name = "Mobile No.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Locker No. Not Supplied")]
        [Display(Name = "Locker No.")]
        public string LockerNo { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; } = false;

        [Required(ErrorMessage = "Remark Not Supplied")]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Remark Date")]
        public DateTime? RemarkDate { get; set; }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }

    }
    public class OtherRequestCrtVM
    {
        [Required(ErrorMessage = "User Id Not Supplied")]
        [RegularExpression(@"^[\w \.\,\?\;\:\""\''\[\]\!\@\#\$\%\&\*\(\)\-\=\+\\\/]*$", ErrorMessage = "This < >^| special chars not allowed for security reasons.")]
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Mobile No. Not Supplied")]
        [Display(Name = "Mobile No.")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only Number Accepted.")]
        public string MobileNo { get; set; }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }

        [NotMapped]
        public string VfyCaptcha { get; set; }

        [NotMapped]
        public string Usertype { get; set; }

        //[Required(ErrorMessage = "Locker No. Not Supplied")]
        [Display(Name = "Locker No.")]
        [RequiredIf(nameof(Usertype), "Member")]   
        [RegularExpression(@"^[0-9]{4,5}$", ErrorMessage = "Minimum 4 digits and Max 5 digits.")]
        //[System.Web.Mvc.Remote("CheckLocker", "Auth", ErrorMessage = "Locker No does not exists. Please enter the correct Locker No.")]
        public string LockerNo { get; set; }


        public int TotalStrength { get; set; }
    }
    public class OtherRequestUpdVM
    {
        public int OtherRequestId { get; set; }

        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "Mobile No.")]
        public string MobileNo { get; set; }

        [Display(Name = "Locker No.")]
        public string LockerNo { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; } = false;

        [Required(ErrorMessage = "Remark Not Supplied")]
        [Display(Name = "Remark")]
        public string Remark { get; set; }

        [Display(Name = "Remark Date")]
        public DateTime? RemarkDate { get; set; }

        [Display(Name = "Request Date")]
        public DateTime RequestDate { get; set; }
    }
}