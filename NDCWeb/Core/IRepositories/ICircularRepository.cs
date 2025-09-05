using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface ICircularRepository : IRepository<Circular>
    {
        IEnumerable<CircularAlertVM> GetAlertCircularList(string category);
    }
}
