using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class ForumBlogRepository : Repository<ForumBlog>, IForumBlogRepository
    {
        public ForumBlogRepository(DbContext context) : base(context)
        {
        }
        
        public async Task<IEnumerable<ForumBlogAllVM>> GetMemberUploadsForStaff(string Uid, string Category)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Uid", Uid),
                new SqlParameter("@Category", Category),
            };
            return await NDCWebContext.Database.SqlQuery<ForumBlogAllVM>("Get_MemberOnlineUploads_ForStaff @Uid, @Category", sqlParam).ToListAsync();
        }
        public IEnumerable<ForumBlogAllVM> GetAlertOnlineUploadsList()
        {
            return NDCWebContext.Database.SqlQuery<ForumBlogAllVM>("Get_OnlineUploads_Alert").ToList();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}