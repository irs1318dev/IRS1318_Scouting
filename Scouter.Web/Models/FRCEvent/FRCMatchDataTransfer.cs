using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.FRCEvent
{
    public class FRCMatchDataTransfer
    {
        public int Id { get; set; }
        public int SequenceNumber { get; set; }

        public virtual AllianceDataTransfer RedAlliance { get; set; }
        public virtual AllianceDataTransfer BlueAlliance { get; set; }
    }
}