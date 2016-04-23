using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml;

namespace Scouting_Server
{
  public partial class Form1 : Form
  {
    const int PORT = 11111;
    Data.DataFile<Models.Match> Matches;
    Data.DataFile<Models.Team> Teams;
    Data.DataFile<Models.Event> RobotEvents;
    MatchInfo current;
    Net.NetworkServer Serv;
    List<String> ObjectName = new List<String>();
    List<String> PageNames = new List<String>();
    List<int> ObjectType = new List<int>();
    System.Windows.Forms.Timer ErrorTimer;
    Dictionary<TcpClient, int> ScoutersDictionary;
    TcpClient[] Scouters;
    ScoutControl[] ScouterControls;

    public Form1()
    {
      InitializeComponent();
      current = new MatchInfo();

      ErrorTimer = new System.Windows.Forms.Timer();
      ErrorTimer.Interval = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
      ErrorTimer.Tick += ErrorTimer_Tick;

      Matches = new Data.DataFile<Models.Match>("Matches.csv");
      Teams = new Data.DataFile<Models.Team>("Teams.csv");
      RobotEvents = new Data.DataFile<Models.Event>("Events.csv");

            MatchCount.Text = Matches.GetAll().Length.ToString() + " matches set";
            XmlDocument doc = new XmlDocument();
            doc.Load("layout.xml");
            string pageName = "";
            XmlElement v = doc.DocumentElement;
            XmlNodeList mainlist = v.GetElementsByTagName("Category");
            for (int i = 0; i < mainlist.Count; i++)
            {
                XmlNode category = mainlist[i];
                if (pageName != category.Attributes["Page"].Value)
                {
                    ObjectName.Add(category.Attributes["Page"].Value);
                    ObjectType.Add(1);
                    PageNames.Add(category.Attributes["Page"].Value);
                    pageName = category.Attributes["Page"].Value;
                }
                ObjectName.Add(category.Attributes["Name"].Value);
                ObjectType.Add(2);
                XmlElement actions = category["Actions"];
                loadObjects(actions);
            }
            PageNames.Add("Waiting for server...");
            string defences = "";
            for (int i = 0; i < RedDef1.Items.Count; i++) defences += RedDef1.Items[i] + "&";
            for (int i = 0; i < RedDef2.Items.Count; i++) defences += RedDef2.Items[i] + "&";
            for (int i = 0; i < RedDef3.Items.Count; i++) defences += RedDef3.Items[i] + "&";
            for (int i = 0; i < RedDef4.Items.Count; i++) defences += RedDef4.Items[i] + "&";
            for (int i = 0; i < RedDef5.Items.Count; i++) defences += RedDef5.Items[i] + "&";
            ObjectName.Add(defences);
            ObjectType.Add(10);

      ScoutersDictionary = new Dictionary<TcpClient, int>();
      Scouters = new TcpClient[6];
      ScouterControls = new ScoutControl[6];
      ScouterControls[0] = scoutControl1;
      ScouterControls[1] = scoutControl2;
      ScouterControls[2] = scoutControl3;
      ScouterControls[3] = scoutControl4;
      ScouterControls[4] = scoutControl5;
      ScouterControls[5] = scoutControl6;

      Serv = new Net.NetworkServer(PORT);
      Serv.Connected += Serv_Connected1;
      Serv.Disconnected += Serv_Disconnected;
      Serv.DataAvailable += Serv_DataAvailable1;
      Serv.Start(PORT);

    }

    private void ErrorTimer_Tick(object sender, EventArgs e)
    {
      errorMessage.Text = "";
      ErrorTimer.Stop();
    }

    string GetDataPacket(ulong teamID, Models.Match match)
    {
      var quer = from ev in RobotEvents.GetAll()
                 where ev.MatchKey == match.id &&
                 ev.TeamKey == teamID
                 select ev;

      string data = match.MatchNumber + "," + Teams.Get(teamID).TeamNumber + "&";

      var evs = new Dictionary<int, int>();
      foreach (var evnt in quer)
      {
        if (evs.ContainsKey(evnt.EventType))
          ++evs[evnt.EventType];
        else
          evs.Add(evnt.EventType, 1);
      }
      bool first = true;
      foreach (var p in evs)
      {
        if (!first)
          data += ",";
        else
          first = false;

        data += p.Key + ":" + p.Value;
      }
      return data;
    }

    //don't call this. this is mine
    //seriously don't
    private void SDD()
    {
      string data = "";

      data += RedDef1.Text + ",";
      data += RedDef2.Text + ",";
      data += RedDef3.Text + ",";
      data += RedDef4.Text + ",";
      data += RedDef5.Text + "&";

      data += BlueDef1.Text + ",";
      data += BlueDef2.Text + ",";
      data += BlueDef3.Text + ",";
      data += BlueDef4.Text + ",";
      data += BlueDef5.Text;

      Serv.SendPacket("DefenseInfo", data);
    }

    //for the 2016 game
    private void SendDefenseData()
    {
      if (InvokeRequired)
      {
        BeginInvoke(new MethodInvoker(() =>
        {
          SDD();
        }));
      }
      else
        SDD();
    }

    private void SendData(TcpClient to)
    {
      string path = AppDomain.CurrentDomain.BaseDirectory + "/data.csv";
      using (StreamWriter writer = new StreamWriter(path))
      {
        foreach (var match in Matches.GetAll())
        {
          string data = "";

          data = match.MatchNumber + "&";
          data += match.RedDef1 + ",";
          data += match.RedDef2 + ",";
          data += match.RedDef3 + ",";
          data += match.RedDef4 + ",";
          data += match.RedDef5 + "&";

          data += match.BlueDef1 + ",";
          data += match.BlueDef2 + ",";
          data += match.BlueDef3 + ",";
          data += match.BlueDef4 + ",";
          data += match.BlueDef5;

          writer.WriteLine(data);
          writer.WriteLine(GetDataPacket(match.R1TeamKey, match));
          writer.WriteLine(GetDataPacket(match.R2TeamKey, match));
          writer.WriteLine(GetDataPacket(match.R3TeamKey, match));
          writer.WriteLine(GetDataPacket(match.B1TeamKey, match));
          writer.WriteLine(GetDataPacket(match.B2TeamKey, match));
          writer.WriteLine(GetDataPacket(match.B3TeamKey, match));
        }
      }
      Serv.SendPacket("MatchEnd", path, to);
    }

    private void Serv_DataAvailable1(object sender)
    {
      foreach (var packet in Serv.GetPackets())
      {
        if (packet.Name == "Page")
        {
          // ScoutNumber,PageNumber,MatchNumber,TeamNumber
          var PageInfo = packet.GetDataAs<NetworkData.PageChangeTransferData>();
          ScouterControls[PageInfo.ScoutNumber].SetMatchNumber(PageInfo.MatchNumber);
          ScouterControls[PageInfo.ScoutNumber].SetStatus("Page: " + PageNames[PageInfo.PageNumber]);
          ScouterControls[PageInfo.ScoutNumber].SetTeamNumber(PageInfo.TeamNumber);
        }
        else if (packet.Name == "Event")
        {
          // ScoutNumber,EventNumber
          var EventData = packet.GetDataAs<NetworkData.EventTransferData>();
          Models.Event ev = new Models.Event();
          ev.EventType = EventData.EventType;
          ev.MatchKey = current.Match.id;
          ev.TeamKey = current.Teams[EventData.ScoutNumber].id;
          RobotEvents.Add(ev);
          RobotEvents.Save();
        }
        else if (packet.Name == "Undo")
        {
          //ScoutNumber,EventNumber
          //R1, R2, R3, B1, B2, B3 (0-5)
          var EventData = packet.GetDataAs<NetworkData.EventTransferData>();

          var query = from evnt in RobotEvents.GetAll()
                      where evnt.MatchKey == current.Match.id &&
                      evnt.TeamKey == current.Teams[EventData.ScoutNumber].id &&
                      evnt.EventType == EventData.EventType
                      select evnt;

          if (query.Count() > 0)
            RobotEvents.Remove(query.ToArray()[query.Count() - 1]);

        }
        else if (packet.Name == "Hello")
        {
          var scoutNumber = packet.DataAsInt;

          if (Scouters[scoutNumber] != null && Scouters[scoutNumber] != packet.Sender)
          {
            ScoutersDictionary.Remove(Scouters[scoutNumber]);
            Serv.DisconnectClient(Scouters[scoutNumber]);
          }
          if (ScoutersDictionary.ContainsKey(packet.Sender))
          {
            int oldnum = ScoutersDictionary[packet.Sender];

            ScouterControls[oldnum].SetMatchNumber(0);
            ScouterControls[oldnum].SetStatus("None");
            ScouterControls[oldnum].SetTeamNumber(0);

            Scouters[oldnum] = null;
            ScoutersDictionary.Remove(packet.Sender);
          }

          ScouterControls[scoutNumber].SetMatchNumber(0);
          ScouterControls[scoutNumber].SetStatus("Connected");
          ScouterControls[scoutNumber].SetTeamNumber(0);

          ScoutersDictionary.Add(packet.Sender, scoutNumber);
          Scouters[scoutNumber] = packet.Sender;
          ScouterControls[scoutNumber].SetLastIP(packet.Sender.Client.LocalEndPoint.ToString());

          var info = new NetworkData.MatchInfoTransferData();
          if (current.Match != null)
          {
            info.MatchNumber = current.Match.MatchNumber;
            info.TeamName = current.Teams[scoutNumber].TeamName;
            info.TeamNumber = current.Teams[scoutNumber].TeamNumber;
          }

          Serv.SendPacket("Match", info.ToString(), packet.Sender);
        }
        else if (packet.Name == "GetData")
        {
          SendData(packet.Sender);
        }
      }
    }

    private void Serv_Disconnected(object sender)
    {
      TcpClient client = (TcpClient)sender;
      if (ScoutersDictionary.ContainsKey(client))
      {
        int scoutnum = ScoutersDictionary[client];
        ScouterControls[scoutnum].SetStatus("None");
        ScouterControls[scoutnum].SetMatchNumber(0);
        ScouterControls[scoutnum].SetTeamNumber(0);
        ScoutersDictionary.Remove(client);
        Scouters[scoutnum] = null;
      }
    }

    private void Serv_Connected1(object sender)
    {
      TcpClient client = (TcpClient)sender;
      Serv.SendPacket("GameStart", ObjectType.Count.ToString(), client);
      for (int i = 0; i < ObjectType.Count; i++)
      {
        Serv.SendPacket("Game", ObjectName[i] + "," + ObjectType[i].ToString(), client);
      }
      Serv.SendPacket("GameEnd", " ", client);
    }

    public void loadObjects(XmlElement category)
    {
      XmlNodeList actions = category.GetElementsByTagName("Action");
      for (int i = 0; i < actions.Count; i++)
      {
        int j = 0;
        int num = 1;
        if (actions[i].Attributes["Number"] != null) num = int.Parse(actions[i].Attributes["Number"].Value);
        while (j < num)
        {
          string name = actions[i].Attributes["Name"].Value;
          if (actions[i].Attributes["Number"] != null)
            name += "#" + (j + 1);
          ObjectName.Add(name);
          switch (actions[i].Attributes["Type"].Value)
          {
            case "Switch":
              ObjectType.Add(3);
              break;
            case "Count":
              ObjectType.Add(4);
              break;
            case "Choice":
              ObjectType.Add(5);
              break;
            case "Line":
              ObjectType.Add(6);
              break;
            case "Label":
              ObjectType.Add(7);
              break;
            case "Change":
              ObjectType.Add(8);
              break;
            case "Hold":
              ObjectType.Add(9);
              break;
          }
          j++;
        }
      }
    }

    private void Error(string message)
    {
      ErrorTimer.Stop();
      errorMessage.ForeColor = Color.Red;
      errorMessage.Text = DateTime.Now.ToLongTimeString() + ": " + message;
      ErrorTimer.Start();
    }

    private void Warning(string message)
    {
      ErrorTimer.Stop();
      errorMessage.ForeColor = Color.Goldenrod;
      errorMessage.Text = DateTime.Now.ToLongTimeString() + ": " + message;
      ErrorTimer.Start();
    }

    private void Message(string message)
    {
      ErrorTimer.Stop();
      errorMessage.ForeColor = Color.Black;
      errorMessage.Text = DateTime.Now.ToLongTimeString() + ": " + message;
      ErrorTimer.Start();
    }

    private Models.Team GetTeamByNumber(int number)
    {
      var teams = from team in this.Teams.GetAll()
                  where team.TeamNumber == number
                  select team;

      return teams.ToArray()[0];
    }

    private Models.Match GetMatchByNumber(int number)
    {
      var matches = from match in this.Matches.GetAll()
                    where match.MatchNumber == number
                    select match;

      return matches.ToArray()[0];
    }

    private void SaveMatchButton_Click(object sender, EventArgs e)
    {
      bool update = true;
      Models.Match m;

      try
      {
        m = GetMatchByNumber((int)matchNumber.Value);
      }
      catch (IndexOutOfRangeException)
      {
        update = false;
        m = new Models.Match();
      }


      #region getTeams
      Models.Team red1;
      Models.Team red2;
      Models.Team red3;
      Models.Team blue1;
      Models.Team blue2;
      Models.Team blue3;
      //RED 1
      try
      {
        red1 = GetTeamByNumber((int)red1Team.Value);
        m.R1TeamKey = red1.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)red1Team.Value + " not found");
        return;
      }
      //RED 2
      try
      {
        red2 = GetTeamByNumber((int)red2Team.Value);
        m.R2TeamKey = red2.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)red2Team.Value + " not found");
        return;
      }
      //RED 3
      try
      {
        red3 = GetTeamByNumber((int)red3Team.Value);
        m.R3TeamKey = red3.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)red3Team.Value + " not found");
        return;
      }
      //BLUE 1
      try
      {
        blue1 = GetTeamByNumber((int)blue1Team.Value);
        m.B1TeamKey = blue1.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)blue1Team.Value + " not found");
        return;
      }
      //BLUE 2
      try
      {
        blue2 = GetTeamByNumber((int)blue2Team.Value);
        m.B2TeamKey = blue2.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)blue2Team.Value + " not found");
        return;
      }
      //BLUE 3
      try
      {
        blue3 = GetTeamByNumber((int)blue3Team.Value);
        m.B3TeamKey = blue3.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)blue3Team.Value + " not found");
        return;
      }
      #endregion

      m.MatchNumber = (int)matchNumber.Value;

      if (update)
        Matches.Update(m);
      else
        Matches.Add(m);

            Matches.Save();
            MatchCount.Text = Matches.GetAll().Length.ToString() + " matches set";
            Message("Match Saved");
        }

    private void SetMatchButton_Click(object sender, EventArgs e)
    {
      bool update = true;
      Models.Match m;

      try
      {
        m = GetMatchByNumber((int)matchNumber.Value);
      }
      catch (IndexOutOfRangeException)
      {
        update = false;
        m = new Models.Match();
      }


      #region getTeams
      Models.Team red1;
      Models.Team red2;
      Models.Team red3;
      Models.Team blue1;
      Models.Team blue2;
      Models.Team blue3;
      //RED 1
      try
      {
        red1 = GetTeamByNumber((int)red1Team.Value);
        m.R1TeamKey = red1.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)red1Team.Value + " not found");
        return;
      }
      //RED 2
      try
      {
        red2 = GetTeamByNumber((int)red2Team.Value);
        m.R2TeamKey = red2.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)red2Team.Value + " not found");
        return;
      }
      //RED 3
      try
      {
        red3 = GetTeamByNumber((int)red3Team.Value);
        m.R3TeamKey = red3.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)red3Team.Value + " not found");
        return;
      }
      //BLUE 1
      try
      {
        blue1 = GetTeamByNumber((int)blue1Team.Value);
        m.B1TeamKey = blue1.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)blue1Team.Value + " not found");
        return;
      }
      //BLUE 2
      try
      {
        blue2 = GetTeamByNumber((int)blue2Team.Value);
        m.B2TeamKey = blue2.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)blue2Team.Value + " not found");
        return;
      }
      //BLUE 3
      try
      {
        blue3 = GetTeamByNumber((int)blue3Team.Value);
        m.B3TeamKey = blue3.id;
      }
      catch (IndexOutOfRangeException)
      {
        Error("Match could not be set. Team: " + (int)blue3Team.Value + " not found");
        return;
      }
      #endregion

      m.MatchNumber = (int)matchNumber.Value;

      if (update)
        Matches.Update(m);
      else
        Matches.Add(m);

      Matches.Save();

      current.Match = m;

      current.Teams[0] = red1;
      current.Teams[1] = red2;
      current.Teams[2] = red3;
      current.Teams[3] = blue1;
      current.Teams[4] = blue2;
      current.Teams[5] = blue3;

      for (int i = 0; i < 6; ++i)
      {
        if (Scouters[i] != null)
        {
          var inf = new NetworkData.MatchInfoTransferData();
          inf.MatchNumber = current.Match.MatchNumber;
          inf.TeamName = current.Teams[i].TeamName;
          inf.TeamNumber = current.Teams[i].TeamNumber;
          Serv.SendPacket("Match", inf.ToString(), Scouters[i]);
        }
      }

            MatchCount.Text = Matches.GetAll().Length.ToString() + " matches set";
            Message("Match Set");
        }

    private void LoadMatchButton_Click(object sender, EventArgs e)
    {
      int num = (int)matchNumber.Value;
      var matches = from m in this.Matches.GetAll()
                    where m.MatchNumber == num
                    select m;

      if (matches.Count() == 0)
      {
        Error("Info for match " + num + " could not be found");
        return;
      }

      var match = matches.ToArray()[0];

      red1Team.Value = Teams.Get(match.R1TeamKey).TeamNumber;
      red2Team.Value = Teams.Get(match.R2TeamKey).TeamNumber;
      red3Team.Value = Teams.Get(match.R3TeamKey).TeamNumber;
      blue1Team.Value = Teams.Get(match.B1TeamKey).TeamNumber;
      blue2Team.Value = Teams.Get(match.B2TeamKey).TeamNumber;
      blue3Team.Value = Teams.Get(match.B3TeamKey).TeamNumber;
    }

    private void pulse_Tick(object sender, EventArgs e)
    {
      if (Serv != null)
      {
        Serv.SendPacket("PING", "");
      }
    }

    private void matchNumber_ValueChanged(object sender, EventArgs e)
    {
      red1Team.Value = 0;
      red2Team.Value = 0;
      red3Team.Value = 0;
      blue1Team.Value = 0;
      blue2Team.Value = 0;
      blue3Team.Value = 0;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      SendDefenseData();

      if (current.Match != null)
      {
        current.Match.RedDef1 = RedDef1.Text;
        current.Match.RedDef2 = RedDef2.Text;
        current.Match.RedDef3 = RedDef3.Text;
        current.Match.RedDef4 = RedDef4.Text;
        current.Match.RedDef5 = RedDef5.Text;

        current.Match.BlueDef1 = BlueDef1.Text;
        current.Match.BlueDef2 = BlueDef2.Text;
        current.Match.BlueDef3 = BlueDef3.Text;
        current.Match.BlueDef4 = BlueDef4.Text;
        current.Match.BlueDef5 = BlueDef5.Text;

        Matches.Update(current.Match);
        Matches.Save();
      }
    }
  }
}
