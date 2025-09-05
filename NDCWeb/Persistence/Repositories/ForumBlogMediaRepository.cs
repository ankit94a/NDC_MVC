using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class ForumBlogMediaRepository : Repository<ForumBlogMedia>, IForumBlogMediaRepository
    {
        public ForumBlogMediaRepository(DbContext context) : base(context)
        {
        }
    }
}