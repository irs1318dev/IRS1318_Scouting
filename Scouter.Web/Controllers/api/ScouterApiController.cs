using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.FRCEvent;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Scouter.Web.Controllers.api
{
    public class ScouterApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();

        /// <summary>
        /// Gets the current information about all the scouts, including status, match number, and team number
        /// </summary>
        [HttpGet]
        public ScoutDataTransfer GetScoutData()
        {
            var data = _unit.CurrentScoutData.GetById(1);


            return new ScoutDataTransfer()
            {
                Blue1 = data.Blue1,
                Blue1Status = data.Blue1Status,
                Blue2 = data.Blue2,
                Blue2Status = data.Blue2Status,
                Blue3 = data.Blue3,
                Blue3Status = data.Blue3Status,
                Red1 = data.Red1,
                Red1Status = data.Red1Status,
                Red2 = data.Red2,
                Red2Status = data.Red2Status,
                Red3 = data.Red3,
                Red3Status = data.Red3Status,
                Blue1Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Blue1Match != null) ? data.Blue1Match.Id : 0,
                    SequenceNumber = (data.Blue1Match != null) ? data.Blue1Match.SequenceNumber : 0
                },
                Blue2Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Blue2Match != null) ? data.Blue2Match.Id : 0,
                    SequenceNumber = (data.Blue2Match != null) ? data.Blue2Match.SequenceNumber : 0
                },
                Blue3Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Blue3Match != null) ? data.Blue3Match.Id : 0,
                    SequenceNumber = (data.Blue3Match != null) ? data.Blue3Match.SequenceNumber : 0
                },
                Red1Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Red1Match != null) ? data.Red1Match.Id : 0,
                    SequenceNumber = (data.Red1Match != null) ? data.Red1Match.SequenceNumber : 0
                },
                Red2Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Red2Match != null) ? data.Red2Match.Id : 0,
                    SequenceNumber = (data.Red2Match != null) ? data.Red2Match.SequenceNumber : 0
                },
                Red3Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Red3Match != null) ? data.Red3Match.Id : 0,
                    SequenceNumber = (data.Red3Match != null) ? data.Red3Match.SequenceNumber : 0
                },
                Match_ID = data.Match_ID,
                MatchNumber = _unit.FRCMatches.GetById(data.Match_ID).SequenceNumber,
                Human1Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Human1Match != null) ? data.Human1Match.Id : 0,
                    SequenceNumber = (data.Human1Match != null) ? data.Human1Match.SequenceNumber : 0
                },
                Human2Match = new Models.FRCEvent.FRCMatchDataTransfer()
                {
                    Id = (data.Human2Match != null) ? data.Human2Match.Id : 0,
                    SequenceNumber = (data.Human2Match != null) ? data.Human2Match.SequenceNumber : 0
                },
                Human1ScoutStatus = data.Human1Status,
                Human2ScoutStatus = data.Human2Status
            };
        }

        /// <summary>
        /// Saves a stack event to the database
        /// </summary>
        /// <param name="stackEvent">the stack event to add to the data base</param>
        [HttpPost]
        public HttpResponseMessage SaveStackEvent(StackEventDataTransfer stackEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var scoutData = _unit.CurrentScoutData.GetById(1);
                    Team team = null;
                    switch (stackEvent.Scouter_Id)
                    {
                        case 1:
                            team = scoutData.Red1;
                            break;
                        case 2:
                            team = scoutData.Red2;
                            break;
                        case 3:
                            team = scoutData.Red3;
                            break;
                        case 4:
                            team = scoutData.Blue1;
                            break;
                        case 5:
                            team = scoutData.Blue2;
                            break;
                        case 6:
                            team = scoutData.Blue3;
                            break;
                    }
                    StackEvent se = new StackEvent()
                    {
                        IsContainerAdded = stackEvent.IsContainerAdded,
                        NumTotesAdded = stackEvent.NumTotesAdded,
                        StartingHeight = stackEvent.StartingHeight,
                        Match = _unit.FRCMatches.GetById(scoutData.Match_ID),
                        Team = team
                    };

                    this._unit.StackEvents.Add(se);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.NoContent);

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

        /// <summary>
        /// Deletes the last Stack event from the scouter
        /// </summary>
        /// <param name="id">The ID of the scouter</param>
        [HttpDelete]
        public HttpResponseMessage UndoStack(int id)
        {
            var scoutData = _unit.CurrentScoutData.GetById(1);
            int teamId = 0;
            FRCMatch match = null;
            switch (id)
            {
                case 1:
                    teamId = scoutData.Red1.Id;
                    match = scoutData.Red1Match;
                    break;
                case 2:
                    teamId = scoutData.Red2.Id;
                    match = scoutData.Red2Match;
                    break;
                case 3:
                    teamId = scoutData.Red3.Id;
                    match = scoutData.Red3Match;
                    break;
                case 4:
                    teamId = scoutData.Blue1.Id;
                    match = scoutData.Blue1Match;
                    break;
                case 5:
                    teamId = scoutData.Blue2.Id;
                    match = scoutData.Blue2Match;
                    break;
                case 6:
                    teamId = scoutData.Blue3.Id;
                    match = scoutData.Blue3Match;
                    break;
            }
            var query = from r in _unit.StackEvents.GetAll()
                        where r.Team.Id == teamId && r.Match.Id == match.Id
                        select r;

            if (query.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception("Could not find a stack event from scout " + id + " in match " + match.SequenceNumber));

            var list = query.ToList();
            _unit.StackEvents.Delete(list.Last().Id);
            _unit.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

    }
}