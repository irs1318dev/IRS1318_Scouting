using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server.NetworkData
{
  public class PageChangeTransferData
  {
    public int ScoutNumber;
    public int PageNumber;
    public int MatchNumber;
    public int TeamNumber;

    public static PageChangeTransferData Parse(string str)
    {
      PageChangeTransferData data = new PageChangeTransferData();
      string[] split = str.Split(',');
      data.ScoutNumber = int.Parse(split[0]);
      data.PageNumber = int.Parse(split[1]);
      data.MatchNumber = int.Parse(split[2]);
      data.TeamNumber = int.Parse(split[3]);
      return data;
    }

    public override string ToString()
    {
      return ScoutNumber + "," + PageNumber + ',' + MatchNumber + ',' + TeamNumber;
    }
  }
}
