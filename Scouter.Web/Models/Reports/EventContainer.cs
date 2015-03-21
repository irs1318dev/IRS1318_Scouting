using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Scouter.Models;

namespace Scouter.Web.Models.Scouting
{
    public class EventContainer
    {
        public string EventIs {get;set;}
        public string EventType {get;set;} 
        public RobotMode Mode {get;set;}
        public int NumTotesAdded {get;set;} 
        public bool IsContainerAdded {get;set;}
        public bool IsLitterAdded {get;set;}
        public int StartingHeight {get;set;}
        public Team Team {get;set;}
        public Scouter.Models.FRCMatch Match {get; set;}
        public int Id {get; set;}
        public DateTime CreatedOn { get; set; }
    }
}