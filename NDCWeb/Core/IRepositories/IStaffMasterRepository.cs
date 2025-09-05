using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Core.IRepositories
{
    public interface IStaffMasterRepository : IRepository<StaffMaster>
    {
        IEnumerable<SelectListItem> GetStaff();
        IEnumerable<SelectListItem> GetSDS();
        IEnumerable<SelectListItem> GetIAG();
        IEnumerable<SelectListItem> GetSDSAppointment();
        IEnumerable<SelectListItem> GetIAGAppointment();
        Task<IEnumerable<StaffUserIndxListVM>> GetStaffUserListAsync();
        int DMLUpdateStaffAndAccount(int staffId);
    }
}
