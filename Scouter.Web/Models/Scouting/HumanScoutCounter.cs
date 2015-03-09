using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
    public class HumanScoutCounter
    {
        public int ThrowToOwnLandfill1 { get; set; }
        public int ThrowToOpponentLandfill1 { get; set; }
        public int ThrowPastOpponentLandfill1 { get; set; }
        public int Failure1 { get; set; }

        public int ThrowToOwnLandfill2 { get; set; }
        public int ThrowToOpponentLandfill2 { get; set; }
        public int ThrowPastOpponentLandfill2 { get; set; }
        public int Failure2 { get; set; }

        public int ThrowToOwnLandfill3 { get; set; }
        public int ThrowToOpponentLandfill3 { get; set; }
        public int ThrowPastOpponentLandfill3 { get; set; }
        public int Failure3 { get; set; }
    }
}