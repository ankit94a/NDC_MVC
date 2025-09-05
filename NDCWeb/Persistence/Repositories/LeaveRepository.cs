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
    public class LeaveRepository : Repository<Leave>, ILeaveRepository
    {
        public LeaveRepository(DbContext context) : base(context)
        {

        }
        public int CheckMemberLeaveInsVal(string courseMemberId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@MemberId", courseMemberId),
            };
            return NDCWebContext.Database.SqlQuery<int>("CheckMemberLeaveInsertVal @MemberId", sqlParam).Single();
        }
        public async Task<IEnumerable<AddStatusLeaveInfoListVM>> GetAddStatusLeaveInfoByAppointmentAsync(int staffId, int courseId, string staffType)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@StaffId", staffId),
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@StaffType", staffType),
                new SqlParameter("@Mode", "AddStatus"),
            };
            return await NDCWebContext.Database.SqlQuery<AddStatusLeaveInfoListVM>("Get_AddViewStatus_LeaveInfoByAppointment2 @StaffId, @CourseId, @StaffType, @Mode", sqlParam).ToListAsync();
        }
        public async Task<IEnumerable<ShowCompleteLeaveStatusListVM>> GetViewStatusLeaveInfoByAppointmentAsync(int staffId, int courseId, string staffType)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@StaffId", staffId),
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@StaffType", staffType),
                new SqlParameter("@Mode", "ViewStatus"),
            };
            return await NDCWebContext.Database.SqlQuery<ShowCompleteLeaveStatusListVM>("Get_AddViewStatus_LeaveInfoByAppointment2 @StaffId, @CourseId, @StaffType, @Mode", sqlParam).ToListAsync();
        }

        public async Task<IEnumerable<ShowCompleteLeaveStatusListVM>> GetViewStatusLeaveInfoByAppointmentForGenCertAsync(int staffId, int courseId, string staffType)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@StaffId", staffId),
                new SqlParameter("@CourseId", courseId),
                new SqlParameter("@StaffType", staffType),
                new SqlParameter("@Mode", "GenerateCertficate"),
            };
            return await NDCWebContext.Database.SqlQuery<ShowCompleteLeaveStatusListVM>("Get_AddViewStatus_LeaveInfoByAppointment2 @StaffId, @CourseId, @StaffType, @Mode", sqlParam).ToListAsync();
        }
        public async Task<IEnumerable<CalendarLeaveInfoListVM>> GetViewCourseWiseLeaveCountAsync(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<CalendarLeaveInfoListVM>("GetViewCourseWiseLeaveCount @CourseId", sqlParam).ToListAsync();
        }
        public IEnumerable<CalendarLeaveInfoListVM> GetViewCourseWiseLeaveCount(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return NDCWebContext.Database.SqlQuery<CalendarLeaveInfoListVM>("GetViewCourseWiseLeaveCount @CourseId", sqlParam).ToList();
        }
        public ShowLeaveRptVM GetLeaveReportDetail(int leaveId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@LeaveId", leaveId),
            };
            return NDCWebContext.Database.SqlQuery<ShowLeaveRptVM>("Get_LeaveReportDetail @LeaveId", sqlParam).Single();
        }
        public ShowLeaveCertificateRptVM GetLeaveCertificate(int leaveId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@LeaveId", leaveId),
            };
            return NDCWebContext.Database.SqlQuery<ShowLeaveCertificateRptVM>("Get_Rpt_LeaveCertificate @LeaveId", sqlParam).Single();
        }
        public string GetAlertLeaveForCourseMember(string uId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@UId", uId),
            };
            return NDCWebContext.Database.SqlQuery<string>("Get_Alert_LeaveForCourseMember @UId", sqlParam).Single();
        }
        public ShowLeaveCertificateRptVM GenerateCertificate(int leaveId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@LeaveId", leaveId),
            };
            return NDCWebContext.Database.SqlQuery<ShowLeaveCertificateRptVM>("Generate_Rpt_LeaveCertificate @LeaveId", sqlParam).Single();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}
