using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.FRCEvent
{
    public class FRCEventDataTransfer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Venue { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}