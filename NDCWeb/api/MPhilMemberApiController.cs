using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace NDCWeb.api
{
    [RoutePrefix("api/mphilMembers")]
    [Authorize]
    public class MPhilMemberApiController : ApiController
    {
        // GET: api/MPhilMemberApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MPhilMemberApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MPhilMemberApi
        [HttpPost]
        [Route("Enrol")]
        public async Task<HttpResponseMessage> Enrol([FromBody]MPhilMember mphilMember)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (mphilMember == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.MPhilMemberRepo.Add(mphilMember);
                    await uow.CommitAsync();

                    var message = Request.CreateResponse(HttpStatusCode.Created, mphilMember);
                    //message.Headers.Location = new Uri(Request.RequestUri +"/"+ courseMember.CourseMemberId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [Route("Enrol2")]
        public HttpResponseMessage Enrol2()
        {
            try
            {
                HttpResponseMessage result = null;
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/MPhilMemberApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MPhilMemberApi/5
        public void Delete(int id)
        {
        }
    }
}
