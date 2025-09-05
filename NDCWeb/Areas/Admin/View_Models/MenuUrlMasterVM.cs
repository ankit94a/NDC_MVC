using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NDCWeb.Infrastructure.Constants;

namespace NDCWeb.Areas.Admin.View_Models
{
    public class MenuUrlMasterVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MenuUrl Id")]
        [Display(Name = "MenuUrl Id")]
        public int MenuUrlId { get; set; }
        
        [Required(ErrorMessage = "Please Enter UrlPrefix")]
        [Display(Name = "UrlPrefix")]
        [RegularExpression(@"^[a-zA-Z0-9-]*$", ErrorMessage = "Special chars not allowed")]
        public string UrlPrefix { get; set; }

        [Required(ErrorMessage = "Please Enter Controller")]
        [Display(Name = "Controller")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Special chars not allowed")]
        public string Controller { get; set; }

        [Required(ErrorMessage = "Please Enter Action")]
        [Display(Name = "Action")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Special chars not allowed")]
        public string Action { get; set; }

        [Required(ErrorMessage = "Please Select PageType")]
        [Display(Name = "PageType")]
        [Range(1, int.MaxValue, ErrorMessage = "Select PageType")]
        public PageType PageType { get; set; }

        [Required(ErrorMessage = "Please Select MenuLevel")]
        [Display(Name = "MenuLevel")]
        [Range(1, int.MaxValue, ErrorMessage = "Select MenuLevel")]
        public MenuLevel MenuLevel { get; set; }

        [Required(ErrorMessage = "Please Select Area")]
        [Display(Name = "Area")]
        public string UrlArea { get; set; }
    }
    public class MenuUrlMasterIndxVM : MenuUrlMasterVM
    {
    }
    public class MenuUrlMasterCrtVM : MenuUrlMasterVM
    {
    }
    public class MenuUrlMasterUpVM : MenuUrlMasterVM
    {
    }
}