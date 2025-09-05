using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class HonourAwardRepository : Repository<HonourAward>, IHonourAwardRepository
    {
        public HonourAwardRepository(DbContext context) : base(context)
        {
        }
    }
}