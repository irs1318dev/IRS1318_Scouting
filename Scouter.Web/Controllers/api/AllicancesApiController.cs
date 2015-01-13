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
    public class AllicancesApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        public IEnumerable<Alliance> Get()
        {
            return _unit.Alliances.GetAll();
        }

        public Alliance Get(int id)
        {
            Alliance Alliance = _unit.Alliances.GetById(id);
            if (Alliance == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return Alliance;
        }

        public HttpResponseMessage Put(int id, Alliance Alliance)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != Alliance.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Alliance existingAlliance = this._unit.Alliances.GetById(id);
            _unit.Alliances.Detach(existingAlliance);

            // Keep the orginal CreatedOn value
            Alliance.CreatedOn = existingAlliance.CreatedOn;

            this._unit.Alliances.Update(Alliance);

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

        public HttpResponseMessage Post(Alliance Alliance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.Alliances.Add(Alliance);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, Alliance);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = Alliance.Id }));

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
            Alliance Alliance = this._unit.Alliances.GetById(id);
            if (Alliance == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Delete(Alliance);
        }

        public HttpResponseMessage Delete(Alliance Alliance)
        {
            if (Alliance == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No Alliance specified.");

            this._unit.Alliances.Delete(Alliance);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, Alliance);
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
