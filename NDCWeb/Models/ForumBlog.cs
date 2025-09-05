using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Models
{
    public class ForumBlog : BaseEntity
    {
        public ForumBlog()
        {
            iForumBlogMedias = new List<ForumBlogMedia>();
        }
        [Key]
        public int ForumBlogId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string MemberRemark { get; set; }
        public string StaffRemark { get; set; }
        public string Status { get; set; }
        public string Module {  get; set; } //Add by Amitesh
        public int StaffId { get; set; }
        public virtual StaffMaster StaffMasters { get; set; }

        public virtual ICollection<ForumBlogMedia> iForumBlogMedias { get; set; }
        public string IsReaded {  get; set; }
    }
}