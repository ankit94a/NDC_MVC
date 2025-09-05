using NDCWeb.Core.IRepositories;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Persistence.Repositories
{
    public class SuggestionRepository : Repository<Suggestion>, ISuggestionRepository
    {
        public SuggestionRepository(DbContext context) : base(context)
        {

        }
        public IEnumerable<SelectListItem> GetSuggestionType()
        {
            var SuggestionOptions = new List<SelectListItem>
                {
                    new SelectListItem{ Text="Library", Value = "Library"},
                    new SelectListItem{ Text="GSO (System)", Value = "GSO (System)" },
                    new SelectListItem{ Text="Training", Value = "Training" },
                    new SelectListItem{ Text="Admin", Value = "Admin" },
                    new SelectListItem{ Text="Security", Value = "Security" },
                    new SelectListItem{ Text="Officer Mess", Value = "Officer Mess" },
                    new SelectListItem{ Text="University Division", Value = "University Division" },
                };
            return SuggestionOptions;
        }
        public IEnumerable<SelectListItem> GetStatusType()
        {
            var StatusOptions = new List<SelectListItem>
                {
                    new SelectListItem{ Text="Pending", Value = "Pending"},
                    new SelectListItem{ Text="Work In Progress", Value = "Work In Progress" },
                    new SelectListItem{ Text="Completed", Value = "Completed" },
                    new SelectListItem{ Text="Rejected", Value = "Rejected" },
                };
            return StatusOptions;
        }
    }
}