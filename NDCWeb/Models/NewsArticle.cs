using NDCWeb.Infrastructure.Constants;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDCWeb.Models
{
    public class NewsArticle : BaseEntity
    {
        [Key]
        public int NewsArticleId { get; set; }
        public NewsCategory NewsCategory { get; set; }
        public string Headline { get; set; }
        public string ArticleUrl { get; set; }

        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public bool Highlight { get; set; }
        public bool Archive { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ArchiveDate { get; set; }
        public NewsDisplayArea? DisplayArea { get; set; }
    }
}