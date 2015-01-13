using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Reports
{
    public class Anon
    {
        public RobotMode RobotMode {get; set;}
        public RobotEventType RobotEventType {get;set;}
        public bool GoalWasHot { get; set; }
        public int Match_Seq { get; set; }
        public int Team_Number { get; set; }
        public int Team_Id { get; set; }
    }
}