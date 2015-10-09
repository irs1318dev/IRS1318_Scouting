using System;
using System.ComponentModel.DataAnnotations;

namespace Scouter.Models
{
    public enum HumanEventType
    {
        ThrowToOwnLandfill, //+1 points
        ThrowToOpponentLandfill, //-1 points
        ThrowPastOpponentLandfill, //+4 points
        ThrowShortOfOwnLandfill,//fully before our own landfill
        ThrowToStep,
        Failure,//out of field, not fully contained anywhere


        //Used for seeding, must be last
        MAX
    }

    public class HumanEvent : IAuditInfo
    {
        [Key]
        public int Id { get; set; }
        public virtual Team Team { get; set; }
        public virtual FRCMatch Match { get; set; }
        public HumanEventType HumanEventType { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime ModifiedOn { get; set; }
    }
}
