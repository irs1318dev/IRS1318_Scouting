using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Scouter.Web.Controllers.api
{
    public class NotesApiController : ApiController
	{
		private ApplicationUnit _unit = new ApplicationUnit();

		public HttpResponseMessage Put(int id, NotesDataTransfer data)
		{
			var notes = _unit.Notes.GetById(id);

			notes.Notes = data.Notes;

			_unit.Notes.Update(notes);
			_unit.SaveChanges();

			return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
		}

		public HttpResponseMessage Post(NotesDataTransfer data)
		{
			var query = from n in _unit.Notes.GetAll()
						where n.Match.Id == data.Match_Id && n.Team.Id == data.Team_Id && n.Mode == data.Mode
						select n;

			if(query.Count() > 0)
			{
				return Put(query.First().Id,data);
			}
			ScoutingNotes notes = new ScoutingNotes()
			{
				Notes = data.Notes,
				Team = _unit.Teams.GetById(data.Team_Id),
				Match = _unit.FRCMatches.GetById(data.Match_Id),
				Mode = data.Mode
			};

			_unit.Notes.Add(notes);
			_unit.SaveChanges();
			return Request.CreateResponse(System.Net.HttpStatusCode.Accepted);
		}
	}
}