using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDCWeb.Core.IRepositories
{
    public interface ICourseFeedbackRepository : IRepository<CourseFeedback>
    {
        Task<IEnumerable<CourseFeedbackIndxListVM>> GetCourseEndFeedbackMemberList(int courseId);
    }
}
