using NDCWeb.Areas.Admin.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class PageContentRepository : Repository<PageContent>, IPageContentRepository
    {
        public PageContentRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<PageContentCompleteIndxVM>> GetPageContentListAsync()
        {
            return await NDCWebContext.Database.SqlQuery<PageContentCompleteIndxVM>("Get_PageContent").ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}