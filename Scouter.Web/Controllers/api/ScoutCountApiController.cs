using Scouter.Data;
using Scouter.Models;
using Scouter.Web.Models.Scouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Scouter.Web.Controllers.api
{
    public class ScoutCountApiController : ApiController
	{
		private ApplicationUnit _unit = new ApplicationUnit();

        public ScoutCounter Get(int id)
        {
			var scoutData = _unit.CurrentScoutData.GetById(1);
			ScoutStatus scoutStatus = ScoutStatus.NoScout;
			Team team = null;
			FRCMatch match = null;

			switch(id)
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

			

				return new ScoutCounter()
				{
					HighCount = 0
				};
			return null;
        }
	}
}