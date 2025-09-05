using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.View_Models
{
    public class HomeVM
    {
    }
    public class PageContentSearchVM
    {
        public int PageContentId { get; set; }
        public List<string> Content { get; set; }
        public int MenuId { get; set; }
    }
    #region WorkWithUs
    public class WorkWithUsVM : BaseEntityVM
    {
        [Key]
        public int WWUId { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Contact No")]
        [Display(Name = "Contact No")]
        public long MobileNo { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }
    }
    public class WorkWithUsIndxVM : WorkWithUsVM
    {
    }
    #endregion
}