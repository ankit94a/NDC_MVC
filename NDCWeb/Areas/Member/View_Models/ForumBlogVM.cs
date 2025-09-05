using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NDCWeb.Areas.Member.View_Models
{
    public class ForumBlogVM
    {

        [Key]
        [Required(ErrorMessage = "ForumBlog Id Not Supplied")]
        [Display(Name = "ForumBlog Id")]
        public int ForumBlogId { get; set; }

        [Required(ErrorMessage = "Category Not Supplied")]
        [Display(Name = "Category")]
        [RegularExpression(@"^[\w ]*$", ErrorMessage = "Special chars not allowed")]
        public string Category { get; set; }

        //[Required(ErrorMessage = "Module Not Supplied")]
        [Display(Name = "Module")]
        //[RegularExpression(@"^[\w ]*$", ErrorMessage = "Special chars not allowed")]
        public string Module { get; set; }

        [Display(Name = "Description")]
        //[RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string Description { get; set; }

        [Display(Name = "Member Remark")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string MemberRemark { get; set; }

        [Display(Name = "Staff Remark")]
        [RegularExpression(@"^[\w,.!? ]*$", ErrorMessage = "Special chars not allowed")]
        public string StaffRemark { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Staff")]
        [Required(ErrorMessage = "Select Staff")]
        public int StaffId { get; set; }
        public virtual StaffMaster StaffMasters { get; set; }
    }
    public class ForumBlogIndxVM : ForumBlogVM
    {
        public ForumBlogIndxVM()
        {
            iForumBlogMedias = new List<ForumBlogMedia>();
        }
        public virtual ICollection<ForumBlogMedia> iForumBlogMedias { get; set; }
    }
    public class ForumBlogCrtVM : ForumBlogVM
    {
        public ForumBlogCrtVM()
        {
            iForumBlogMedias = new List<ForumBlogMedia>();
        }
        public virtual ICollection<ForumBlogMedia> iForumBlogMedias { get; set; }
    }
    public class ForumBlogUpVM : ForumBlogVM
    {
        public ForumBlogUpVM()
        {
            iForumBlogMedias = new List<ForumBlogMedia>();
        }
        public virtual ICollection<ForumBlogMedia> iForumBlogMedias { get; set; }
        public string IsReaded { get; set; }
    }
    public class ForumBlogAllVM : ForumBlogVM
    {
        public ForumBlogAllVM()
        {
            iForumBlogMedias = new List<ForumBlogMedia>();
        }
        public virtual ICollection<ForumBlogMedia> iForumBlogMedias { get; set; }
        #region Member
        public string MemberFullName { get; set; }
        public string LockerNo { get; set; }
        public string ServiceNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? CreatedAt { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime? LastUpdatedAt { get; set; }
        public string IsReaded { get; set; }
        #endregion
    }
}