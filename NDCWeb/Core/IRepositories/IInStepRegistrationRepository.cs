using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Models;
using NDCWeb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NDCWeb.Core.IRepositories
{
    public interface IInStepRegistrationRepository: IRepository<InStepRegistration>
    {
        IEnumerable<InStepRegistrationIndexVM> GetInStepCourseMemberList(int courseid);
    }
}