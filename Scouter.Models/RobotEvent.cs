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
       
        CoopertitionToteOne,
        CoopertitionToteTwo,
        CoopertitionToteThree,
        CoopertitionToteFour,

        RightContainerFromStep,
        RightCenterContainerFromStep,
        LeftCenterContainerFromStep,
        LeftContainerFromStep,

        RightContainerMoved,
        CenterContainerMoved,
        LeftContainerMoved,

        AutonomousMoved,
        NoAutonomous,
        AutoAttemptClutter,
        Foul,

        RightChutePickUp,
        LeftChutePickUp,
        GroundPickUp,
        DriveOverPlatform,

        OrientContainer,
        OrientTote,
        ClearContainer,
        ClearTote,
        ClearLitter,
 
        BulldozeLitterToLandfill,


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
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
