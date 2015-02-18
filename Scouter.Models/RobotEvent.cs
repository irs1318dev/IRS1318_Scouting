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
        TotesStacked,
        RightToteMoved,
        CenterToteMoved,
        LeftToteMoved,
        YellowTotesMovedToStep,

        RightContainerFromStep,
        CenterRightContainerFromStep,
        CenterLeftContainerFromStep,
        LeftContainerFromStep,

        RightContainerMoved,
        CenterContainerMoved,
        LeftContainerMoved,

        AutonomousMoved,
        NoAutonomous,
        AutoResultClutter,
        AutoFoul,

        RightChutePickUp,
        LeftChutePickUp,
        GroundPickUp,
        DriveOverPlatform,
        HumanPlayerShoots,
        HumanPlayerFails,

        OrientContainer,
        OrientTote,
        ClearContainer,
        ClearTote,
        ClearLitter,
       
        

        LitterPlacedAtHeight,
        BulldozeLitterToLandfill,
        TeleopFoul,



        //Used for seeding, must be last
        MAX
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
