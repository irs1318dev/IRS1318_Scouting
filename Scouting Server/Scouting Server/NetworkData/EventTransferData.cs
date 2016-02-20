using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scouting_Server.NetworkData
{
  public class EventTransferData
  {
    public int ScoutNumber;
    public int EventType;

    public static EventTransferData Parse(string str)
    {
      EventTransferData data = new EventTransferData();
      string[] split = str.Split(',');
      data.ScoutNumber = int.Parse(split[0]);
      data.EventType = int.Parse(split[1]);
      return data;
    }

    public override string ToString()
    {
      return ScoutNumber + "," + EventType;
    }
  }
}
