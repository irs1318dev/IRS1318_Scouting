using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server.Models
{
  public class Match : DataModel
  {
    public int MatchNumber { get; set; }
    public int B1TeamKey { get; set; }
    public int B2TeamKey { get; set; }
    public int B3TeamKey { get; set; }
    public int R1TeamKey { get; set; }
    public int R2TeamKey { get; set; }
    public int R3TeamKey { get; set; }
  }
}
