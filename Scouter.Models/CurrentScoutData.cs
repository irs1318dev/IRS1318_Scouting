using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
	public class CurrentScoutData : IAuditInfo
	{
		public int Id { get; set; }
		public int Event_ID { get; set; }
		public int Match_ID { get; set; }

		public virtual Team Red1 { get; set; }
		public ScoutStatus Red1Status { get; set; }
		public virtual FRCMatch Red1Match { get; set; }

		public virtual Team Red2 { get; set; }
		public ScoutStatus Red2Status { get; set; }
		public virtual FRCMatch Red2Match { get; set; }

		public virtual Team Red3 { get; set; }
		public ScoutStatus Red3Status { get; set; }
		public virtual FRCMatch Red3Match { get; set; }

		public virtual Team Blue1 { get; set; }
		public ScoutStatus Blue1Status { get; set; }
		public virtual FRCMatch Blue1Match { get; set; }

		public virtual Team Blue2 { get; set; }
		public ScoutStatus Blue2Status { get; set; }
		public virtual FRCMatch Blue2Match { get; set; }

		public virtual Team Blue3 { get; set; }
		public ScoutStatus Blue3Status { get; set; }
		public virtual FRCMatch Blue3Match { get; set; }

		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
	public enum ScoutStatus
	{
		NoScout,
		Autonomous,
		Teleoperated,
		Notes,
		WaitingForMatch
	}
}