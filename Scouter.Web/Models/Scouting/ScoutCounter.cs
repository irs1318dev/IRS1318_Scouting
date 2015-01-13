using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	public class ScoutCounter
	{
		public int HighCount { get; set; }
		public int HighHotCount { get; set; }
		public int HighMissedCount { get; set; }
		public int LowCount { get; set; }
		public int LowHotCount { get; set; }
		public int LowMissedCount { get; set; }
		public int BlockedCount { get; set; }
		public int FoulCount { get; set; }
		public int TechFoulCount { get; set; }
		public int BlockedShotCount { get; set; }
		public int BlockedRobotCount { get; set; }
		public int BlockedPassCount { get; set; }
		public int TrussCount { get; set; }
		public int CatchCount { get; set; }
		public int PassCount { get; set; }
		public int InboundCount { get; set; }
		public int MissedInboundCount { get; set; }
		public int LostBallCount { get; set; }
	}
}