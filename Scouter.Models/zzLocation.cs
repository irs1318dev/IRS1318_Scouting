using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scouter.Models
{
    [Obsolete("Do not use", true)]
    public class zzLocation
    {
        public int Id { get; set; }
        public bool HumanPlayer { get; set; }
        public int GridX { get; set; }
        public int GridY { get; set; }
    }
}
