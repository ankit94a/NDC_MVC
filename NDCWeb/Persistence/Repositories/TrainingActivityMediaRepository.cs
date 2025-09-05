using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class TrainingActivityMediaRepository : Repository<TrainingActivityMedia>, ITrainingActivityMediaRepository
    {
        public TrainingActivityMediaRepository(DbContext context) : base(context)
        {
        }
    }
}