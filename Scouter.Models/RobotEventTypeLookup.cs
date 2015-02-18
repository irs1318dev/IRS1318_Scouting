using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    public class RobotEventTypeLookup : IAuditInfo
    {
        public int Id { get; set; }
        public int RobotEventTypeValue { get; set; }
        public string RobotEventTypeName { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
