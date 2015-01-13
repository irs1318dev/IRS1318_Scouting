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
using System.Web.Mvc;

namespace Scouter.Web.Controllers.api
{
    public class ScoutDataApiController : ApiController
    {
		private ApplicationUnit _unit = new ApplicationUnit();

		public ScoutDataTransfer Get()
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
					Id = data.Blue1Match.Id,
					SequenceNumber = data.Blue1Match.SequenceNumber
				},
				Blue2Match = new Models.FRCEvent.FRCMatchDataTransfer()
				{
					Id = data.Blue2Match.Id,
					SequenceNumber = data.Blue2Match.SequenceNumber
				},
				Blue3Match = new Models.FRCEvent.FRCMatchDataTransfer()
				{
					Id = data.Blue3Match.Id,
					SequenceNumber = data.Blue3Match.SequenceNumber
				},
				Red1Match = new Models.FRCEvent.FRCMatchDataTransfer()
				{
					Id = data.Red1Match.Id,
					SequenceNumber = data.Red1Match.SequenceNumber
				},
				Red2Match = new Models.FRCEvent.FRCMatchDataTransfer()
				{
					Id = data.Red2Match.Id,
					SequenceNumber = data.Red2Match.SequenceNumber
				},
				Red3Match = new Models.FRCEvent.FRCMatchDataTransfer()
				{
					Id = data.Red3Match.Id,
					SequenceNumber = data.Red3Match.SequenceNumber
				},
				Match_ID = data.Match_ID,
				MatchNumber = _unit.FRCMatches.GetById(data.Match_ID).SequenceNumber
			};
		}

		public HttpResponseMessage Put(int id, CurrentScoutData data)
		{
			if (!ModelState.IsValid)
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

			if (id != data.Id)
				return Request.CreateResponse(HttpStatusCode.BadRequest);

			FRCEvent existingFRCEvent = this._unit.FRCEvents.GetById(id);
			_unit.FRCEvents.Detach(existingFRCEvent);

			// Keep the orginal CreatedOn value
			data.CreatedOn = existingFRCEvent.CreatedOn;

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

		public HttpResponseMessage Post(RobotEventDataTransfer robotEvent)
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

		public HttpResponseMessage Delete(int id)
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