using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Core.IRepositories
{
    public interface IInfotechRepository : IRepository<Infotech>
    {
        Task<IEnumerable<InfoTechCompleteVM>> GetInfoTechComplete();
    }
}