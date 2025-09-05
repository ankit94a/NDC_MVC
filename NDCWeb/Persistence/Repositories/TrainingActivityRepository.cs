using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class TrainingActivityRepository : Repository<TrainingActivity>, ITrainingActivityRepository
    {
        public TrainingActivityRepository(DbContext context) : base(context)
        {
        }
        //public async Task<IEnumerable<TrainingActivityCompleteIndxVM>> GetTrainingActivityListAsync(int? trainingActivityId)
        //{
        //    SqlParameter[] sqlParam =
        //    {
        //        new SqlParameter("@TrainingActivityId", trainingActivityId),
        //    };
        //    return await NDCWebContext.Database.SqlQuery<TrainingActivityCompleteIndxVM>("Get_TrainingActivity @TrainingActivityId", sqlParam).ToListAsync();
        //}
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}