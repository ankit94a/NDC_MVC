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
    public class LockerAllotmentRepository : Repository<LockerAllotment>, ILockerAllotmentRepository
    {
        public LockerAllotmentRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<LockerAllotmentReadVM>> GetLockerAllotmentListAsync(int courseId, string mode)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@Mode", mode),
                //returnCode
            };
            return await NDCWebContext.Database.SqlQuery<LockerAllotmentReadVM>("PopCourseMemberNameWithRank @CourseId, @Mode", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}