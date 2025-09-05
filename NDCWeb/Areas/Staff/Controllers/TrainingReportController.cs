using NDCWeb.Areas.Admin.Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Infrastructure.Extensions;
using NDCWeb.Infrastructure.Filters;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace NDCWeb.Areas.Staff.Controllers
{
    [CSPLHeaders]
    [Authorize(Roles = CustomRoles.Staff)]
    [StaffStaticUserMenu]
    [EncryptedActionParameter]
    //[UserMenu(MenuArea = "Staff")]
    public class TrainingReportController : Controller
    {
        // GET: Staff/TrainingReport
        public async Task<ActionResult> ShowSpeechFeedbackSummary(int speechEventId)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                var speechfdbkSummary = uow.FeedbackSpeakerRepo.GetSpeechFeedbackSummaryRpt(speechEventId);
                var speechfdbk = await uow.FeedbackSpeakerRepo.GetSpeechFeedbackRpt(speechEventId, course.CourseId);
                if (speechfdbk != null)
                {
                    speechfdbkSummary.iSpeechFeedbackVM = speechfdbk.ToList();
                }
                return View(speechfdbkSummary);
            }  
        }
        public ActionResult ShowSpeechFeedbackNoting()
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {

                ViewBag.Subject = uow.SubjectMasterRepository.GetSubjects();
                return View();

            }
        }
        [HttpPost]
        public async Task<ActionResult> LoadSpeechFeedbackNoting(int subjectId, string fromDate, string toDate)
        {
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();

                if (string.IsNullOrEmpty(fromDate))
                    fromDate = "01 Jan 2020";
                if (string.IsNullOrEmpty(toDate))
                    toDate = "01 Dec 2022";
                
                DateTime FromDate = DateTime.ParseExact(fromDate, "dd MMM yyyy", null);
                DateTime ToDate = DateTime.ParseExact(toDate, "dd MMM yyyy", null);

                var speechfdbkNoting = await uow.FeedbackSpeakerRepo.GetSpeechFeedbackNoting(subjectId, FromDate, ToDate, course.CourseId);
                //return View(speechfdbkNoting);
                return Json(data: speechfdbkNoting, behavior: JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ConsolidatedSpeakerFeedback(string id)
        {
            id = id ?? "All";
            int courseid = 0;
            using (var uow = new UnitOfWork(new NDCWebContext()))
            {
                var course = uow.CourseRepo.Find(x => x.IsCurrent == true).OrderByDescending(x => x.CourseId).FirstOrDefault();
                courseid = course.CourseId;
            }
            DataSet ds = new DataSet();
            string constr = ConfigurationManager.ConnectionStrings["NDCWebConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand sqlComm = new SqlCommand("GetConsolidatedFeedback", con);
                sqlComm.Parameters.AddWithValue("@Subject", id);      

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                da.Fill(ds);

                ds.Tables[0].DefaultView.Sort = "[Locker No] ASC";
                //string query = @"";
                //using (SqlCommand cmd = new SqlCommand(query))
                //{
                //    cmd.Connection = con;
                //    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                //    {
                //        sda.Fill(ds);
                //    }
                //}
            }

            return View(ds);
        }
    }
}