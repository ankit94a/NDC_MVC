using NDCWeb.Models;
using NDCWeb.Areas.Staff.View_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface IMessBillRepository : IRepository<MessBill>
    {
        Task<IEnumerable<MessBillReadVM>> GetMessBillListAsync (int courseId);
        Task<IEnumerable<MessBillReadVM>> GetStaffUserandbillListAsync();
        Task<IEnumerable<MessBillUpdate>> GetMessBillList(int courseId);
        IEnumerable<MessBillReadVM> GetBillDetail(string Uid);
       
    }
}