using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface ICrsMemberPersonalRepository : IRepository<CrsMemberPersonal>
    {
        Task<IEnumerable<ParticipantIndxVM>> GetCourseMemberVerifiedListAsync(int courseId);
        Task<IEnumerable<ParticipantIndxVM>> GetIAGSDSCourseMemberListAsync(int staffId, int courseId);

        #region Social Alerts
        IEnumerable<StaffCrsMbrBirthdayAlertVM> GetStaffCourseMemberBirthdayAlert(int courseId);
        IEnumerable<StaffCrsMbrBirthdayAlertVM> GetStaffCourseMemberBirthdayAlert(int courseId,int month);
        IEnumerable<CrsMbrFamilyBirthdayAlertVM> GetCrsMbrFamilyBirthdayAlert(int courseId);
        IEnumerable<MarriageAnniversaryAlertVM> GetMarriageAnniversaryAlert(int courseId);
        #endregion
    }
}
