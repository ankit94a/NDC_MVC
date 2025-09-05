using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface ICircularDetailRepository: IRepository<CircularDetail>
    {
        IEnumerable<CircularAlertVM> GetOrderAsPerDesignation(string designation, string uId);
    }
}
