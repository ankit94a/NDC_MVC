using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class TallyDetailRepository : Repository<TallyDetail>, ITallyDetailRepository
    {
        public TallyDetailRepository(DbContext context) : base(context)
        {
        }
    }
}