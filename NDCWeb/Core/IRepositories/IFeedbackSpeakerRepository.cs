using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace NDCWeb.Core.IRepositories
{
    public interface IFeedbackSpeakerRepository : IRepository<FeedbackSpeaker>
    {
        Task<IEnumerable<ShowSpeechFeedbackAllVM>> GetSpeechFeedback(int courseId);
        ShowSpeechFeedbackSummaryVM GetSpeechFeedbackSummaryRpt(int speechEventId);
        Task<IEnumerable<ShowSpeechFeedbackAllVM>> GetSpeechFeedbackRpt(int speechEventId, int courseId);
        Task<IEnumerable<ShowSpeechFeedbackNotingVM>> GetSpeechFeedbackNoting(int SubjectId, DateTime FromDate, DateTime ToDate, int courseId);
    }
}
