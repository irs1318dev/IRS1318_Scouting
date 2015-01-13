using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    [Obsolete("Do not use", true)]
    public class zzParticipant
    {
        public bool IsHuman { get; set; }
        public Team Team { get; set; }
        //public zzLocation Location { get; set; }
    }
}
