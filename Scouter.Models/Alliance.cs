using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scouter.Models
{
    public class Alliance : IAuditInfo
    {
        public int Id { get; set; }
        public AllianceColor Color { get; set; }
        public virtual FRCMatch Match { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
        public virtual Team Team3 { get; set; }
        //public List<zzCycle> Cycles { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
