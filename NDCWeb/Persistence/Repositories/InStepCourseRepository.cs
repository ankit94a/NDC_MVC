using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class InStepCourseRepository : Repository<InStepCourse>, IInStepCourseRepository
    {
        public InStepCourseRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetInStepCourses()
        {
            List<SelectListItem> courses = NDCWebContext.InStepCourses
                       .OrderBy(n => n.CourseName)
                           .Select(n =>
                           new SelectListItem
                           {
                               Value = n.CourseId.ToString(),
                               Text = n.CourseName
                           }).ToList();
            return new SelectList(courses, "Value", "Text");

        }
        public IEnumerable<SelectListItem> GetSelectedInStepCourses(int id)
        {
            var CourseOptions = NDCWebContext.InStepCourses.Where(x => x.CourseId == id)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.CourseId.ToString(),
                            Text = n.CourseName
                        }).ToList();
            return new SelectList(CourseOptions, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}