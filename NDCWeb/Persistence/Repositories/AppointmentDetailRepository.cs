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
    public class AppointmentDetailRepository : Repository<AppointmentDetail>, IAppointmentDetailRepository
    {
        public AppointmentDetailRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetAppointments()
        {
            List<SelectListItem> Appointments = NDCWebContext.AppointmentDetails
                    .OrderByDescending(n => n.AppointmentId)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.AppointmentId.ToString(),
                            Text = n.Organisation
                        }).ToList();

            return new SelectList(Appointments, "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}