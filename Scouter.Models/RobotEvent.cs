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
        TotesStacked = 1,
        RightToteMoved = 2,
        CenterToteMoved = 3,
        LeftToteMoved = 4,
        YellowTotesMovedToStep = 5,

        ContainersFromStep = 6,
        RightContainerMoved = 8,
        CenterContainerMoved = 9,
        LeftContainerMoved = 10,

        AutonomousMoved = 11,
        NoAutonomous = 12,
        AutoResultClutter = 13,
        AutoFoul = 14,

        ChutePickUp = 15,
        GroundPickUp = 16,
        DriveOverPlatform = 17,
        HumanPlayerShoots = 18,

        OrientContainer = 19,
        OrientTote = 20,
        ClearContainer = 21,
        ClearTote = 22,
        ClearLitter = 23,
       
        LitterPlacedAtHeight = 24,
        BulldozeLitterToLandfill = 25,
        TeleopFoul = 26
    }

    public class RobotEvent : IAuditInfo
    {
        public int Id { get; set; }
        public virtual Team Team { get; set; }
        public virtual FRCMatch Match { get; set; }
        public RobotMode RobotMode { get; set; }
        public RobotEventType RobotEventType { get; set; }
        public bool GoalWasHot { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
