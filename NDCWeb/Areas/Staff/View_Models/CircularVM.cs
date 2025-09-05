using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class CircularVM
    {
        public CircularVM()
        {
            iCircularMedias = new List<CircularMedia>();
        }

        [Key]
        [Required(ErrorMessage = "Circular Id Not Supplied")]
        [Display(Name = "Circular Id")]
        public int CircularId { get; set; }

        [Required(ErrorMessage = "Category Not Supplied")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        public virtual ICollection<CircularMedia> iCircularMedias { get; set; }
    }
    public class CircularIndxVM : CircularVM
    {
    }
    public class CircularCrtVM : CircularVM
    {
        //public CircularVM()
        //{
        //    iCircularDetail = new List<CircularDetail>();
        //}
        [Required(ErrorMessage = "Designation Id Not Supplied")]
        [Display(Name = "Designation Id")]
        public int DesignationId { get; set; }
        //public virtual ICollection<CircularDetail> iCircularDetail { get; set; }
    }
    public class CircularUpVM : CircularVM
    {
    }

    public class CircularAlertVM
    {
        public CircularAlertVM()
        {
            iCircularMedias = new List<CircularMedia>();
        }

        [Key]
        [Required(ErrorMessage = "Circular Id Not Supplied")]
        [Display(Name = "Circular Id")]
        public int CircularId { get; set; }

        [Required(ErrorMessage = "Category Not Supplied")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        public virtual ICollection<CircularMedia> iCircularMedias { get; set; }
    }
    public class OrderVisibilityVM
    {
        public bool Allowed { get; set; }
    }
}