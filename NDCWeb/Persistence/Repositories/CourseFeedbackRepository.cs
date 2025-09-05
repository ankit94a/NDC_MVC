using NDCWeb.Areas.Member.View_Models;
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
    public class CourseFeedbackRepository : Repository<CourseFeedback>, ICourseFeedbackRepository
    {
        public CourseFeedbackRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<CourseFeedbackIndxListVM>> GetCourseEndFeedbackMemberList(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<CourseFeedbackIndxListVM>("GetCourseEndFeedbackMember @CourseId", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}