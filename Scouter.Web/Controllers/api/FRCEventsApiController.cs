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
    public class FRCEventsApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();
        
        public IEnumerable<FRCEvent> Get()
        {
            return _unit.FRCEvents.GetAll();   
        }

        public FRCEvent Get(int id)
        {
            FRCEvent FRCEvent = _unit.FRCEvents.GetById(id);
            if (FRCEvent == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return FRCEvent;
        }

        public HttpResponseMessage Put(int id, FRCEvent FRCEvent)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != FRCEvent.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            FRCEvent existingFRCEvent = this._unit.FRCEvents.GetById(id);
            _unit.FRCEvents.Detach(existingFRCEvent);

            // Keep the orginal CreatedOn value
            FRCEvent.CreatedOn = existingFRCEvent.CreatedOn;

            this._unit.FRCEvents.Update(FRCEvent);

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
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post(FRCEvent FRCEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.FRCEvents.Add(FRCEvent);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, FRCEvent);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = FRCEvent.Id }));

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
            FRCEvent FRCEvent = this._unit.FRCEvents.GetById(id);
            if (FRCEvent == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Delete(FRCEvent);
        }

        public HttpResponseMessage Delete(FRCEvent FRCEvent)
        {
            if (FRCEvent == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No FRCEvent specified.");

            this._unit.FRCEvents.Delete(FRCEvent);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, FRCEvent);
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
