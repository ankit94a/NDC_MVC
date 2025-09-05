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
    public class SubjectMasterRepository : Repository<SubjectMaster>, ISubjectMasterRepository
    {
        public SubjectMasterRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<SelectListItem> GetSubjects()
        {
            List<SelectListItem> countries = NDCWebContext.SubjectMasters
                    .OrderBy(n => n.SubjectName)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.SubjectId.ToString(),
                            Text = n.SubjectName
                        }).ToList();
            return new SelectList(countries, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}