using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	public class ScoutUpdateInfo
	{
		public int Scouter { get; set; }
		public ScoutStatus ScouterStatus { get; set; }
		public int Match_Id { get; set; }
		public int Team_Id { get; set; }
	}
}