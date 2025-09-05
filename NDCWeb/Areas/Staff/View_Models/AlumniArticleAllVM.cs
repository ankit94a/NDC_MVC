using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Staff.View_Models
{
    public class AlumniArticleAllVM
    {
        public AlumniArticleAllVM()
        {
            iAlumniArticleMedias = new List<AlumniArticleMedia>();
        }
        public int CircularId { get; set; }

        public string FullName { get; set; }
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "Category Not Supplied")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"^[\w \.\,\-]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        public virtual ICollection<AlumniArticleMedia> iAlumniArticleMedias { get; set; }
    }
}