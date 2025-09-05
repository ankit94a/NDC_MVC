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
    public class SpeechEventRepository : Repository<SpeechEvent>, ISpeechEventRepository
    {
        public SpeechEventRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SpeechEventAll>> GetSpeakerFeedbackForStaff(string Category)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@Subject", Category),
            };
            return await NDCWebContext.Database.SqlQuery<SpeechEventAll>("Get_SpeakerFeedback_ForStaff  @Subject", sqlParam).ToListAsync();
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}