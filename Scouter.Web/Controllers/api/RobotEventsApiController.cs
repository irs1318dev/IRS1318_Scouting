using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Scouter.Data;
using Scouter.Models;

namespace Scouter.Web.Controllers
{
    public class RobotEventsApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        public IEnumerable<RobotEvent> Get()
        {
            return _unit.RobotEvents.GetAll();
        }

        public RobotEvent Get(int id)
        {
            RobotEvent RobotEvent = _unit.RobotEvents.GetById(id);
            if (RobotEvent == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return RobotEvent;
        }

        public HttpResponseMessage Put(int id, RobotEvent RobotEvent)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != RobotEvent.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            RobotEvent existingRobotEvent = this._unit.RobotEvents.GetById(id);
            _unit.RobotEvents.Detach(existingRobotEvent);

            // Keep the orginal CreatedOn value
            RobotEvent.CreatedOn = existingRobotEvent.CreatedOn;

            this._unit.RobotEvents.Update(RobotEvent);

            try
            {
                this._unit.SaveChanges();

                // Return an explicit value to avoid the fail callback being incorrectly invoked.
                return Request.CreateResponse(HttpStatusCode.OK, "{success: 'true', verb: 'PUT'}");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post(RobotEvent RobotEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.RobotEvents.Add(RobotEvent);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, RobotEvent);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = RobotEvent.Id }));

                    return result;
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

        public HttpResponseMessage Delete(int id)
        {
            RobotEvent RobotEvent = this._unit.RobotEvents.GetById(id);
            if (RobotEvent == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Delete(RobotEvent);
        }

        public HttpResponseMessage Delete(RobotEvent RobotEvent)
        {
            if (RobotEvent == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No RobotEvent specified.");

            this._unit.RobotEvents.Delete(RobotEvent);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, RobotEvent);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            this._unit.Dispose();
            base.Dispose(disposing);
        }
    }
}
