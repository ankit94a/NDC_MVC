using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class InfoTechVM
    {
        [Key]
        [Display(Name = "Id")]
        public int ITId { get; set; }

        [Display(Name = "Are you in possession of official laptop from previous Department/Organisation")]
        public bool LaptopFromDepartment { get; set; }

        //[Required(ErrorMessage = "Enter Laptop Make")]
        [Display(Name = "Laptop Make")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string DLaptopMake { get; set; }

        //[Required(ErrorMessage = "Enter Model No")]
        [Display(Name = "Model No")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string DModelNo { get; set; }

        //[Required(ErrorMessage = "Enter Sl No")]
        [Display(Name = "Sl No")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string DSlNo { get; set; }

        [Display(Name = "Adapter available")]
        public bool DAdaptor { get; set; }

        [Display(Name = "Bag available")]
        public bool DBag { get; set; }

        [Display(Name = "Laptop from college")]
        public bool LaptopFromCollege { get; set; }

        [Display(Name = "Laptop Make")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string CLaptopMake { get; set; }

        [Display(Name = "Model No")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string CModelNo { get; set; }

        [Display(Name = "SlNo")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string CSlNo { get; set; }

        [Display(Name = "Adapter available")]
        public bool CAdaptor { get; set; }

        [Display(Name = "Bag available")]
        public bool CBag { get; set; }

        #region File Path
        public string DeclarationFormDocPath { get; set; }
        public string InsuranceReceiptDocPath { get; set; }
        public string OutsidePermissionDocPath { get; set; }
        #endregion
    }
    public class InfoTechIndxVM : InfoTechVM
    {
    }
    public class InfoTechCrtVM : InfoTechVM
    {
    }
    public class InfoTechUpVM : InfoTechVM
    {
    }
    public class InfoTechCompleteVM : InfoTechVM
    {
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberFullName { get; set; }
        
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string LockerNo { get; set; }
    }
    public class InfoTechACKVM : InfoTechCrtVM
    {

        #region File Path
        public bool IsDeclarationFormDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(DeclarationFormDocPath) || DeclarationFormDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        public bool IsInsuranceReceiptDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(InsuranceReceiptDocPath) || InsuranceReceiptDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        public bool IsOutsidePermissionDocPath
        {
            get
            {
                if (string.IsNullOrEmpty(OutsidePermissionDocPath) || OutsidePermissionDocPath == "#")
                    return false;
                else
                    return true;
            }
        }
        #endregion
    }
}