using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Scouter.Models
{
    public enum DrivetrainType
    {
        Mechanum = 0,
        Tank = 1,
        Omniwheel = 2,
        Swerve = 3,
        Ackermann = 4
    }

    public class Team : IAuditInfo
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
		public int Number { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public DrivetrainType Drivetrain { get; set; }
        public string DrivetrainName { get { return Drivetrain.ToString(); } }
        public int WheelCount { get; set; }
        /// <summary>
        /// inches of Length
        /// </summary>
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
        // has Matches and MatchResults
    }
}