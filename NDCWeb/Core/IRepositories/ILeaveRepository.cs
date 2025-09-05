using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Core.IRepositories
{
    public interface ILeaveRepository : IRepository<Leave>
    {
        int CheckMemberLeaveInsVal(string courseMemberId);
        Task<IEnumerable<AddStatusLeaveInfoListVM>> GetAddStatusLeaveInfoByAppointmentAsync(int staffId, int courseId, string staffType);
        Task<IEnumerable<ShowCompleteLeaveStatusListVM>> GetViewStatusLeaveInfoByAppointmentAsync(int staffId, int courseId, string staffType);

        Task<IEnumerable<ShowCompleteLeaveStatusListVM>> GetViewStatusLeaveInfoByAppointmentForGenCertAsync(int staffId, int courseId, string staffType);
        Task<IEnumerable<CalendarLeaveInfoListVM>> GetViewCourseWiseLeaveCountAsync(int courseId);
        IEnumerable<CalendarLeaveInfoListVM> GetViewCourseWiseLeaveCount(int courseId);
        ShowLeaveRptVM GetLeaveReportDetail(int leaveId);
        ShowLeaveCertificateRptVM GetLeaveCertificate(int leaveId);

        ShowLeaveCertificateRptVM GenerateCertificate(int leaveId);
        string GetAlertLeaveForCourseMember(string uId);
    }
}