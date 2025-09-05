using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class AlumniFeedbackRepository : Repository<AlumniFeedback>, IAlumniFeedbackRepository
    {
        public AlumniFeedbackRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AlumniFeedbackListVM>> AlumniFeedbackGetAll()
        {
            return await NDCWebContext.Database.SqlQuery<AlumniFeedbackListVM>("Get_AlumniFeedback").ToListAsync();
        }
        //public AlumniFeedbackVM AlumniFeedbackGetById(int feedBackId)
        //{
        //    SqlParameter[] sqlParam =
        //    {
        //        new SqlParameter("@FeedBackId", feedBackId),
        //    };
        //    return NDCWebContext.Database.SqlQuery<AlumniFeedbackVM>("Get_AlumniFeedback @FeedBackId", sqlParam).Single();
        //}
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}