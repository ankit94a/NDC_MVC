using AutoMapper;
using Microsoft.AspNet.Identity;
using NDCWeb.Areas.Admin.Models;
using NDCWeb.Areas.Member.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NDCWeb.Areas.Member.Controllers
{
    [Authorize(Roles = CustomRoles.Candidate)]
    [UserMenu(MenuArea = "Member")]
    [CSPLHeaders]
    public class LibraryMembershipController : Controller
    {
        public ActionResult Index()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberships = uow.LibraryMembershipRepo.GetAll();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<LibraryMembership>, List<LibraryMembershipIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<IEnumerable<LibraryMembership>, IEnumerable<LibraryMembershipIndxVM>>(memberships).ToList();
                return View(indexDto);
            }
        }
        // GET: Member/LibraryMembership/Membership
        [Authorize(Roles = CustomRoles.Candidate)]
        public ActionResult Membership()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var memberships = uow.LibraryMembershipRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (memberships == null)
                {
                    return View("Create");
                }
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IEnumerable<LibraryMembership>, List<LibraryMembershipIndxVM>>();
                });
                IMapper mapper = config.CreateMapper();
                var indexDto = mapper.Map<LibraryMembership, LibraryMembershipIndxVM>(memberships);  //mapper.Map<IEnumerable<LibraryMembership>, IEnumerable<LibraryMembershipIndxVM>>(memberships).ToList();
                return View(indexDto);
            }
        }
        // GET: Member/LibraryMembership/Create
        [Authorize(Roles = CustomRoles.Candidate)]
        public ActionResult Create()
        {
            string uId = User.Identity.GetUserId();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                LibraryMembershipCrtVM objLibmember = new LibraryMembershipCrtVM();
                var memberships = uow.LibraryMembershipRepo.FirstOrDefault(x => x.CreatedBy == uId);
                var crsMemberPersonal = uow.CrsMbrPersonalRepo.FirstOrDefault(x => x.CreatedBy == uId);
                if (crsMemberPersonal == null)
                {
                    return Redirect("~/member");
                }
                if (memberships == null)
                {
                    var memberPersonal = uow.CrsMbrPersonalRepo.Find(x => x.CreatedBy == uId).OrderByDescending(x => x.CourseMemberId).FirstOrDefault();
                    var memberService = uow.CrsMbrAppointmentRepo.Find(x => x.CreatedBy == uId, fk=>fk.Ranks).FirstOrDefault();
                    if (memberPersonal != null)
                    {
                        objLibmember.MemberName = memberPersonal.FirstName + " " + memberPersonal.MiddleName + " " + memberPersonal.Surname;
                        objLibmember.Address = memberPersonal.CommunicationAddress;
                        objLibmember.MobileNo = memberPersonal.MobileNo;
                        objLibmember.EmailId = memberPersonal.EmailId;

                        var lockerAllotment = uow.LockerAllotmentRepo.FirstOrDefault(x => x.CourseMemberId == memberPersonal.CourseMemberId);
                        if (lockerAllotment != null)
                        {
                            objLibmember.LockerNo = lockerAllotment.LockerNo;
                        }
                    }
                    if (memberService!=null)
                    {
                        objLibmember.Designation = memberService.Designation;
                    }
                    return View("Create", objLibmember);
                }
                else
                {
                    return RedirectToAction("Membership");
                    //return View();
                }
            }
        }

        // POST: Member/LibraryMembership/Create
        [Authorize(Roles = CustomRoles.Candidate)]
        [HttpPost]
        public async Task<ActionResult> Create(LibraryMembershipCrtVM objLibraryMembership)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LibraryMembershipCrtVM, LibraryMembership>();
                });
                IMapper mapper = config.CreateMapper();
                LibraryMembership CreateDto = mapper.Map<LibraryMembershipCrtVM, LibraryMembership>(objLibraryMembership);
                uow.LibraryMembershipRepo.Add(CreateDto);
                await uow.CommitAsync();
                return RedirectToAction("Membership");
            }
        }
        // GET: Member/LibraryMembership/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/LibraryMembership/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/LibraryMembership/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/LibraryMembership/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
