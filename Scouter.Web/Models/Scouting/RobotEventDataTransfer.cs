using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
	public class RobotEventDataTransfer
	{
		public int Id { get; set; }
		public int Scouter_Id { get; set; }
		public RobotMode RobotMode { get; set; }
		public RobotEventType RobotEventType { get; set; }
		public bool GoalWasHot { get; set; }
	}
}