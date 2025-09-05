using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class UserActivityRepository: Repository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(DbContext context) : base(context)
        {
        }
    }
}