using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class AccountInfoRepository : Repository<AccountInfo>, IAccountInfoRepository
    {
        public AccountInfoRepository(DbContext context) : base(context)
        {
        }
    }
}