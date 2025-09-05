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
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class CrsMemberPersonalRepository : Repository<CrsMemberPersonal>, ICrsMemberPersonalRepository
    {
        public CrsMemberPersonalRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<ParticipantIndxVM>> GetCourseMemberVerifiedListAsync(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<ParticipantIndxVM>("Get_CourseMemberVerifiedList @CourseId", sqlParam).ToListAsync();
        }
        public async Task<IEnumerable<ParticipantIndxVM>> GetIAGSDSCourseMemberListAsync(int staffId, int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@StaffId", staffId),
            };
            return await NDCWebContext.Database.SqlQuery<ParticipantIndxVM>("Get_IAGSDS_CourseMemberList @CourseId, @StaffId", sqlParam).ToListAsync();
        }

        #region Social Alerts
        public IEnumerable<StaffCrsMbrBirthdayAlertVM> GetStaffCourseMemberBirthdayAlert(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return NDCWebContext.Database.SqlQuery<StaffCrsMbrBirthdayAlertVM>("Get_Alert_Staff_CourseMember_Birthday @CourseId", sqlParam).ToList();
        }
        public IEnumerable<StaffCrsMbrBirthdayAlertVM> GetStaffCourseMemberBirthdayAlert(int courseId,int month)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@Month", month)
            };
            return NDCWebContext.Database.SqlQuery<StaffCrsMbrBirthdayAlertVM>("Get_Alert_Staff_CourseMember_Birthday @CourseId,@Month", sqlParam).ToList();
        }
        public IEnumerable<CrsMbrFamilyBirthdayAlertVM> GetCrsMbrFamilyBirthdayAlert(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return NDCWebContext.Database.SqlQuery<CrsMbrFamilyBirthdayAlertVM>("Get_Alert_CrsMbrFamilyBirthday @CourseId", sqlParam).ToList();
        }
        public IEnumerable<MarriageAnniversaryAlertVM> GetMarriageAnniversaryAlert(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return NDCWebContext.Database.SqlQuery<MarriageAnniversaryAlertVM>("Get_Alert_MarriageAnniversary @CourseId", sqlParam).ToList();
        }
        #endregion

        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}