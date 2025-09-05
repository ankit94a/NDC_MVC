using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class OtherRequestRepository : Repository<OtherRequest>, IOthrerRequestRepository
    {
        public OtherRequestRepository(DbContext context) : base(context)
        {

        }
    }
}