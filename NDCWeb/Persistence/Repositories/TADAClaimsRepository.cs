using NDCWeb.Core.IRepositories;
using NDCWeb.Areas.Member.View_Models;
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
    public class TADAClaimsRepository : Repository<TADAClaims>, ITADAClaimsRepository
    {
        public TADAClaimsRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TADAClaimsVM>> GetViewTADAAllInfoAsync(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<TADAClaimsVM>("GetCourseWiseTADADetail @CourseId", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}