using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
	public class ScoutingNotes : IAuditInfo
	{
		public int Id { get; set; }
		public string Notes { get; set; }
		public virtual Team Team { get; set; }
		public virtual FRCMatch Match { get; set; }
		public RobotMode Mode { get; set; }

		public DateTime CreatedOn { get; set; }
		public DateTime ModifiedOn { get; set; }
	}
}
