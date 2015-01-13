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

			if(scoutStatus == ScoutStatus.Autonomous)
			{
				var query = from v in _unit.RobotEvents.GetAll()
							where v.Team.Id == team.Id && v.Match.Id == match.Id
							select v;

				var highQ = from v in query
							where v.RobotEventType == RobotEventType.ScoredHigh && v.RobotMode == RobotMode.Autonomous && !v.GoalWasHot
							select v;
				int highCount = highQ.Count();

				var highHotQ = from v in query
							where v.RobotEventType == RobotEventType.ScoredHigh && v.RobotMode == RobotMode.Autonomous && v.GoalWasHot
							select v;
				int highHotCount = highHotQ.Count();

				var highMissedQ = from v in query
							where v.RobotEventType == RobotEventType.MissedHigh && v.RobotMode == RobotMode.Autonomous
							select v;
				int highMissedCount = highMissedQ.Count();

				var lowQ = from v in query
							where v.RobotEventType == RobotEventType.ScoredLow && v.RobotMode == RobotMode.Autonomous && !v.GoalWasHot
							select v;
				int lowCount = lowQ.Count();

				var lowHotQ = from v in query
							where v.RobotEventType == RobotEventType.ScoredLow && v.RobotMode == RobotMode.Autonomous && v.GoalWasHot
							select v;
				int lowHotCount = lowHotQ.Count();

				var lowMissedQ = from v in query
							where v.RobotEventType == RobotEventType.MissedLow && v.RobotMode == RobotMode.Autonomous
							select v;
				int lowMissedCount = lowMissedQ.Count();

				var blockedQ = from v in query
							where v.RobotEventType == RobotEventType.BlockedShot && v.RobotMode == RobotMode.Autonomous
							select v;
				int blockedCount = blockedQ.Count();

				var foulQ = from v in query
							where v.RobotEventType == RobotEventType.Foul && v.RobotMode == RobotMode.Autonomous
							select v;
				int foulCount = foulQ.Count();

				var techFoulQ = from v in query
							where v.RobotEventType == RobotEventType.TechFoul && v.RobotMode == RobotMode.Autonomous
							select v;
				int techFoulCount = techFoulQ.Count();

				return new ScoutCounter()
				{
					HighCount = highCount,
					HighHotCount = highHotCount,
					HighMissedCount = highMissedCount,
					LowCount = lowCount,
					LowHotCount = lowHotCount,
					LowMissedCount = lowMissedCount,
					BlockedCount = blockedCount,
					FoulCount = foulCount,
					TechFoulCount = techFoulCount
				};
			}
			else if(scoutStatus == ScoutStatus.Teleoperated)
			{
				var query = from v in _unit.RobotEvents.GetAll()
							where v.Team.Id == team.Id && v.Match.Id == match.Id
							select v;

				var highQ = from v in query
							where v.RobotEventType == RobotEventType.ScoredHigh && v.RobotMode == RobotMode.Teleop
							select v;
				int highCount = highQ.Count();

				var highMissedQ = from v in query
									where v.RobotEventType == RobotEventType.MissedHigh && v.RobotMode == RobotMode.Teleop
									select v;
				int highMissedCount = highMissedQ.Count();

				var lowQ = from v in query
							where v.RobotEventType == RobotEventType.ScoredLow && v.RobotMode == RobotMode.Teleop
							select v;
				int lowCount = lowQ.Count();

				var lowMissedQ = from v in query
									where v.RobotEventType == RobotEventType.MissedLow && v.RobotMode == RobotMode.Teleop
									select v;
				int lowMissedCount = lowMissedQ.Count();

				var blockedShotQ = from v in query
								where v.RobotEventType == RobotEventType.BlockedShot && v.RobotMode == RobotMode.Teleop
								select v;
				int blockedShotCount = blockedShotQ.Count();

				var blockedPassQ = from v in query
									where v.RobotEventType == RobotEventType.BlockedPass && v.RobotMode == RobotMode.Teleop
									select v;
				int blockedPassCount = blockedPassQ.Count();

				var blockedRobotQ = from v in query
									where v.RobotEventType == RobotEventType.BlockedRobot && v.RobotMode == RobotMode.Teleop
									select v;
				int blockedRobotCount = blockedRobotQ.Count();

				var passQ = from v in query
									where v.RobotEventType == RobotEventType.Pass && v.RobotMode == RobotMode.Teleop
									select v;
				int passCount = passQ.Count();

				var trussQ = from v in query
							where v.RobotEventType == RobotEventType.Truss && v.RobotMode == RobotMode.Teleop
							select v;
				int trussCount = trussQ.Count();

				var catchQ = from v in query
							where v.RobotEventType == RobotEventType.TrussCatch && v.RobotMode == RobotMode.Teleop
							select v;
				int catchCount = catchQ.Count();

				var foulQ = from v in query
							where v.RobotEventType == RobotEventType.Foul && v.RobotMode == RobotMode.Teleop
							select v;
				int foulCount = foulQ.Count();

				var techFoulQ = from v in query
								where v.RobotEventType == RobotEventType.TechFoul && v.RobotMode == RobotMode.Teleop
								select v;
				int techFoulCount = techFoulQ.Count();


				var inboundQ = from v in query
								where v.RobotEventType == RobotEventType.Inbound && v.RobotMode == RobotMode.Teleop//even though Inbounding is only for teleop
								select v;
				int inboundCount = inboundQ.Count();

				var minboundQ = from v in query
								where v.RobotEventType == RobotEventType.MissedInbound && v.RobotMode == RobotMode.Teleop//even though Inbounding is only for teleop
								select v;
				int minboundCount = minboundQ.Count();

				var lostBallQ = from v in query
								where v.RobotEventType == RobotEventType.LostBall && v.RobotMode == RobotMode.Teleop
								select v;
				int lostBallCount = lostBallQ.Count();

				return new ScoutCounter()
				{
					HighCount = highCount,
					HighMissedCount = highMissedCount,
					LowCount = lowCount,
					LowMissedCount = lowMissedCount,
					BlockedPassCount = blockedPassCount,
					BlockedRobotCount = blockedRobotCount,
					BlockedShotCount = blockedShotCount,
					PassCount = passCount,
					TrussCount = trussCount,
					CatchCount = catchCount,
					FoulCount = foulCount,
					TechFoulCount = techFoulCount,
					InboundCount = inboundCount,
					MissedInboundCount = minboundCount,
					LostBallCount = lostBallCount
				};
			}
			return null;
        }
	}
}