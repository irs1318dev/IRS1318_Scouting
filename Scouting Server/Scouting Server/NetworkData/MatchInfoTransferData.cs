using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server.NetworkData
{
  public class MatchInfoTransferData
  {
    public int MatchNumber;
    public int TeamNumber;
    public string TeamName;

    public static MatchInfoTransferData Parse(string str)
    {
      MatchInfoTransferData data = new MatchInfoTransferData();
      string[] split = str.Split(',');
      data.MatchNumber = int.Parse(split[0]);
      data.TeamNumber = int.Parse(split[1]);
      data.TeamName = split[2];
      return data;
    }

    public override string ToString()
    {
      return MatchNumber + "," + TeamNumber + ',' + TeamName;
    }
  }
}
