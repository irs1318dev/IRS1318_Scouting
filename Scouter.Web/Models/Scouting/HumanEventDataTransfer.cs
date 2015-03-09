using Scouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Scouter.Web.Models.Scouting
{
    public class HumanEventDataTransfer
    {
        public int Id { get; set; }
        public int Scouter_Id { get; set; }
        /// <summary>
        /// EG: (Red)1 (Blue)2 (insert color here)3
        /// </summary>
        public int Team_Number { get; set; }
        public HumanEventType HumanEventType { get; set; }
    }
}