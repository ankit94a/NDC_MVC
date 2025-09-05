using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NDCWeb.Data_Contexts;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class CourseRepository:Repository<Course>,ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetCourses()
        {
            List<SelectListItem> courses = NDCWebContext.Courses
                 .Where(n => n.CourseId > 59)
                    .OrderBy(n => n.CourseName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CourseId.ToString(),
                            Text = n.CourseName
                        }).ToList();
            return new SelectList(courses, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}