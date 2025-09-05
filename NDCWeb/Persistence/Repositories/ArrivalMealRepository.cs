using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class ArrivalMealRepository : Repository<ArrivalMeal>, IArrivalMealRepository
    {
        public ArrivalMealRepository(DbContext context) : base(context)
        {
        }
    }
}