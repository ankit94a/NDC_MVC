using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDCWeb.Models
{
    public class PageContent
    {
        [Key]
        public int PageContentId { get; set; }
        
        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        public int MenuId { get; set; }
        public virtual MenuItemMaster MenuItemMasters { get; set; }
    }
}