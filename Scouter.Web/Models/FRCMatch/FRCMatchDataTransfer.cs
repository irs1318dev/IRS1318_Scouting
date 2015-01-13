using Scouter.Models;
using Scouter.Web.Models.FRCEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.FRCMatch
{
    public class FRCMatchDataTransfer
    {
        public int Id { get; set; }
        public int SequenceNumber { get; set; }

        public  AllianceDataTransfer RedAlliance { get; set; }
        public  AllianceDataTransfer BlueAlliance { get; set; }
        public string FRCEventName { get; set; }

    }
}