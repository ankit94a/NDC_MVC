using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class LibraryMembershipVM
    {
        [Key]
        [Required(ErrorMessage = "LibraryMembershipId is Required")]
        [Display(Name = "LibraryMembershipId")]
        public int LibraryMembershipId { get; set; }

        [Required(ErrorMessage = "Locker No is Required")]
        [Display(Name = "Locker No")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string LockerNo { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [Display(Name = "Name (First name & last name)")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberName { get; set; }

        [Required(ErrorMessage = "Designation is Required")]
        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        [Display(Name = "Address (For Communication)")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Mobile No")]
        [Display(Name = "Mobile No")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        // [RegularExpression(@"^([0-9]{15})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Permanent E-mail Address")]
        [Display(Name = "Permanent E-mail Address")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid e-mail adress")]
        public string EmailId { get; set; }
    }
    public class LibraryMembershipCrtVM : LibraryMembershipVM
    {
    }
    public class LibraryMembershipUpVM : LibraryMembershipVM
    {
    }
    public class LibraryMembershipIndxVM : LibraryMembershipVM
    {
    }
}