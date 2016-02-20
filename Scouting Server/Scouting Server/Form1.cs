﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
      
      XmlDocument doc = new XmlDocument();
      doc.Load("../../path.xml");
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
          pageName = category.Attributes["Page"].Value;
        }
        ObjectName.Add(category.Attributes["Name"].Value);
        ObjectType.Add(2);
        XmlElement actions = category["Actions"];
        loadObjects(actions);
      }
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

    private void Serv_DataAvailable1(object sender)
    {
      foreach (var packet in Serv.GetPackets())
      {
        if (packet.Name == "Page")
        {
          // ScoutNumber,PageNumber,MatchNumber,TeamNumber
          var PageInfo = packet.GetDataAs<NetworkData.PageChangeTransferData>();
          ScouterControls[PageInfo.ScoutNumber].SetMatchNumber(PageInfo.ScoutNumber);
          ScouterControls[PageInfo.ScoutNumber].SetStatus("Page: " + PageInfo.PageNumber);
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
          ThreadPool.QueueUserWorkItem(
            (object state) =>
          {
            RobotEvents.Save();
          });
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

          if(query.Count() > 0)
            RobotEvents.Remove(query.ToArray()[query.Count() - 1]);

        }
        else if (packet.Name == "Hello")
        {
          var scoutNumber = packet.DataAsInt;
          ScoutersDictionary[packet.Sender] = scoutNumber;
          ScoutersDictionary[scoutNumber] = packet.Sender;
          ScouterControls[scoutNumber].SetMatchNumber(0);
          ScouterControls[scoutNumber].SetStatus("Connected");
          ScouterControls[scoutNumber].SetTeamNumber(0);

          var info = new NetworkData.MatchInfoTransferData();
          info.MatchNumber = current.Match.MatchNumber;
          info.TeamName = current.Teams[scoutNumber].TeamName;
          info.TeamNumber = current.Teams[scoutNumber].TeamNumber;

          Serv.SendPacket("Match", info.ToString(), packet.Sender);
        }
      }
    }

    private void Serv_Disconnected(object sender)
    {
      TcpClient client = (TcpClient)sender;
      if(ScoutersDictionary.ContainsKey(client))
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
      for (int i = 0; i < ObjectType.Count; i++)
      {
        Serv.SendPacket("Game", ObjectName[i] + "," + ObjectType[i].ToString(), client);
      }
    }

    public void loadObjects(XmlElement category)
    {
      XmlNodeList actions = category.GetElementsByTagName("Action");
      for (int i = 0; i < actions.Count; i++)
      {
        ObjectName.Add(actions[i].Attributes["Name"].Value);
        switch (actions[i].Attributes["Type"].Value)
        {
          case "Check":
            ObjectType.Add(3);
            break;
          case "Counter":
            ObjectType.Add(4);
            break;
          case "Choice":
            ObjectType.Add(5);
            break;
                    case "Fade":
                        ObjectType.Add(6);
                        break;
                    case "Line":
                        ObjectType.Add(7);
                        break;
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

      for(int i = 0; i < 6; ++i)
      {
        if(Scouters[i] != null)
        {
          var inf = new NetworkData.MatchInfoTransferData();
          inf.MatchNumber = current.Match.MatchNumber;
          inf.TeamName = current.Teams[i].TeamName;
          inf.TeamNumber = current.Teams[i].TeamNumber;
          Serv.SendPacket("Match", inf.ToString(), Scouters[i]);
        }
      }

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
  }
}
