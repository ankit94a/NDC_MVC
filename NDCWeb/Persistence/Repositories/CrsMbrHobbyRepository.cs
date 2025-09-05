using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class CrsMbrHobbyRepository : Repository<CrsMbrHobby>, ICrsMbrHobbyRepository
    {
        public CrsMbrHobbyRepository(DbContext context) : base(context)
        {
        }
    }
}