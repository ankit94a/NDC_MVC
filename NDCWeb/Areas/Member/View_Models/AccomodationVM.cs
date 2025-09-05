using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class AccomodationVM
    {
        [Key]
        [Required(ErrorMessage = "Accomodation Id Not Supplied")]
        [Display(Name = "Accomodation Id")]
        public int AccomodationId { get; set; }

        [Required(ErrorMessage = "Accomodation Required Type Not Supplied")]
        [Display(Name = "Married Accn Required (choose one)")]
        public string AccomReq { get; set; }

        [Required(ErrorMessage = "Marital Status Required")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }

        [Display(Name = "Accn Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> AccomodationDate { get; set; }

        [Display(Name = "Date of Seniority")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DateOfseniority { get; set; }

        [Required(ErrorMessage = "Arrangement Type Not Supplied")]
        [Display(Name = "Retaining Accn/Own Arrangement")]
        public string ArrangeType { get; set; }

        [Required(ErrorMessage = "Preference Not Supplied")]
        [Display(Name = "a. Preference of Accn, Priority 1")]
        public string PriorityFirst { get; set; }

        [Required(ErrorMessage = "Preference Not Supplied")]
        [Display(Name = "b. Preference of Accn, Priority 2")]
        [RegularExpression(@"^[a-zA-Z0-9,. ]*$", ErrorMessage = "Special chars not allowed")]
        public string PrioritySecond { get; set; }

        [Display(Name = "c. Special Request (1st Floor/Ground Floor)")]
        public string SpecialRequest { get; set; }

        [Required(ErrorMessage = "Signature Not Supplied")]
        [Display(Name = "Signature")]
        public string SignatureDoc { get; set; }
        //Added on 28 Nov 2023 by CP
        [Display(Name = "Any special request ?")]
        public bool AnySpecialRequest { get; set; }

        [Display(Name = "Special request with reason")]
        public string SpecialRequestWithReason { get; set; }

       
    }
    public class AccomodationIndexVM : AccomodationVM
    {
        public AccomodationIndexVM()
        {
            iAccomodationMedias = new List<AccomodationMedia>();
        }
        public virtual ICollection<AccomodationMedia> iAccomodationMedias { get; set; }
    }
    public class AccomodationCrtVM : AccomodationVM
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public AccomodationCrtVM()
        {
            iAccomodationMedias = new List<AccomodationMedia>();
        }
        public virtual ICollection<AccomodationMedia> iAccomodationMedias { get; set; }
    }
    public class AccomodationUpVM : AccomodationVM
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public AccomodationUpVM()
        {
            iAccomodationMedias = new List<AccomodationMedia>();
        }
        public virtual ICollection<AccomodationMedia> iAccomodationMedias { get; set; }
    }
}