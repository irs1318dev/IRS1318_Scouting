using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	public class MatchInfoDataTransfer
	{
		public int MatchNumber { get; set; }
		public int Red1 { get; set; }
		public int Red2 { get; set; }
		public int Red3 { get; set; }
		public int Blue1 { get; set; }
		public int Blue2 { get; set; }
		public int Blue3 { get; set; }
	}
}