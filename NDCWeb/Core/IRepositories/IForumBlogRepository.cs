using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface IForumBlogRepository : IRepository<ForumBlog>
    {
        Task<IEnumerable<ForumBlogAllVM>> GetMemberUploadsForStaff(string Uid, string Category);
        IEnumerable<ForumBlogAllVM> GetAlertOnlineUploadsList();
    }
}
