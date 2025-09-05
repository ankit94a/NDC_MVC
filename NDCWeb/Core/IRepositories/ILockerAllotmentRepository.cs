using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface ILockerAllotmentRepository : IRepository<LockerAllotment>
    {
        Task<IEnumerable<LockerAllotmentReadVM>> GetLockerAllotmentListAsync(int courseId, string mode);
    }
}
