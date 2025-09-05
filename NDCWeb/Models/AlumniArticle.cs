using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class AlumniArticle : BaseEntity
    {
        public AlumniArticle()
        {
            iAlumniArticleMedias = new List<AlumniArticleMedia>();
        }
        [Key]
        public int ArticleId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AlumniArticleMedia> iAlumniArticleMedias { get; set; }
    }
}