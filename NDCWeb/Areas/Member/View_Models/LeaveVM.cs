using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class LeaveVM
    {
        [Key]
        [Required(ErrorMessage = "Leave Id Not Supplied")]
        [Display(Name = "Leave Id")]
        public int LeaveId { get; set; }

        //[Required(ErrorMessage = "Member Id Not Supplied")]
        //[Display(Name = "Member Id")]
        //public int MemberId { get; set; }

        [Required(ErrorMessage = "Leave Category Not Supplied")]
        [Display(Name = "Leave For")]
        public string LeaveCategory { get; set; }

		[Required(ErrorMessage = "Leave In Not Supplied")]
		[Display(Name = "Leave In")]
		public string LeaveIn { get; set; }

		[Required(ErrorMessage = "Leave Type Not Supplied")]
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }

        [Required(ErrorMessage = "From Date Not Supplied")]
        [Display(Name = "From Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To Date Not Supplied")]
        [Display(Name = "To Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ToDate { get; set; }

        [Display(Name = "Prefix From Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PrefixDate { get; set; }

        [Display(Name = "Prefix To Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PrefixToDate { get; set; }

        [Display(Name = "Suffix From Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuffixDate { get; set; }

        [Display(Name = "Suffix To Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SuffixToDate { get; set; }

        [Required(ErrorMessage = "Total Days Not Supplied")]
        [Display(Name = "Total Days")]
        public int TotalDays { get; set; }

        [Required(ErrorMessage = "Reason For Leave Not Supplied")]
        [Display(Name = "Reason For Leave")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string ReasonForLeave { get; set; }

        [Required(ErrorMessage = "Address On Leave Not Supplied")]
        [Display(Name = "Address On Leave")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string AddressOnLeave { get; set; }

        [Required(ErrorMessage = "Telephone No Not Supplied")]
        [Display(Name = "Telephone No")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Special chars not allowed")]
        //[MaxLength(14, ErrorMessage = "Invalid Tele No.")]
        //[MinLength(10, ErrorMessage = "Invalid Tele No.")]
        //[RegularExpression(@"^([0-9])$", ErrorMessage = "Invalid Tele No.")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string TeleNo { get; set; }

        [Display(Name = "Recommend By Embassy")]
        public string RecommendByEmbassy { get; set; }

        
        public string AQStatus { get; set; }
        public DateTime? AQStatusDate { get; set; }
        public string IAGStatus { get; set; }
        public DateTime? IAGStatusDate { get; set; }
        public string ServiceSDSStatus { get; set; }
        public DateTime? ServiceSDSStatusDate { get; set; }
        public string SecretaryStatus { get; set; }
        public DateTime? SecretaryStatusDate { get; set; }
        public string ComdtStatus { get; set; }
        public DateTime? ComdtStatusDate { get; set; }

        [Display(Name = "Document")]
        public string DocPath { get; set; }

        [Display(Name = "Country Id")]
        public int? CountryId { get; set; }
        public virtual CountryMaster Country { get; set; }
		[Required(ErrorMessage = "Leave Duration Not Supplied")]
		public string LeaveDuration { get; set; }
	}
    
    public class LeaveIndexVM : LeaveVM
    {
        public bool BtnLeaveCertificate { get; set; }
    }
    public class LeaveCrtVM : LeaveVM
    {
        
    }
    public class LeaveUpVM : LeaveVM
    {

    }
    public class LeaveForeignOffrVM : LeaveVM
    {

    }
}