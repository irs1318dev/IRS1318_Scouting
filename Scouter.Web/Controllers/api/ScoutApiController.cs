using Scouter.Data;
using Scouter.Models;
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
	public class ScoutApiController : ApiController
	{
		private ApplicationUnit _unit = new ApplicationUnit();

		public IEnumerable<RobotEvent> Get()
		{
			return _unit.RobotEvents.GetAll();
		}

		public RobotEvent Get(int id)
		{
			RobotEvent robotEvent = _unit.RobotEvents.GetById(id);
			if (robotEvent == null)
				throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

			return robotEvent;
		}

		public HttpResponseMessage Put(int id, RobotEvent robotEvent)
		{
			if (!ModelState.IsValid)
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

			if (id != robotEvent.Id)
				return Request.CreateResponse(HttpStatusCode.BadRequest);

			FRCEvent existingFRCEvent = this._unit.FRCEvents.GetById(id);
			_unit.FRCEvents.Detach(existingFRCEvent);

			// Keep the orginal CreatedOn value
			robotEvent.CreatedOn = existingFRCEvent.CreatedOn;

			this._unit.RobotEvents.Add(robotEvent);

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

		public HttpResponseMessage Post(RobotEvent robotEvent)
		{
			try
			{
				if (ModelState.IsValid)
				{
					this._unit.RobotEvents.Add(robotEvent);
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
			RobotEvent robotEvent = this._unit.RobotEvents.GetById(id);
			if (robotEvent == null)
				return Request.CreateResponse(HttpStatusCode.NotFound);

			return Delete(robotEvent);
		}

		public HttpResponseMessage Delete(RobotEvent robotEvent)
		{
			if (robotEvent == null)
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No FRCEvent specified.");

			this._unit.RobotEvents.Delete(robotEvent);

			try
			{
				this._unit.SaveChanges();
				return Request.CreateResponse(HttpStatusCode.OK, robotEvent);
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