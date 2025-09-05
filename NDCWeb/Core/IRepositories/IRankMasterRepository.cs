using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NDCWeb.Core.IRepositories
{
    public interface IRankMasterRepository : IRepository<RankMaster>
    {
        IEnumerable<SelectListItem> GetRanks();
        IEnumerable<SelectListItem> GetRanks(string Service); 
        IEnumerable<SelectListItem> GetRanksParticipants();
        IEnumerable<SelectListItem> GetRanksParticipants(string Service);
        IEnumerable<SelectListItem> GetRanksInstep(string Service);
    }
}
