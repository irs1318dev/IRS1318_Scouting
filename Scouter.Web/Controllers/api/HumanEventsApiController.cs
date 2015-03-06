using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.Scouting;


namespace Scouter.Web.Controllers
{
    public class HumanEventsApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        public IEnumerable<HumanEvent> Get()
        {
            return _unit.HumanEvents.GetAll();
        }

        public HumanEvent Get(int id)
        {
            HumanEvent HumanEvent = _unit.HumanEvents.GetById(id);
            if (HumanEvent == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return HumanEvent;
        }

        public HttpResponseMessage Put(int id, HumanEvent HumanEvent)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != HumanEvent.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            RobotEvent existingRobotEvent = this._unit.RobotEvents.GetById(id);
            _unit.RobotEvents.Detach(existingRobotEvent);

            // Keep the orginal CreatedOn value
            HumanEvent.CreatedOn = existingRobotEvent.CreatedOn;

            this._unit.HumanEvents.Update(HumanEvent);

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

        public HttpResponseMessage Post(HumanEventDataTransfer humanEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var scoutData = _unit.CurrentScoutData.GetById(1);
                    var match = _unit.FRCMatches.GetById(scoutData.Match_ID);
                    Team team = null;
                    switch (humanEvent.Scouter_Id)
                    {
                        case 1:
                            if (humanEvent.Team_Number == 1)
                                team = match.RedAlliance.Team1;
                            else if (humanEvent.Team_Number == 2)
                                team = match.RedAlliance.Team2;
                            else if (humanEvent.Team_Number == 3)
                                team = match.RedAlliance.Team3;
                            break;
                        case 2:
                            if (humanEvent.Team_Number == 1)
                                team = match.BlueAlliance.Team1;
                            else if (humanEvent.Team_Number == 2)
                                team = match.BlueAlliance.Team2;
                            else if (humanEvent.Team_Number == 3)
                                team = match.BlueAlliance.Team3;
                            break;
                    }
                    HumanEvent re = new HumanEvent();
                    re.HumanEventType = humanEvent.HumanEventType;
                    re.Match = match;
                    re.Team = team;
                    this._unit.HumanEvents.Add(re);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, humanEvent);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = humanEvent.Id }));

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

        [HttpDelete]
        public HttpResponseMessage Undo(int id)
        {
            var scoutData = _unit.CurrentScoutData.GetById(1);
            int team1Id = 0;
            int team2Id = 0;
            int team3Id = 0;
            FRCMatch match = _unit.FRCMatches.GetById(scoutData.Match_ID);
            switch (id)
            {
                case 1:
                    team1Id = match.RedAlliance.Team1.Id;
                    team2Id = match.RedAlliance.Team2.Id;
                    team3Id = match.RedAlliance.Team3.Id;
                    break;
                case 2:
                    team1Id = match.BlueAlliance.Team1.Id;
                    team2Id = match.BlueAlliance.Team2.Id;
                    team3Id = match.BlueAlliance.Team3.Id;
                    break;
            }
            var query = from r in _unit.HumanEvents.GetAll()
                        where (r.Team.Id == team1Id ||
                               r.Team.Id == team2Id ||
                               r.Team.Id == team3Id) &&
                               r.Match.Id == match.Id
                        select r;

            if (query.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception("Could not find a human event from scout " + id + " in match " + match.SequenceNumber));

            var list = query.ToList();
            _unit.HumanEvents.Delete(list.Last().Id);
            _unit.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Delete(HumanEvent HumanEvent)
        {
            if (HumanEvent == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No RobotEvent specified.");

            this._unit.HumanEvents.Delete(HumanEvent);

            try
            {
                this._unit.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, HumanEvent);
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