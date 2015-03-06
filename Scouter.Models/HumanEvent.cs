using System;
using System.ComponentModel.DataAnnotations;

namespace Scouter.Models
{
    public enum HumanEventType
    {
        ThrowToOwnLandfill,
        ThrowPastOpponentLandfill,
        Failure,//awwwww


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
