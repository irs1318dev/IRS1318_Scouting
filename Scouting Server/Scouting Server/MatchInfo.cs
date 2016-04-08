using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server
{
  public class MatchInfo
  {
    public Models.Team[] Teams;
    public Models.Match Match;

    public MatchInfo()
    {
      Teams = new Models.Team[6];
      Match = null;
    }
  }
}
