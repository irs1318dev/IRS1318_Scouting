using System;

namespace Scouter.Models
{

    [Obsolete("Do not use", true)]
    public class zzRobotIntel : IAuditInfo
    {
        public int Id { get; set; }
        public virtual Team Team { get; set; }
        public DrivetrainType Drivetrain { get; set; }
        public int WheelCount { get; set; }
        public float Length { get; set; }
        /// <summary>
        /// inches of Width
        /// </summary>
        public float Width { get; set; }
        /// <summary>
        /// inches of Height
        /// </summary>
        public float Height { get; set; }
        /// <summary>
        /// pounds of Weight
        /// </summary>
        public float Weight { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
