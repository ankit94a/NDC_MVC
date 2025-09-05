using NDCWeb.Models;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Core.IRepositories
{
    public interface IVisitorRepository : IRepository<Visitor>
    {
        Task<IEnumerable<LatestVisit>> GetVisitStats(int year);
    }
}