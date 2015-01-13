using System;

namespace Scouter.Models
{
    public enum BallEventType
    {
        ShotLow,
        ShotHigh,
        Pass,
        PassCatch,
        PassMissed,
        Truss,
        Field,
        Block
    }

    [Obsolete("Do not use", true)]
    public class zzBallEvent : IAuditInfo
    {
        public AllianceColor BallColor { get; set; }
        public zzParticipant Participant { get; set; }
        public BallEventType BallEventType { get; set; }
        public bool IsHot { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
