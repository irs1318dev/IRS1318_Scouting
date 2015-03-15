using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Scouter.Web.Controllers.api
{
    public class ScoutDataApiController : ApiController
    {
        private ApplicationUnit _unit = new ApplicationUnit();
        RobotEventType[] booleans = { RobotEventType.TotesStacked,
                                      RobotEventType.RightToteMoved,
                                      RobotEventType.CenterToteMoved,
                                      RobotEventType.LeftToteMoved,
                                      RobotEventType.RightContainerMoved,
                                      RobotEventType.CenterContainerMoved,
                                      RobotEventType.LeftContainerMoved,
                                      RobotEventType.LeftContainerFromStep,
                                      RobotEventType.LeftCenterContainerFromStep,
                                      RobotEventType.RightCenterContainerFromStep,
                                      RobotEventType.RightContainerFromStep,
                                      RobotEventType.AutonomousMoved,
                                      RobotEventType.NoAutonomous,
                                      RobotEventType.AutoAttemptClutter,
                                      RobotEventType.CoopertitionToteFour,
                                      RobotEventType.CoopertitionToteThree,
                                      RobotEventType.CoopertitionToteTwo,
                                      RobotEventType.CoopertitionToteOne,
                                      };
        
        //GetScoutData is in ScouterApiController

        /// <summary>
        /// Gets the current event count for the given scouter
        /// </summary>
        /// <param name="id">the scouter</param>
        /// <returns>the count for every event</returns>
        [HttpGet]
        public ScoutCounter CurrentEventCount(int id)
        {
            var scoutData = _unit.CurrentScoutData.GetById(1);
            ScoutStatus scoutStatus = ScoutStatus.NoScout;
            Team team = null;
            FRCMatch match = null;
            RobotMode robotMode = RobotMode.Teleop;

            switch (id)
            {
                case 1:
                    scoutStatus = scoutData.Red1Status;
                    team = scoutData.Red1;
                    match = scoutData.Red1Match;
                    break;
                case 2:
                    scoutStatus = scoutData.Red2Status;
                    team = scoutData.Red2;
                    match = scoutData.Red2Match;
                    break;
                case 3:
                    scoutStatus = scoutData.Red3Status;
                    team = scoutData.Red3;
                    match = scoutData.Red3Match;
                    break;
                case 4:
                    scoutStatus = scoutData.Blue1Status;
                    team = scoutData.Blue1;
                    match = scoutData.Blue1Match;
                    break;
                case 5:
                    scoutStatus = scoutData.Blue2Status;
                    team = scoutData.Blue2;
                    match = scoutData.Blue2Match;
                    break;
                case 6:
                    scoutStatus = scoutData.Blue3Status;
                    team = scoutData.Blue3;
                    match = scoutData.Blue3Match;
                    break;
                default:
                    throw new ArgumentException("Scout ID must be between 1 and 6");
            }

            if (scoutStatus == ScoutStatus.NoScout)
                throw new Exception("No scout with ID: " + id);

            if (scoutStatus == ScoutStatus.Autonomous)
                robotMode = RobotMode.Autonomous;



            ScoutCounter count = new ScoutCounter();

            var query = from e in _unit.RobotEvents.GetAll()
                        where e.Match.Id == match.Id &&
                        e.Team.Id == team.Id
                        select e;

            RobotEvent[] events = query.ToArray();

            foreach (RobotEvent e in events)
            {
                switch (e.RobotEventType)
                {
                    case RobotEventType.TotesStacked: //change to BOOLEAN
                        if (e.RobotMode == robotMode)
                            count.TotesStacked = true;
                        break;
                    //THINGS MOVED TO AUTO ZONE
                    case RobotEventType.RightToteMoved:
                        if (e.RobotMode == robotMode)
                            count.RightToteMoved = true;
                        break;
                    case RobotEventType.CenterToteMoved:
                        if (e.RobotMode == robotMode)
                            count.CenterToteMoved = true;
                        break;
                    case RobotEventType.LeftToteMoved:
                        if (e.RobotMode == robotMode)
                            count.LeftToteMoved = true;
                        break;
                    case RobotEventType.RightContainerMoved:
                        if (e.RobotMode == robotMode)
                            count.RightContainerMoved = true;
                        break;
                    case RobotEventType.CenterContainerMoved:
                        if (e.RobotMode == robotMode)
                            count.CenterContainerMoved = true;
                        break;
                    case RobotEventType.LeftContainerMoved:
                        if (e.RobotMode == robotMode)
                            count.LeftContainerMoved = true;
                        break;
                    
                    // CONTAINERS TAKEN FROM STEP
                    case RobotEventType.RightContainerFromStep:
                        count.RightContainerFromStep = true;
                        break;
                    case RobotEventType.RightCenterContainerFromStep:
                        count.RightCenterContainerFromStep = true;
                        break;
                    case RobotEventType.LeftCenterContainerFromStep:
                        count.LeftCenterContainerFromStep = true;
                        break;
                    case RobotEventType.LeftContainerFromStep:
                        count.LeftContainerFromStep = true;
                        break;

                    //AUTONOMOUS EVENTS
                    case RobotEventType.AutonomousMoved:
                        if (e.RobotMode == robotMode)
                            count.AutonomousMoved = true;
                        break;
                    case RobotEventType.NoAutonomous:
                        if (e.RobotMode == robotMode)
                            count.NoAutonomous = true;
                        break;
                    case RobotEventType.AutoAttemptClutter:
                        if (e.RobotMode == robotMode)
                            count.AutoAttemptClutter = true;
                        break;
                    case RobotEventType.Foul:
                        if (e.RobotMode == robotMode)
                            ++count.Foul;
                        break;

                    //CHANGES IF 4 COOP Evnts
                    case RobotEventType.CoopertitionToteOne:
                        if (e.RobotMode == robotMode)
                            count.CoopertitionToteOne = true;
                        break;
                    case RobotEventType.CoopertitionToteTwo:
                        if (e.RobotMode == robotMode)
                            count.CoopertitionToteTwo = true;
                        break;
                    case RobotEventType.CoopertitionToteThree:
                        if (e.RobotMode == robotMode)
                            count.CoopertitionToteThree = true;
                        break;
                    case RobotEventType.CoopertitionToteFour:
                        if (e.RobotMode == robotMode)
                            count.CoopertitionToteFour = true;
                        break;

                    case RobotEventType.RightChutePickUp:
                        if (e.RobotMode == robotMode)
                            ++count.RightChutePickUp;
                        break;
                    case RobotEventType.LeftChutePickUp:
                        if (e.RobotMode == robotMode)
                            ++count.LeftChutePickUp;
                        break;
                    case RobotEventType.GroundPickUp:
                        if (e.RobotMode == robotMode)
                            ++count.GroundPickUp;
                        break;
                    case RobotEventType.BulldozeLitterToLandfill:
                        if (e.RobotMode == robotMode)
                            ++count.BulldozeLitterToLandfill;
                        break;

                    //ASSISTS
                    case RobotEventType.OrientContainer:
                        if (e.RobotMode == robotMode)
                            ++count.OrientContainer;
                        break;
                    case RobotEventType.OrientTote:
                        if (e.RobotMode == robotMode)
                            ++count.OrientTote;
                        break;
                    case RobotEventType.ClearContainer:
                        if (e.RobotMode == robotMode)
                            ++count.ClearContainer;
                        break;
                    case RobotEventType.ClearTote:
                        if (e.RobotMode == robotMode)
                            ++count.ClearTote;
                        break;
                    case RobotEventType.ClearLitter:
                        if (e.RobotMode == robotMode)
                            ++count.ClearLitter;
                        break;

                    //case RobotEventType.DriveOverPlatform:
                    //    if (e.RobotMode == robotMode)
                    //        ++count.DriveOverPlatform;
                    //    break;
                }
            }

            return count;
        }

        /// <summary>
        /// Updates the status, match #, and team # of a scouter
        /// </summary>
        /// <param name="info">the info of the scouter</param>
        [HttpPatch]
        public HttpResponseMessage UpdateScoutData(ScoutUpdateInfo info)
        {
            if (info.Scouter > 8 || info.Scouter < 1)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new IndexOutOfRangeException("scouter must be between 1 and 8"));

            var scoutInfo = _unit.CurrentScoutData.GetById(1);
            var match = _unit.FRCMatches.GetById(info.Match_Id);
            if (match == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception("No match with ID " + info.Match_Id + "was found"));

            switch (info.Scouter)
            {
                case 1:
                    scoutInfo.Red1Match = match;
                    scoutInfo.Red1Status = info.ScouterStatus;
                    scoutInfo.Red1 = _unit.Teams.GetById(info.Team_Id);
                    break;
                case 2:
                    scoutInfo.Red2Match = match;
                    scoutInfo.Red2Status = info.ScouterStatus;
                    scoutInfo.Red2 = _unit.Teams.GetById(info.Team_Id);
                    break;
                case 3:
                    scoutInfo.Red3Match = match;
                    scoutInfo.Red3Status = info.ScouterStatus;
                    scoutInfo.Red3 = _unit.Teams.GetById(info.Team_Id);
                    break;
                case 4:
                    scoutInfo.Blue1Match = match;
                    scoutInfo.Blue1Status = info.ScouterStatus;
                    scoutInfo.Blue1 = _unit.Teams.GetById(info.Team_Id);
                    break;
                case 5:
                    scoutInfo.Blue2Match = match;
                    scoutInfo.Blue2Status = info.ScouterStatus;
                    scoutInfo.Blue2 = _unit.Teams.GetById(info.Team_Id);
                    break;
                case 6:
                    scoutInfo.Blue3Match = match;
                    scoutInfo.Blue3Status = info.ScouterStatus;
                    scoutInfo.Blue3 = _unit.Teams.GetById(info.Team_Id);
                    break;
                case 7:
                    scoutInfo.Human1Match = match;
                    scoutInfo.Human1Status = info.ScouterStatus;
                    break;
                case 8:
                    scoutInfo.Human2Match = match;
                    scoutInfo.Human2Status = info.ScouterStatus;
                    break;
            }

            try
            {
                _unit.SaveChanges();
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        private bool RemoveExistingEvent(int teamId, int matchId, RobotEventType type)
        {
            var q = from r in _unit.RobotEvents.GetAll()
                    where r.Team.Id == teamId &&
                          r.Match.Id == matchId &&
                          r.RobotEventType == type
                    select r;

            if (q.Count() > 0)
            {
                var list = q.ToList();
                while (list.Count > 0 )
                {
                    _unit.RobotEvents.Delete(list[0].Id);
                    list.RemoveAt(0);
                }
                _unit.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Saves a robot event to the database
        /// </summary>
        /// <param name="robotEvent">the robot event</param>
        [HttpPost]
        public HttpResponseMessage SaveRobotEvent(RobotEventDataTransfer robotEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var scoutData = _unit.CurrentScoutData.GetById(1);
                    Team team = null;
                    var match = _unit.FRCMatches.GetById(scoutData.Match_ID);
                    switch (robotEvent.Scouter_Id)
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

                    //check to see if event is boolean
                    //if yes, then try to remove it - if success, return; else create it
                    foreach(RobotEventType type in booleans)
                    {
                        if(robotEvent.RobotEventType == type)
                        {
                            if (RemoveExistingEvent(team.Id, match.Id, type))
                                return Request.CreateResponse(HttpStatusCode.NoContent);
                            break;
                        }
                    }

                    RobotEvent re = new RobotEvent();
                    re.RobotEventType = robotEvent.RobotEventType;
                    re.RobotMode = robotEvent.RobotMode;
                    re.Match = match;
                    re.Team = team;
                    this._unit.RobotEvents.Add(re);
                    this._unit.SaveChanges();

                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.Created, robotEvent);

                    result.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = robotEvent.Id }));

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
        /// Deletes the last Robot event from the scouter
        /// </summary>
        /// <param name="id">The ID of the scouter</param>
        [HttpDelete]
        public HttpResponseMessage Undo(int id)
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
            var query = from r in _unit.RobotEvents.GetAll()
                        where r.Team.Id == teamId && r.Match.Id == match.Id
                        select r;

            if (query.Count() < 1)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, new Exception("Could not find a robot event from scout " + id + " in match " + match.SequenceNumber));

            var list = query.ToList();
            _unit.RobotEvents.Delete(list.Last().Id);
            _unit.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            this._unit.Dispose();
            base.Dispose(disposing);
        }
    }
}