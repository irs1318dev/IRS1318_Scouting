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
    public class TeamsApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();
        
        public IEnumerable<Team> Get()
        {
            return _unit.Teams.GetAll();   
        }

        public Team Get(int id)
        {
            Team team = _unit.Teams.GetById(id);
            if (team == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return team;
        }

        public HttpResponseMessage Put(int id, Team team)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != team.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            Team existingTeam = this._unit.Teams.GetById(id);
            _unit.Teams.Detach(existingTeam);

            // Keep the orginal CreatedOn value
            team.CreatedOn = existingTeam.CreatedOn;

            this._unit.Teams.Update(team);

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

        public HttpResponseMessage Post(Team team)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._unit.Teams.Add(team);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, team);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = team.Id }));

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
            Team team = this._unit.Teams.GetById(id);
            if (team == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Delete(team);
        }

        public HttpResponseMessage Delete(Team team)
        {
            if (team == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No team specified.");

            this._unit.Teams.Delete(team);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, team);
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
