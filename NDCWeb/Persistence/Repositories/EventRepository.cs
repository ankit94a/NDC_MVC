using NDCWeb.Areas.Staff.View_Models;
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
    public class EventRepository: Repository<Event>, IEventRepository
    {
        public EventRepository(DbContext context) : base(context)
        {

        }
        public async Task<IEnumerable<LatestEventVM>> GetViewLatestEventAllInfoAsync()
        {
            return await NDCWebContext.Database.SqlQuery<LatestEventVM>("GetLatestEvent").ToListAsync();
        }
        public async Task<IEnumerable<LatestEventVM>> GetViewLatestEventMembersAllInfoAsync(string uId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@EventId", uId),
            };
            return await NDCWebContext.Database.SqlQuery<LatestEventVM>("GetLatestEventMembers @EventId", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}