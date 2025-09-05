using NDCWeb.Areas.Staff.View_Models;
using NDCWeb.Data_Contexts;
using NDCWeb.Models;
using NDCWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.RegularExpressions;
using NDCWeb.Areas.Admin.Models;

namespace NDCWeb.api
{
    [RoutePrefix("api/staffMasterApi")]//StaffMasterApi
    [Authorize(Roles = CustomRoles.Staff)]
    public class StaffMasterApiController : ApiController
    {
        [HttpPost]
        [Route("LockerAllotmentAdd")]
        public HttpResponseMessage LockerAllotmentAdd([FromBody]List<LockerAllotment> lockerAllotments)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (lockerAllotments == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.LockerAllotmentRepo.AddRange(lockerAllotments);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
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
        [Route("LockerAllotmentEdit/{id}")]
        public HttpResponseMessage LockerAllotmentEdit(int id, [FromBody] LockerAllotmentEditVM lockerAllotment)//int id, [FromBody]LockerAllotment lockerAllotment
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var uow = new UnitOfWork(new NDCWebContext()))
                    {
                        var entity = uow.LockerAllotmentRepo.GetById(id);
                        if (entity == null)
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                        else
                        {
                            entity.LockerNo = lockerAllotment.LockerNo;
                            entity.SDSId = lockerAllotment.SDSId;
                            entity.IAG = lockerAllotment.IAG;
                            entity.RolesAssign = lockerAllotment.RolesAssign;

                            uow.Commit();
                            return Request.CreateResponse(HttpStatusCode.Created);
                        }
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("LockerAllotmentEdit/locker/{lockerId}/uptype/{uptype}")]
        public HttpResponseMessage LockerAllotmentEditUptype([FromUri] int lockerId, [FromUri] string uptype, [FromBody] LockerAllotment lockerAllotment) //[FromUri] int lockerId, [FromUri] string uptype, [FromBody] LockerAllotment lockerAllotment
        {
            try 
            {
                Regex r = new Regex(@"^[a-zA-Z0-9]*$");
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.LockerAllotmentRepo.GetById(lockerId);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        if (uptype == null)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else if (uptype.ToUpper() == "AQ")
                        {
                            if(!String.IsNullOrEmpty(lockerAllotment.LockerNo))
                            {
                                if (Regex.Match(lockerAllotment.LockerNo, r.ToString()).Success)
                                {
                                    entity.LockerNo = lockerAllotment.LockerNo;
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not Valid Input.");
                                }
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Locker No is not supplied.");
                            }

                        }
                        else if (uptype.ToUpper() == "DS")
                        {
                            if (!String.IsNullOrEmpty(lockerAllotment.SDSId.ToString()) && !String.IsNullOrEmpty(lockerAllotment.IAG) && !String.IsNullOrEmpty(lockerAllotment.RolesAssign))
                            {
                                if (Regex.Match(lockerAllotment.RolesAssign, r.ToString()).Success)
                                {
                                    entity.SDSId = lockerAllotment.SDSId;
                                    entity.IAG = lockerAllotment.IAG;
                                    entity.RolesAssign = lockerAllotment.RolesAssign;
                                }
                                else
                                {
                                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Not Valid Input.");
                                }
                            }
                            else
                            {
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value not supplied.");
                            }

                        }
                        //else
                        //{
                        //    entity.LockerNo = lockerAllotment.LockerNo;
                        //    entity.SDSId = lockerAllotment.SDSId;
                        //    entity.IAG = lockerAllotment.IAG;
                        //    entity.RolesAssign = lockerAllotment.RolesAssign;
                        //}
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [Route("MessBillAdd")]
        public HttpResponseMessage MessBillAdd([FromBody] List<MessBill> Messbills)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    if (Messbills == null)
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "object is null");

                    uow.MessBillRepo.AddRange(Messbills);
                    uow.Commit();

                    var message = Request.CreateResponse(HttpStatusCode.Created);
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
        [Route("MessBillEdit/BillId/{BillId}/uptype/{uptype}")]
        public HttpResponseMessage MessBillEdit([FromUri] int BillId, [FromUri] string uptype, [FromBody] MessBill MessBill)
        {
            try
            {
                using (var uow = new UnitOfWork(new NDCWebContext()))
                {
                    var entity = uow.MessBillRepo.GetById(BillId);
                    if (entity == null)
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    else
                    {
                        if (uptype == null)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else if (uptype.ToUpper() == "JDSADM")
                        {
                            entity.PayStatus = MessBill.PayStatus;
                        }
                        
                        uow.Commit();
                        return Request.CreateResponse(HttpStatusCode.Created);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
