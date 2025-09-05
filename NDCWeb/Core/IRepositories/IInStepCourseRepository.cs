using NDCWeb.Models;
using NDCWeb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Core.IRepositories
{
    public interface IInStepCourseRepository: IRepository<InStepCourse>
    {
        IEnumerable<SelectListItem> GetInStepCourses(); 
        IEnumerable<SelectListItem> GetSelectedInStepCourses(int id);

    }
}