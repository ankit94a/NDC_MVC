using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NDCWeb.Persistence.Repositories
{
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository
    {
        public VisitorRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<LatestVisit>> GetVisitStats(int year)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Year", year)
            };
            return await NDCWebContext.Database.SqlQuery<LatestVisit>("USPGetVisitStats @Year", sqlParam).ToListAsync();
        }
        public int GetLatestVisit(DateTime dtTime)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@VisitDate", dtTime),
            };
            return NDCWebContext.Database.SqlQuery<int>("USPGetLastDayVisit @VisitDate", sqlParam).Single();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }

}