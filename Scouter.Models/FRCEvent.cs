using System;
using System.Collections.Generic;

namespace Scouter.Models
{
    /// <summary>
    /// Represents a single event at a place
    /// </summary>
    public class FRCCompetition : IAuditInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public virtual List<FRCMatch> Matches { get; set; }

        //TODO All teams participating
    }
}
