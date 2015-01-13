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
    public class FRCMatchApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();
        
        public IEnumerable<FRCMatch> Get()
        {
            return _unit.FRCMatches.GetAll();   
        }

        public FRCMatch Get(int id)
        {
            FRCMatch FRCMatch = _unit.FRCMatches.GetById(id);
            if (FRCMatch == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return FRCMatch;
        }

        public HttpResponseMessage Put(int id, FRCMatch FRCMatch)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != FRCMatch.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            FRCMatch existingFRCMatch = this._unit.FRCMatches.GetById(id);
            _unit.FRCMatches.Detach(existingFRCMatch);

            // Keep the orginal CreatedOn value
            FRCMatch.CreatedOn = existingFRCMatch.CreatedOn;

            this._unit.FRCMatches.Update(FRCMatch);

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

        public HttpResponseMessage Post(FRCMatch FRCMatch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.FRCMatches.Add(FRCMatch);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, FRCMatch);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = FRCMatch.Id }));

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
            FRCMatch FRCMatch = this._unit.FRCMatches.GetById(id);
            if (FRCMatch == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Delete(FRCMatch);
        }

        public HttpResponseMessage Delete(FRCMatch FRCMatch)
        {
            if (FRCMatch == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No FRCMatch specified.");

            this._unit.FRCMatches.Delete(FRCMatch);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, FRCMatch);
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
