using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NDCWeb.Models;
namespace NDCWeb.Core.IRepositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<SelectListItem> GetCourses();
    }
}