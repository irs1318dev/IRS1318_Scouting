﻿using Scouter.Data;
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
            }

            //TODO: Add queries to get information here

            return new ScoutCounter()
            {
                TotesStacked = 0,
                RightToteMoved = 0,
                CenterToteMoved = 0,
                LeftToteMoved = 0,
                YellowTotesMovedToStep = 0,

                ContainersFromStep = 0,
                RightContainerMoved = 0,
                CenterContainerMoved = 0,
                LeftContainerMoved = 0,

                AutonomousMoved = false,
                NoAutonomous = false,
                AutoAttemptClutter = false,
                AutoFoul = 0,

                ChutePickUp = 0,
                GroundPickUp = 0,
                DriveOverPlatform = 0,
                HumanPlayerShoots = 0,

                OrientContainer = 0,
                OrientTote = 0,
                ClearContainer = 0,
                ClearTote = 0,
                ClearLitter = 0,

                //eek will change
                //TotesPlacedOnExistingCoopertition = 0,
                //TotesPlacedOnExistingStack = 0,
                //ContainerPlacedAtHeight = 0,

                LitterPlacedAtHeight = 0,
                BulldozeLitterToLandfill = 0,
                TeleopFoul = 0



            };
        }

        /// <summary>
        /// Updates the status, match #, and team # of a scouter
        /// </summary>
        /// <param name="info">the info of the scouter</param>
        [HttpPatch]
        public HttpResponseMessage UpdateScoutData(ScoutUpdateInfo info)
        {
            if (info.Scouter > 6 || info.Scouter < 1)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new IndexOutOfRangeException("scouter must be between 1 and 6"));

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

        /// <summary>
        /// Saves a robot event to the database
        /// </summary>
        /// <param name="robotEvent">the robot event</param>
        /// <returns></returns>
        [HttpPost]
		public HttpResponseMessage SaveRobotEvent(RobotEventDataTransfer robotEvent)
		{
            try
            {
                if (ModelState.IsValid)
                {
					var scoutData = _unit.CurrentScoutData.GetById(1);
					Team team = null;
					switch(robotEvent.Scouter_Id)
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
					RobotEvent re = new RobotEvent();
					re.GoalWasHot = robotEvent.GoalWasHot;
					re.RobotEventType = robotEvent.RobotEventType;
					re.RobotMode = robotEvent.RobotMode;
					re.Match = _unit.FRCMatches.GetById(scoutData.Match_ID);
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
			switch(id)
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