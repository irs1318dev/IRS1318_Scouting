using System;
using System.Collections.Generic;

namespace Scouter.Models
{
    public class FRCMatch : IAuditInfo
    {
        public int Id { get; set; }
        public int SequenceNumber { get; set; }

        public virtual Alliance RedAlliance { get; set; }
        public virtual Alliance BlueAlliance { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual FRCCompetition FRCEvent { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

        //TODO Teams participating
        //TODO Match results
    }
}
