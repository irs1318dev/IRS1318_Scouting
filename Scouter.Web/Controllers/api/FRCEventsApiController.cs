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
        
        public IEnumerable<FRCCompetition> Get()
        {
            return _unit.FRCCompetitions.GetAll();   
        }

        public FRCCompetition Get(int id)
        {
            FRCCompetition FRCEvent = _unit.FRCCompetitions.GetById(id);
            if (FRCEvent == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return FRCEvent;
        }

        public HttpResponseMessage Put(int id, FRCCompetition FRCEvent)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != FRCEvent.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            FRCCompetition existingFRCEvent = this._unit.FRCCompetitions.GetById(id);
            _unit.FRCCompetitions.Detach(existingFRCEvent);

            // Keep the orginal CreatedOn value
            FRCEvent.CreatedOn = existingFRCEvent.CreatedOn;

            this._unit.FRCCompetitions.Update(FRCEvent);

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

        public HttpResponseMessage Post(FRCCompetition FRCEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.FRCCompetitions.Add(FRCEvent);
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
            FRCCompetition FRCEvent = this._unit.FRCCompetitions.GetById(id);
            if (FRCEvent == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Delete(FRCEvent);
        }

        public HttpResponseMessage Delete(FRCCompetition FRCEvent)
        {
            if (FRCEvent == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No FRCEvent specified.");

            this._unit.FRCCompetitions.Delete(FRCEvent);

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
