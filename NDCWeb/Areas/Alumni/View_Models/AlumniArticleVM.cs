using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Alumni.View_Models
{
    public class AlumniArticleVM
    {
        [Key]
        [Required(ErrorMessage = "Article Id Not Supplied")]
        [Display(Name = "AlumniArticle Id")]
        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Article Category Not Supplied")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Description")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }
    }
    public class AlumniArticleIndxVM : AlumniArticleVM
    {
        public AlumniArticleIndxVM()
        {
            iAlumniArticleMedias = new List<AlumniArticleMedia>();
        }
        public virtual ICollection<AlumniArticleMedia> iAlumniArticleMedias { get; set; }
    }
    public class AlumniArticleCrtVM : AlumniArticleVM
    {
        public AlumniArticleCrtVM()
        {
            iAlumniArticleMedias = new List<AlumniArticleMedia>();
        }
        public virtual ICollection<AlumniArticleMedia> iAlumniArticleMedias { get; set; }
    }
    public class AlumniArticleUpVM : AlumniArticleVM
    {
        public AlumniArticleUpVM()
        {
            iAlumniArticleMedias = new List<AlumniArticleMedia>();
        }
        public virtual ICollection<AlumniArticleMedia> iAlumniArticleMedias { get; set; }
    }
    public class AlumniArticleAllVM : AlumniArticleVM
    {
        public AlumniArticleAllVM()
        {
            iAlumniArticleMedias = new List<AlumniArticleMedia>();
        }
        public virtual ICollection<AlumniArticleMedia> iAlumniArticleMedias { get; set; }
        public string AlumniFullName { get; set; }
    }
}