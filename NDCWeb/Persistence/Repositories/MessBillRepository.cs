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
    public class MessBillRepository:Repository<MessBill>, IMessBillRepository
    {
        public MessBillRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<MessBillReadVM>> GetMessBillListAsync(int courseId, string mode)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
               // new SqlParameter("@Mode", mode),
                //returnCode
            };
            return await NDCWebContext.Database.SqlQuery<MessBillReadVM>("PopCourseMemberNameWithRankTest @CourseId, @Mode", sqlParam).ToListAsync();
        }

        public async Task<IEnumerable<MessBillReadVM>> GetMessBillListAsync(int courseId)
        {
            SqlParameter[] sqlParam =
             {
                new SqlParameter("@CourseId", courseId),
               // new SqlParameter("@Mode", mode),
                //returnCode
            };
            return await NDCWebContext.Database.SqlQuery<MessBillReadVM>("PopCourseMemberNameWithRankTest @CourseId", sqlParam).ToListAsync();
        }
        public async Task<IEnumerable<MessBillReadVM>> GetStaffUserandbillListAsync()
        {
            return await NDCWebContext.Database.SqlQuery<MessBillReadVM>("GetStaffMasterAndMessBill").ToListAsync();
        }
        public async Task<IEnumerable<MessBillUpdate>> GetMessBillList(int courseId)
        {
            SqlParameter[] sqlParam =
             {
                new SqlParameter("@CourseId", courseId),
               // new SqlParameter("@Mode", mode),
                //returnCode
            };
            return await NDCWebContext.Database.SqlQuery<MessBillUpdate>("CourseMemberNamewithBillDetail @CourseId", sqlParam).ToListAsync();
        }

        public IEnumerable<MessBillReadVM>GetBillDetail(string Uid)
        {
            SqlParameter[] sqlParam =
             {
                new SqlParameter("@UId", Uid),
               // new SqlParameter("@Mode", mode),
                //returnCode
            };
            return NDCWebContext.Database.SqlQuery<MessBillReadVM>("CourseMemberWiseBillDetail @UId", sqlParam).ToList();
        }


        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}