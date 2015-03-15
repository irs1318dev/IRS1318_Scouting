using Scouter.Models;
using Scouter.Web.Models.FRCEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	public class ScoutDataTransfer
	{
		public int Match_ID { get; set; }
		public int MatchNumber { get; set; }

		public virtual Team Red1 { get; set; }
		public ScoutStatus Red1Status { get; set; }
		public virtual FRCMatchDataTransfer Red1Match { get; set; }

		public virtual Team Red2 { get; set; }
		public ScoutStatus Red2Status { get; set; }
		public virtual FRCMatchDataTransfer Red2Match { get; set; }

		public virtual Team Red3 { get; set; }
		public ScoutStatus Red3Status { get; set; }
		public virtual FRCMatchDataTransfer Red3Match { get; set; }

		public virtual Team Blue1 { get; set; }
		public ScoutStatus Blue1Status { get; set; }
		public virtual FRCMatchDataTransfer Blue1Match { get; set; }

		public virtual Team Blue2 { get; set; }
		public ScoutStatus Blue2Status { get; set; }
		public virtual FRCMatchDataTransfer Blue2Match { get; set; }

		public virtual Team Blue3 { get; set; }
		public ScoutStatus Blue3Status { get; set; }
		public virtual FRCMatchDataTransfer Blue3Match { get; set; }

        public ScoutStatus Human1ScoutStatus { get; set; }
        public ScoutStatus Human2ScoutStatus { get; set; }

        public virtual FRCMatchDataTransfer Human1Match { get; set; }
        public virtual FRCMatchDataTransfer Human2Match { get; set; }
	}
}