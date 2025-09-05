using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Core.IRepositories
{
    public interface IStateMasterRepository : IRepository<StateMaster>
    {
        IEnumerable<SelectListItem> GetStates(int countryid);
    }
}
