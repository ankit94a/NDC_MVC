using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class TADAClaimsVM
    {
        [Key]
        [Required(ErrorMessage = "TADA Id Not Supplied")]
        [Display(Name = "TADA Id")]
        public int TADAId { get; set; }

        [Required(ErrorMessage = "CDA AC No Not Supplied")]
        [Display(Name = "CDA AC No")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string CDAACNo { get; set; }

        [Required(ErrorMessage = "Basic Pay Not Supplied")]
        [Display(Name = "Basic Pay")]
        [RegularExpression(@"^[a-zA-Z0-9- ]*$", ErrorMessage = "Special chars not allowed")]
        public string BasicPay { get; set; }

        [Required(ErrorMessage = "MSP Not Supplied")]
        [Display(Name = "MSP")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Special chars not allowed")]
        public string MSP { get; set; }

        [Required(ErrorMessage = "Pay Level Supplied")]
        [Display(Name = "Pay Level")]
        [RegularExpression(@"^[a-zA-Z0-9-]*$", ErrorMessage = "Special chars not allowed")]
        public string PayLevel { get; set; }

        [Required(ErrorMessage = "Mobile No Not Supplied")]
        [Display(Name = "Mobile No")]
        
        [RegularExpression(@"^([0-9]{10,14})$", ErrorMessage = "Invalid Mobile Number.")]
        
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Bankers Name & Address Not Supplied")]
        [Display(Name = "Banker Name & Add with A/C No")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string BankNameAddress { get; set; }

        [Required(ErrorMessage = "Pay AC Offc Add Not Supplied")]
        [Display(Name = "Pay A/C Offc Add")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string PayACOffcAddress { get; set; }
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Special chars not allowed")]
        public string FullName { get; set; }

    }
    public class TADAClaimsIndexVM: TADAClaimsVM
    {
       
    }
    public class TADAClaimsCrtVM : TADAClaimsVM
    {
      

        
    }
    public class TADAClaimsUpVM : TADAClaimsVM
    {
      
    }
}