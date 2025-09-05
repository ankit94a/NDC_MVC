using NDCWeb.Areas.Alumni.View_Models;
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
    public class AlumniArticleRepository : Repository<AlumniArticle>, IAlumniArticleRepository
    {
        public AlumniArticleRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<AlumniArticleAllVM>> GetAlumniUploadsForStaff(string Uid, string Category)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Uid", Uid),
                new SqlParameter("@Category", Category)
            };
            return await NDCWebContext.Database.SqlQuery<AlumniArticleAllVM>("Get_AlumniUploads_ForStaff @Uid, @Category", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}