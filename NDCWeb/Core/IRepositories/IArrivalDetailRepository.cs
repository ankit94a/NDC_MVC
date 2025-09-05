using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Core.IRepositories
{
    public interface IArrivalDetailRepository : IRepository<ArrivalDetail>
    {
        Task<IEnumerable<ArrivalAllVM>> GetViewArrivalAllInfoAsync(int courseId);
    }
}