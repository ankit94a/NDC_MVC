using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class MenuRoleRepository : Repository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(DbContext context) : base(context)
        {
        }

        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}