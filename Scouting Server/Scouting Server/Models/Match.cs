using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server.Models
{
  public class Match : DataModel
  {
    public int MatchNumber { get; set; }
    public ulong B1TeamKey { get; set; }
    public ulong B2TeamKey { get; set; }
    public ulong B3TeamKey { get; set; }
    public ulong R1TeamKey { get; set; }
    public ulong R2TeamKey { get; set; }
    public ulong R3TeamKey { get; set; }
  }
}
