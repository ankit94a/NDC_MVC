using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NDCWeb.Persistence.Repositories
{
    public class CircularDetailRepository : Repository<CircularDetail>, ICircularDetailRepository
    {
        public CircularDetailRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<CircularAlertVM> GetOrderAsPerDesignation(string designation, string uId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@DesignationType", designation),
                new SqlParameter("@UserId", uId),
            };
            return NDCWebContext.Database.SqlQuery<CircularAlertVM>("usp_GetOdersAsPerDesignation @DesignationType,@UserId", sqlParam).ToList();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }

    }
}