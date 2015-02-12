using System;
using System.ComponentModel.DataAnnotations;

namespace Scouter.Models
{
    public enum RobotMode
    {
        Teleop,
        Autonomous
    }

    public enum RobotEventType
    {
		AutonomousMoved = 0,
		Foul = 1
    }

    public class RobotEvent : IAuditInfo
    {
        public int Id { get; set; }
        public virtual Team Team { get; set; }
        public virtual FRCMatch Match { get; set; }
        public RobotMode RobotMode { get; set; }
        //public zzLocation Location { get; set; }
        public RobotEventType RobotEventType { get; set; }
        public bool GoalWasHot { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
