using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using WebApplication5.Models;
using Microsoft.AspNet.Identity;

namespace WebApplication5.Controllers
{
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("Requests")]
    [Authorize]
    public class RequestsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        [ResponseType(typeof(List<Request>)), Route("UserRequests")]
        public IHttpActionResult GetUserRequest()
        {
            var loggedUserID = User.Identity.GetUserId();
            var requests = db.Requests.Where(a => a.RequesterID == loggedUserID).Include("VacType").Include("requestStatus").ToList();
            if (requests == null)
            {
                return NotFound();
            }

            return Ok(requests);
        }
        [Route("MgrRequests")]
        public IHttpActionResult GetMgrRequest()
        {
            var reqList = new List<Request>();   
            
            var loggedUserID = User.Identity.GetUserId();
            var requests = db.Requests.Where(a => a.MngrID == loggedUserID).Include("vacType").ToList();







            if (requests == null)
            {
                return NotFound();
            }

            return Ok(requests);
        }



        #region Get all & byID
        //// GET: api/Requests
        //public IQueryable<Request> GetRequests()
        //{
        //    return db.Requests;
        //}

        //// GET: api/Requests/5
        //[ResponseType(typeof(Request))]
        //public IHttpActionResult GetRequest(int id)
        //{
        //    Request request = db.Requests.Find(id);
        //    if (request == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(request);
        //} 

        #endregion

        [Route("acceptRequest/{id}")]
        public IHttpActionResult GetAcceptLeaveReq([FromUri]int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var request = db.Requests.FirstOrDefault(r => r.ID == id);
                var LeaveDuration = request.To.Subtract(request.From).Days;

                request.ReqStatusID = (int)ReqStatus.Approved;

                var user = db.Users.FirstOrDefault(u => u.Id == request.RequesterID);
                // create method to chech balance of type... with respect to requested vac type
                switch (request.VacTypeID)
                {
                    case (int)VacType.Annual:
                        {
                            if (user.AnnualBalance == null || user.AnnualBalance < LeaveDuration)
                            {
                                return BadRequest("Balance not enough");
                            }
                            user.AnnualBalance -= LeaveDuration; break;

                        }
                    case (int)VacType.Sudden:
                        {
                            if (user.SuddenBalance == null || user.SuddenBalance < LeaveDuration)
                            {
                                return BadRequest("Balance not enough");
                            }
                            user.SuddenBalance -= LeaveDuration; break;
                        }
                    case (int)VacType.Sick:
                        {
                            if (user.SickBalance == null || user.SickBalance < LeaveDuration)
                            {
                                return BadRequest("Balance not enough");
                            }
                            user.SickBalance -= LeaveDuration; break;
                        }
                }

                db.SaveChanges();
            }
            return Ok();

        }



        // PUT: api/Requests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRequest(int id, Request request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != request.ID)
            {
                return BadRequest();
            }

            db.Entry(request).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/Requests
        [ResponseType(typeof(Request))]
        public IHttpActionResult PostRequest(Request request)
        {
            if (!ModelState.IsValid || !(request.From <= request.To))
            {
                return BadRequest(ModelState);
            }

            var loggedUserID = User.Identity.GetUserId();
            var prevRequests = db.Requests.Where(a => a.RequesterID == loggedUserID).ToList();
            if (prevRequests.Count > 0)
            {
                foreach (var item in prevRequests)
                {
                    if ((item.From < request.To && request.To <= item.To) || (item.From < request.From && request.From < item.To))
                    {
                        return BadRequest("There is an overlapping in your request");
                    }
                }
            }
            var requester = User.Identity.GetUserId();
            request.RequesterID = requester;
            request.MngrID = db.Users.FirstOrDefault(u => u.Id == requester).MngrID;
            request.ReqStatusID = (int)ReqStatus.Pending;

            db.Requests.Add(request);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = request.ID }, request);
        }

        // DELETE: api/Requests/5
        [ResponseType(typeof(Request))]
        public IHttpActionResult DeleteRequest(int id)
        {
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return NotFound();
            }

            db.Requests.Remove(request);
            db.SaveChanges();

            return Ok(request);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestExists(int id)
        {
            return db.Requests.Count(e => e.ID == id) > 0;
        }
    }
}