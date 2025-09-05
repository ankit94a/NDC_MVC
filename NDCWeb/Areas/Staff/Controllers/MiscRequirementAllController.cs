using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Constants;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Infrastructure.Helpers.FileDirectory;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Staff.Controllers
{
    [Authorize(Roles = CustomRoles.CandidateStaff)]
    [CSPLHeaders]
    [StaffStaticUserMenu]
    //[UserMenu(MenuArea = "Staff")]
    public class MiscRequirementAllController : Controller
    {
        // GET: Staff/MiscRequirementAll
        public ActionResult Index()
        {
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                List<TelecommRequirement> teles = db.TelecommRequirements.ToList();
                List<CrsMemberPersonal> crsmbrs = db.CrsMemberPersonals.ToList();
                List<CrsMbrAppointment> spses = db.CrsMbrAppointments.ToList();
                List<RankMaster> rms = db.RankMasters.ToList();

                //from order in OrderList
                //join cust in CustomerList on order.CustomerId equals cust.CustomerId
                //join p in ProductList on order.ItemId equals p.ProductId
                //orderby cust.CustomerId

                var telecommrecord = (from e in teles
                                      join d in crsmbrs on e.CreatedBy equals d.CreatedBy join i in spses on d.CreatedBy equals i.CreatedBy
                                      join j in rms on i.RankId equals j.RankId into table1
                                      from j in table1.ToList()
                                      orderby e.TelecommReqId descending
                                      select new TelecommRequirementIndexVM()
                                      {
                                          FullName = j.RankName + " " + d.FirstName + " " + d.Surname,
                                          Comments = e.Comments,
                                          HouseNo = e.HouseNo,
                                          ReqInternet = e.ReqInternet,
                                          ResidentialComplex = e.ResidentialComplex,
                                          TelecommReqId = e.TelecommReqId,
                                          TypeOfConnection = e.TypeOfConnection,
                                      }).ToList();
                return View(telecommrecord);
            }


            //using (var uow = new UnitOfWork(new NDCWebContext()))
            //{
            //    var miscReq = uow.TelecommRequirementRepository.GetAll();
            //    var config = new MapperConfiguration(cfg =>
            //    {
            //        cfg.CreateMap<IEnumerable<TelecommRequirement>, List<TelecommRequirementIndexVM>>();
            //    });
            //    IMapper mapper = config.CreateMapper();
            //    var indexDto = mapper.Map<IEnumerable<TelecommRequirement>, IEnumerable<TelecommRequirementIndexVM>>(miscReq).ToList();
            //    return View(indexDto);
            //}
        }
    }
}