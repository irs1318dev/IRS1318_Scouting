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
        StartedInGoalBox = 0,
		ScoredLow = 1,
		ScoredHigh = 2,
		MissedLow = 3,
		MissedHigh = 4,
		Pass = 5,
		TrussCatch = 6,
		Truss = 7,
		AutonomousMoved = 8,
		BlockedShot = 9,
		BlockedPass = 10,
		BlockedRobot = 11,
		Foul = 12,
		TechFoul = 13,
		Inbound = 14,
		MissedInbound = 15,
		LostBall = 16
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
