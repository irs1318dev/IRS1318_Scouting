using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    public class HumanEventTypeLookup
    {
        public int Id { get; set; }
        public int RobotEventTypeValue { get; set; }
        public string RobotEventTypeName { get; set; }
    }
}
