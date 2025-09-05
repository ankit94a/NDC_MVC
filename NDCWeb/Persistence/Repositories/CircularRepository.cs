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
    public class CircularRepository : Repository<Circular>, ICircularRepository
    {
        public CircularRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<CircularAlertVM> GetAlertCircularList(string category)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Category", category),
            };
            return NDCWebContext.Database.SqlQuery<CircularAlertVM>("Get_Alert_Circular @Category", sqlParam).ToList();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}