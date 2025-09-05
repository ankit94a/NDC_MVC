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
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetFaculties()
        {
            List<SelectListItem> Fuculties = NDCWebContext.Faculties
                    .OrderByDescending(n => n.FacultyId)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.FacultyId.ToString(),
                            Text = n.FacultyName
                        }).ToList();

            return new SelectList(Fuculties, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}