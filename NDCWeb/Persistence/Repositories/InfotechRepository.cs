using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class InfotechRepository : Repository<Infotech>, IInfotechRepository
    {
        public InfotechRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<InfoTechCompleteVM>> GetInfoTechComplete()
        {
            return await NDCWebContext.Database.SqlQuery<InfoTechCompleteVM>("Get_Infotech").ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}