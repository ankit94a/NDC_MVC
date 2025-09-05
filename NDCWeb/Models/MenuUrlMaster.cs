using NDCWeb.Infrastructure.Constants;
using System.ComponentModel.DataAnnotations;

namespace NDCWeb.Models
{
    public class MenuUrlMaster
    {
        [Key]
        public int MenuUrlId { get; set; }
        public string UrlPrefix { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public PageType PageType { get; set; }
        public MenuLevel MenuLevel { get; set; }
        public string UrlArea { get; set; }
    }
}