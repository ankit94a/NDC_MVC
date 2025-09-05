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
    public class ArrivalDetailRepository : Repository<ArrivalDetail>, IArrivalDetailRepository
    {
        public ArrivalDetailRepository(DbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<ArrivalAllVM>> GetViewArrivalAllInfoAsync(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<ArrivalAllVM>("GetCourseWiseArrivalDetail @CourseId", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}