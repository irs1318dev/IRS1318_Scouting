using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.FRCEvent;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Scouter.Web.Controllers.api
{
    public class ScoutManagerApiController : ApiController
	{
		private ApplicationUnit _unit = new ApplicationUnit();

		public HttpResponseMessage Put(ScoutUpdateInfo info)
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

		public HttpResponseMessage Post(MatchInfoDataTransfer info)
		{

			int frceventID = _unit.CurrentScoutData.GetById(1).Event_ID;
			int id = info.MatchNumber;
			var query = from v in _unit.FRCMatches.GetAll()
						where v.SequenceNumber == id && v.FRCEvent.Id == frceventID
						select v;
			FRCMatch match = null;
			Alliance blue = null;
			Alliance red = null;

			int blue1int = info.Blue1;

			var queryb1 = from v in _unit.Teams.GetAll()
							where v.Number == blue1int
							select v;
			var thing = queryb1.ToList();
			if (queryb1.Count() < 1)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Blue1 + " does not exist"));



			int blue2int = info.Blue2;
			var queryb2 = from v in _unit.Teams.GetAll()
							where v.Number == blue2int
							select v;
			if (queryb2.Count() < 1)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Blue2 + " does not exist"));



			int blue3int = info.Blue3;
			var queryb3 = from v in _unit.Teams.GetAll()
							where v.Number == blue3int
							select v;
			if (queryb3.Count() < 1)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Blue3 + " does not exist"));



			int red1int = info.Red1;
			var queryr1 = from v in _unit.Teams.GetAll()
							where v.Number == red1int
							select v;
			if (queryr1.Count() < 1)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Red1 + " does not exist"));



			int red2int = info.Red2;
			var queryr2 = from v in _unit.Teams.GetAll()
							where v.Number == red2int
							select v;
			if (queryr2.Count() < 1)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Red2 + " does not exist"));



			int red3int = info.Red3;
			var queryr3 = from v in _unit.Teams.GetAll()
							where v.Number == red3int
							select v;
			if (queryr3.Count() < 1)
				return Request.CreateErrorResponse(HttpStatusCode.NotFound, new NullReferenceException("Team " + info.Red3 + " does not exist"));


			if (query.Count() == 0)
			{
				match = new FRCMatch()
				{
					SequenceNumber = id,
					FRCEvent = _unit.FRCEvents.GetById(frceventID)
				};
				_unit.FRCMatches.Add(match);
				_unit.SaveChanges();
				_unit.FRCEvents.GetById(frceventID).Matches.Add(match);

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
				match = query.First();
				blue = match.BlueAlliance;
				red = match.RedAlliance;
			}

			red.Team1 = queryr1.First();
			red.Team2 = queryr2.First();
			red.Team3 = queryr3.First();
			blue.Team1 = queryb1.First();
			blue.Team2 = queryb2.First();
			blue.Team3 = queryb3.First();


			var scoutdata = _unit.CurrentScoutData.GetById(1);

			scoutdata.Blue1Status = ScoutStatus.NoScout;
			scoutdata.Blue2Status = ScoutStatus.NoScout;
			scoutdata.Blue3Status = ScoutStatus.NoScout;
			scoutdata.Red1Status = ScoutStatus.NoScout;
			scoutdata.Red2Status = ScoutStatus.NoScout;
			scoutdata.Red3Status = ScoutStatus.NoScout;
			scoutdata.Match_ID = match.Id;

			_unit.FRCMatches.Update(match);
			_unit.Alliances.Update(red);
			_unit.Alliances.Update(blue);
			_unit.CurrentScoutData.Update(scoutdata);

			_unit.SaveChanges();
			return Request.CreateResponse(HttpStatusCode.NoContent);
		}

		public FRCMatchDataTransfer Get(int id)
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
	}
}