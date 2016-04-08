using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server.Models
{
  public class Event : DataModel
  {
    public ulong MatchKey { get; set; }
    public ulong TeamKey { get; set; }
    public int EventType { get; set; }
  }
}
