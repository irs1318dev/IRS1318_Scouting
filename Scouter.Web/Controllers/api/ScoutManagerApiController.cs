using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.FRCEvent;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Scouter.Web.Controllers.api
{
    public class ScoutManagerApiController : ApiController
	{
		private ApplicationUnit _unit = new ApplicationUnit();

        /// <summary>
        /// Gets all the team information for the requested match
        /// </summary>
        /// <param name="id">the match number</param>
        [HttpGet]
        public FRCMatchDataTransfer GetMatchData(int id)
        {
            int frceventID = _unit.CurrentScoutData.GetById(1).Event_ID;

            var query = from v in _unit.FRCMatches.GetAll()
                        where v.SequenceNumber == id && v.FRCEvent.Id == frceventID
                        select new FRCMatchDataTransfer()
                        {
                            BlueAlliance = new AllianceDataTransfer()
                            {
                                Color = Scouter.Models.AllianceColor.Blue,
                                Id = v.BlueAlliance.Id,
                                Team1 = v.BlueAlliance.Team1,
                                Team2 = v.BlueAlliance.Team2,
                                Team3 = v.BlueAlliance.Team3
                            },
                            RedAlliance = new AllianceDataTransfer()
                            {
                                Color = Scouter.Models.AllianceColor.Red,
                                Id = v.RedAlliance.Id,
                                Team1 = v.RedAlliance.Team1,
                                Team2 = v.RedAlliance.Team2,
                                Team3 = v.RedAlliance.Team3
                            },
                            SequenceNumber = v.SequenceNumber
                        };
            var match = query.First();

            return match;
        }

        /// <summary>
        /// Sets the match with the given info.
        /// Creates the match if it doesn't exist
        /// </summary>
        /// <param name="info">the information about the match</param>
        [HttpPut]
        public HttpResponseMessage SetMatch(MatchInfoDataTransfer info)
        {
            //get the competition ID
            int frcCompID = _unit.CurrentScoutData.GetById(1).Event_ID;

            //find the match
            int id = info.MatchNumber;
            var query = from v in _unit.FRCMatches.GetAll()
                        where v.SequenceNumber == id && v.FRCEvent.Id == frcCompID
                        select v;

            FRCMatch match = null;
            Alliance blue = null;
            Alliance red = null;

            //find the 1st blue team
            int blue1int = info.Blue1;
            var queryb1 = from v in _unit.Teams.GetAll()
                          where v.Number == blue1int
                          select v;
            var thing = queryb1.ToList();
            if (queryb1.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Blue1 + " does not exist"));


            //find the 2nd blue team
            int blue2int = info.Blue2;
            var queryb2 = from v in _unit.Teams.GetAll()
                          where v.Number == blue2int
                          select v;
            if (queryb2.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Blue2 + " does not exist"));


            //find the 3rd blue team
            int blue3int = info.Blue3;
            var queryb3 = from v in _unit.Teams.GetAll()
                          where v.Number == blue3int
                          select v;
            if (queryb3.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Blue3 + " does not exist"));


            //find the 1st red team
            int red1int = info.Red1;
            var queryr1 = from v in _unit.Teams.GetAll()
                          where v.Number == red1int
                          select v;
            if (queryr1.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Red1 + " does not exist"));


            //find the 2nd red team
            int red2int = info.Red2;
            var queryr2 = from v in _unit.Teams.GetAll()
                          where v.Number == red2int
                          select v;
            if (queryr2.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Red2 + " does not exist"));


            //find the 3rd red team
            int red3int = info.Red3;
            var queryr3 = from v in _unit.Teams.GetAll()
                          where v.Number == red3int
                          select v;
            if (queryr3.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Red3 + " does not exist"));

            //if the match doesn't exist
            if (query.Count() == 0)
            {
                //create it
                match = new FRCMatch()
                {
                    SequenceNumber = id,
                    FRCEvent = _unit.FRCCompetitions.GetById(frcCompID)
                };
                _unit.FRCMatches.Add(match);
                _unit.SaveChanges();
                _unit.FRCCompetitions.GetById(frcCompID).Matches.Add(match);

                blue = new Alliance()
                {
                    Match = match,
                    Color = AllianceColor.Blue
                };
                red = new Alliance()
                {
                    Match = match,
                    Color = AllianceColor.Red
                };
                _unit.Alliances.Add(blue);
                _unit.Alliances.Add(red);
                _unit.SaveChanges();

                match.BlueAlliance = blue;
                match.RedAlliance = red;
                _unit.SaveChanges();
            }
            else
            {
                //otherwise grab the first match
                match = query.First();
                blue = match.BlueAlliance;
                red = match.RedAlliance;
            }

            //grab the first found team
            red.Team1 = queryr1.First();
            red.Team2 = queryr2.First();
            red.Team3 = queryr3.First();
            blue.Team1 = queryb1.First();
            blue.Team2 = queryb2.First();
            blue.Team3 = queryb3.First();

            //set all the scout statuses to NoScout
            var scoutdata = _unit.CurrentScoutData.GetById(1);

            scoutdata.Blue1Status = ScoutStatus.NoScout;
            scoutdata.Blue2Status = ScoutStatus.NoScout;
            scoutdata.Blue3Status = ScoutStatus.NoScout;
            scoutdata.Red1Status = ScoutStatus.NoScout;
            scoutdata.Red2Status = ScoutStatus.NoScout;
            scoutdata.Red3Status = ScoutStatus.NoScout;
            //update the match ID
            scoutdata.Match_ID = match.Id;

            //update the match and alliances with the new match data
            _unit.FRCMatches.Update(match);
            _unit.Alliances.Update(red);
            _unit.Alliances.Update(blue);
            _unit.CurrentScoutData.Update(scoutdata);

            _unit.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Changes the current competition to the ID of an already existing one
        /// </summary>
        /// <param name="id">the ID of the FRCEvent</param>
        /// <param name="data"></param>
        [HttpPatch]
        public HttpResponseMessage ChangeCurrentCompetition(int id, CurrentScoutData data)
        {
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            if (id != data.Id)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            FRCCompetition existingFRCEvent = this._unit.FRCCompetitions.GetById(id);
            _unit.FRCCompetitions.Detach(existingFRCEvent);

            
            this._unit.CurrentScoutData.Add(data);

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
	}
}