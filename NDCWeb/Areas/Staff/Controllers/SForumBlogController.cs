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
    public class SForumBlogController : Controller
    {
        // GET: Staff/SForumBlog
        public ActionResult Index()
        {
            //string uId = User.Identity.GetUserId();
            //using (var uow = new UnitOfWork(new NDCWebContext()))
            //{
            //    var forumBlogs = uow.ForumBlogRepo.GetAll();
            //    var config = new MapperConfiguration(cfg =>
            //    {
            //        cfg.CreateMap<IEnumerable<ForumBlog>, List<ForumBlogIndxVM>>();
            //    });
            //    IMapper mapper = config.CreateMapper();
            //    var indexDto = mapper.Map<IEnumerable<ForumBlog>, IEnumerable<ForumBlogIndxVM>>(forumBlogs).ToList();
            //    return View(indexDto);
            //}
            string uId = User.Identity.GetUserId();
            using (NDCWebContext db = new NDCWebContext())
            {
                List<ForumBlog> fbm = db.ForumBlogs.ToList();
                List<CrsMemberPersonal> cmp = db.CrsMemberPersonals.ToList();
                List<Course> ca = db.Courses.ToList();
                List<StaffMaster> sm = db.StaffMasters.ToList();

                var forumblock = (from forumBlock in fbm
                                      join cmPersonal in cmp on forumBlock.CreatedBy equals cmPersonal.CreatedBy
                                      join cMaster in ca on cmPersonal.CourseId equals cMaster.CourseId
                                      join sMaster in sm on forumBlock.StaffId equals sMaster.StaffId into table1
                                      from sMaster in table1.ToList()
                                      where cMaster.IsCurrent == true
                                      orderby forumBlock.ForumBlogId descending
                                      select new ForumBlogIndxVM()
                                      {
                                          ForumBlogId = forumBlock.ForumBlogId,
                                          Category = forumBlock.Category,
                                          Description = forumBlock.Description,
                                          Module = forumBlock.Module, //Add by Amitesh
                                          iForumBlogMedias = forumBlock.iForumBlogMedias,
                                          MemberRemark = forumBlock.MemberRemark,
                                          StaffRemark = forumBlock.StaffRemark,
                                          Status = forumBlock.Status,
                                          StaffId = forumBlock.StaffId,
                                          StaffMasters = sMaster,
                                      }).ToList();
                return View(forumblock);
            }
        }
        public ActionResult ForumBlogEdit(int id)
        {
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                ViewBag.Staff = uow.StaffMasterRepo.GetStaff();
                ViewBag.Status = CustomDropDownList.GetForumBlogStatus();
                var forumblogdata = uow.ForumBlogRepo.GetById(id);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ForumBlog, ForumBlogUpVM>();
                });
                IMapper mapper = config.CreateMapper();
                ForumBlogUpVM CreateDto = mapper.Map<ForumBlog, ForumBlogUpVM>(forumblogdata);
                return View(CreateDto);
            }
        }
        [HttpPost]
        public async Task<ActionResult> ForumBlogEdit(ForumBlogUpVM objForumBlog)
        {
            ViewBag.GetForumBlogCat = CustomDropDownList.GetForumBlogCategory();
            
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var forumblogdata = uow.ForumBlogRepo.GetById(objForumBlog.ForumBlogId);
                forumblogdata.StaffRemark = objForumBlog.StaffRemark;
                forumblogdata.Status = objForumBlog.Status;
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<ForumBlog, ForumBlogUpVM>();
                //});
                //IMapper mapper = config.CreateMapper();
                //ForumBlog UpdateDto = mapper.Map<ForumBlogUpVM, ForumBlog>(objForumBlog);
                //uow.ForumBlogRepo.Update(UpdateDto);
                await uow.CommitAsync();
                this.AddNotification("Record Saved", NotificationType.SUCCESS);
                return RedirectToAction("Index");
            }
        }
    }
}