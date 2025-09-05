using NDCWeb.Core.IRepositories;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace NDCWeb.Persistence.Repositories
{
    public class RankMasterRepository : Repository<RankMaster>, IRankMasterRepository
    {
        public RankMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetRanks()
        {
            List<SelectListItem> Ranks = NDCWebContext.RankMasters
                .Where(m=>m.ForParticipant == true)
                    .OrderBy(n => n.Seniority)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.RankId.ToString(),
                            Text = n.RankName
                        }).ToList();

            return new SelectList(Ranks, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetRanks(string Service)
        {
            List<SelectListItem> Ranks = NDCWebContext.RankMasters
                .Where(w => w.Service == Service)
                    .OrderBy(n => n.Seniority)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.RankId.ToString(),
                            Text = n.RankName
                        }).ToList();

            return new SelectList(Ranks, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetRanksParticipants()
        {
            List<SelectListItem> Ranks = NDCWebContext.RankMasters
                .Where(w => w.ForParticipant == true)
                    .OrderBy(n => n.Seniority)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.RankId.ToString(),
                            Text = n.RankName
                        }).ToList();

            return new SelectList(Ranks, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetRanksParticipants(string Service)
        {
            List<SelectListItem> Ranks = NDCWebContext.RankMasters
                .Where(w => w.ForParticipant == true && w.Service == Service)
                    .OrderBy(n => n.Seniority)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.RankId.ToString(),
                            Text = n.RankName
                        }).ToList();

            return new SelectList(Ranks, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetRanksInstep(string _Service)
        {

            var inputParameterA = new List<string> { "FOREIGN ARMY", "FOREIGN NAVY", "FOREIGN AIR FORCE", "FOREIGN CIVIL SERVICES" };
            var inputParameterc = new List<string> { "NA"};
            int[] rankIdPara = new int[12] { 69, 70, 71, 72, 77, 78, 80, 85, 110, 83, 84, 105 };
            //var inputParameterB = new List<string> { "INDIAN ARMY", "INDIAN NAVY", "INDIAN AIR FORCE", "INDIAN CIVIL SERVICES" };

            var result = from r in NDCWebContext.RankMasters.AsEnumerable()
                         where _Service == "INTERNATIONAL OFFICER" ? rankIdPara.Contains(r.RankId): r.Service.Contains(_Service) & r.ForParticipant == true 
                         orderby r.Seniority
                         select new SelectListItem
                         {
                             Value = r.RankId.ToString(),
                             Text = r.RankName.ToString()
                         };

            // List<SelectListItem> Ranks = NDCWebContext.RankMasters
            // .Where (w => w.ForParticipant == true && w.Service.Any(s=> searchIn.Contains(w.Service)))
            // //.Where(w => w.ForParticipant == true && w.Service.Any(st=> Service == "INTERNATIONAL STUDENT OFFICER" ? searchOc.Contains(w.Service) : searchIn.Contains(w.Service)))
            //.OrderBy(n => n.Seniority)
            //   .Select(n =>
            //   new SelectListItem
            //   {
            //       Value = n.RankId.ToString(),
            //       Text = n.RankName
            //   }).ToList();

            return new SelectList(result.ToList().Distinct(), "Value", "Text");
        }
        public NDCWebContext NDCWebContext
        {
            get { return Context as NDCWebContext; }
        }
    }
}