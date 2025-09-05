using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class MPhilMemberRepository : Repository<MPhilMember>, IMPhilMemberRepository
    {
        public MPhilMemberRepository(DbContext context) : base(context)
        {
        }
    }
}