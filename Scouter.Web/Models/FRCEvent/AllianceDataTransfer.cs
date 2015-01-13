using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.FRCEvent
{
    public class AllianceDataTransfer
    {
        public int Id { get; set; }
        public AllianceColor Color { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Team Team3 { get; set; }

        //public static implicit operator AllianceDataTransfer(Alliance other)
        //{
        //    return new AllianceDataTransfer() 
        //    {
        //        Id = other.Id,
        //        Color = other.Color,
        //        Team1 = other.Team1,
        //        Team2 = other.Team2,
        //        Team3 = other.Team3 
        //    };
        //}
    }
}