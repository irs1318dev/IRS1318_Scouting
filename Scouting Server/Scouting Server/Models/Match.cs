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

    public string RedDef1 { get; set; }
    public string RedDef2 { get; set; }
    public string RedDef3 { get; set; }
    public string RedDef4 { get; set; }
    public string RedDef5 { get; set; }
    public string BlueDef1 { get; set; }
    public string BlueDef2 { get; set; }
    public string BlueDef3 { get; set; }
    public string BlueDef4 { get; set; }
    public string BlueDef5 { get; set; }

    public Match()
    {
      RedDef1 = "";
      RedDef2 = "";
      RedDef3 = "";
      RedDef4 = "";
      RedDef5 = "";
      BlueDef1 = "";
      BlueDef2 = "";
      BlueDef3 = "";
      BlueDef4 = "";
      BlueDef5 = "";
    }
  }
}
