using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class CrsMbrAddressRepository : Repository<CrsMbrAddress>, ICrsMbrAddressRepository
    {
        public CrsMbrAddressRepository(DbContext context) : base(context)
        {
        }
    }
}