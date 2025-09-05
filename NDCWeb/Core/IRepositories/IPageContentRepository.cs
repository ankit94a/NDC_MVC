using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface IPageContentRepository : IRepository<PageContent>
    {
        Task<IEnumerable<PageContentCompleteIndxVM>> GetPageContentListAsync();
    }
}
