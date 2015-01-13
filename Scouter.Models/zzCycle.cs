using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    [Obsolete("Do not use", true)]
    public class zzCycle
    {
        public List<RobotEvent> RobotEvents { get; set; }
        public List<zzBallEvent> BallEvents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public int Score
        { 
            get
            {
                throw new NotImplementedException("score");
            } 
        }

    }
}
