using NDCWeb.Models;
using System;
using NDCWeb.Areas.Member.View_Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface ITADAClaimsRepository : IRepository<TADAClaims>
    {
        Task<IEnumerable<TADAClaimsVM>> GetViewTADAAllInfoAsync(int courseId);
    }
}
