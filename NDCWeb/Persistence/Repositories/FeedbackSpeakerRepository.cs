using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NDCWeb.Persistence.Repositories
{
    public class FeedbackSpeakerRepository: Repository<FeedbackSpeaker>, IFeedbackSpeakerRepository
    {
        public FeedbackSpeakerRepository(DbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<ShowSpeechFeedbackAllVM>> GetSpeechFeedback(int courseId)
        {
            SqlParameter[] sqlParam =
            {
                new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<ShowSpeechFeedbackAllVM>("Get_SpeakerFeedback @CourseId", sqlParam).ToListAsync();
        }

        public ShowSpeechFeedbackSummaryVM GetSpeechFeedbackSummaryRpt(int speechEventId)
        {
            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@SpeechEventId", speechEventId),
            };
            return NDCWebContext.Database.SqlQuery<ShowSpeechFeedbackSummaryVM>("Get_SpeakerFeedbackSummaryByEvent @SpeechEventId", sqlParam).Single();
        }
        
        public async Task<IEnumerable<ShowSpeechFeedbackAllVM>> GetSpeechFeedbackRpt(int speechEventId, int courseId)
        {
            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@SpeechEventId", speechEventId),
                    new SqlParameter("@CourseId", courseId),
            };
            return await NDCWebContext.Database.SqlQuery<ShowSpeechFeedbackAllVM>("Get_MemberSpeakerFeedbackByEvent @SpeechEventId, @CourseId", sqlParam).ToListAsync();
        }
        
        public async Task<IEnumerable<ShowSpeechFeedbackNotingVM>> GetSpeechFeedbackNoting(int SubjectId, DateTime FromDate, DateTime ToDate, int courseId)
        {
            SqlParameter[] sqlParam =
            {
                    new SqlParameter("@SubjectId", SubjectId),
                    new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                    new SqlParameter("@CourseId", courseId),
                };
            var result = await NDCWebContext.Database.SqlQuery<ShowSpeechFeedbackNotingVM>("Get_SpeechFeedbackNoting @SubjectId, @FromDate, @ToDate, @CourseId", sqlParam).ToListAsync();
            return result;
        }

        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}