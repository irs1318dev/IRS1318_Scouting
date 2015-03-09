using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
    public class StackEventDataTransfer
    {
        public int Id { get; set; }
        public int Scouter_Id { get; set; }

        public int StartingHeight { get; set; }
        public int NumTotesAdded { get; set; }
        public bool IsContainerAdded { get; set; }
        public bool IsLitterAdded { get; set; }
    }
}
