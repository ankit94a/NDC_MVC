using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class ChildrenDetailRepository : Repository<ChildrenDetail>, IChildrenDetailRepository
    {
        public ChildrenDetailRepository(DbContext context) : base(context)
        {
        }
    }
}