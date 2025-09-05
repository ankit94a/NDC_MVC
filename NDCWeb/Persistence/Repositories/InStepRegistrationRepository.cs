using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class InStepRegistrationRepository : Repository<InStepRegistration>, IInStepRegistrationRepository
    {
        public InStepRegistrationRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<InStepRegistrationIndexVM> GetInStepCourseMemberList(int courseid)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseid),
            };
            return NDCWebContext.Database.SqlQuery<InStepRegistrationIndexVM>("Get_InStep_CourseMemberList @CourseId", sqlParam).ToList();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}