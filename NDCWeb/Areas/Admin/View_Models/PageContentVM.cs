using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class PageContentVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Page Content Id")]
        public int PageContentId { get; set; }
        
        [RegularExpression(@"^[\w,.!? \r\n]*$", ErrorMessage = "Special chars not allowed")]
        [Required(ErrorMessage = "Please Enter Content")]
        [AllowHtml]
        [Display(Name ="Contents")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Select Menu")]
        [Display(Name = "Menu")]
        public int MenuId { get; set; }
        public virtual MenuItemMaster MenuItemMasters { get; set; }
    }

    public class PageContentIndxVM : PageContentVM
    {
    }
    public class PageContentCrtVM : PageContentVM
    {
        [Display(Name = "Menu Area")]
        public string MenuArea { get; set; }
    }
    public class PageContentUpVM : PageContentVM
    {
    }
    public class PageContentCompleteIndxVM
    {
        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Page Content Id")]
        public int PageContentId { get; set; }

        [Required(ErrorMessage = "Please Enter Content")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        [Display(Name = "Contents")]
        public string Content { get; set; }

        #region Menu
        [Required(ErrorMessage = "Please Enter MenuId")]
        [Display(Name = "Menu Id")]
        public int MenuId { get; set; }

        [Required(ErrorMessage = "Select Parent Menu")]
        [Display(Name = "Parent Menu")]
        [Range(0, int.MaxValue, ErrorMessage = "Select MenuType")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "Please Enter Menu Name")]
        [Display(Name = "Menu Name")]
        public string MenuName { get; set; }

        [Required(ErrorMessage = "Visible")]
        [Display(Name = "Visible")]
        public bool IsVisible { get; set; }

        [Required(ErrorMessage = "Please Select Menu Area")]
        [Display(Name = "Menu Area")]
        public string MenuArea { get; set; }


        [Display(Name = "SlugMenu")]
        public string SlugMenu { get; set; }

        [Display(Name = "Parent Menu")]
        public string ParentMenuName { get; set; }
        #endregion
    }
}