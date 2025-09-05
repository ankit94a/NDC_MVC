using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class SocialCalendarRepository : Repository<SocialCalendarVM>, ISocialCalendarRepository
    {
        public SocialCalendarRepository(DbContext context) : base(context)
        {

        }
        public IEnumerable<BirthDaysVM> GetBirthdayList()
        {
            return NDCWebContext.Database.SqlQuery<BirthDaysVM>("Get_BirthdayList").ToList();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}