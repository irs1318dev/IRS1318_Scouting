using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Scouting_Server
{
  public partial class Form1 : Form
  {
    int devices = 0;
    Data.DataFile<Models.Match> matches;
    Data.DataFile<Models.Team> teams;
    Net.NetworkServer serv;
    List<String> objectName = new List<String>();
    List<int> objectType = new List<int>();
    Timer errorTimer;

    public Form1()
    {
      InitializeComponent();

      errorTimer = new Timer();
      errorTimer.Interval = (int)TimeSpan.FromSeconds(5).TotalMilliseconds;
      errorTimer.Tick += ErrorTimer_Tick;

      matches = new Data.DataFile<Models.Match>("Matches.csv");
      teams = new Data.DataFile<Models.Team>("Teams.csv");
      
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
          objectName.Add(category.Attributes["Page"].Value);
          objectType.Add(1);
          pageName = category.Attributes["Page"].Value;
        }
        objectName.Add(category.Attributes["Name"].Value);
        objectType.Add(2);
        XmlElement actions = category["Actions"];
        loadObjects(actions);
      }

      serv = new Net.NetworkServer(11111);
      serv.Connected += Serv_Connected1;
      serv.Disconnected += Serv_Disconnected;
      serv.DataAvailable += Serv_DataAvailable1;
      serv.Start(11111);

    }

    private void ErrorTimer_Tick(object sender, EventArgs e)
    {
      errorMessage.Text = "";
      errorTimer.Stop();
    }

    private void Serv_DataAvailable1(object sender)
    {

    }

    private void Serv_Disconnected(object sender)
    {

    }

    private void Serv_Connected1(object sender)
    {
      TcpClient client = (TcpClient)sender;
      for (int i = 0; i < objectType.Count; i++)
      {
        serv.SendPacket(objectName[i], objectType[i].ToString(), client);
      }
      devices++;
    }

    public void loadObjects(XmlElement category)
    {
      XmlNodeList actions = category.GetElementsByTagName("Action");
      for (int i = 0; i < actions.Count; i++)
      {
        objectName.Add(actions[i].Attributes["Name"].Value);
        switch (actions[i].Attributes["Type"].Value)
        {
          case "Check":
            objectType.Add(3);
            break;
          case "Counter":
            objectType.Add(4);
            break;
          case "Slider":
            objectType.Add(5);
            break;
          case "Choice":
            objectType.Add(6);
            break;
        }
      }
    }

    private void Serv_DataAvailable(object sender)
    {

    }

    private void Serv_Connected(object sender)
    {

    }

    private void Error(string message)
    {
      errorTimer.Stop();
      errorMessage.ForeColor = Color.Red;
      errorMessage.Text = DateTime.Now.ToLongTimeString() + ": " + message;
      errorTimer.Start();
    }

    private void Warning(string message)
    {
      errorTimer.Stop();
      errorMessage.ForeColor = Color.Goldenrod;
      errorMessage.Text = DateTime.Now.ToLongTimeString() + ": " + message;
      errorTimer.Start();
    }

    private void Message(string message)
    {
      errorTimer.Stop();
      errorMessage.ForeColor = Color.Black;
      errorMessage.Text = DateTime.Now.ToLongTimeString() + ": " + message;
      errorTimer.Start();
    }

    private Models.Team GetTeamByNumber(int number)
    {
      var teams = from team in this.teams.GetAll()
                  where team.TeamNumber == number
                  select team;

      return teams.ToArray()[0];
    }

    private Models.Match GetMatchByNumber(int number)
    {
      var matches = from match in this.matches.GetAll()
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
      //RED 1
      try
      {
        var red1 = GetTeamByNumber((int)red1Team.Value);
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
        var red2 = GetTeamByNumber((int)red2Team.Value);
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
        var red3 = GetTeamByNumber((int)red3Team.Value);
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
        var blue1 = GetTeamByNumber((int)blue1Team.Value);
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
        var blue2 = GetTeamByNumber((int)blue2Team.Value);
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
        var blue3 = GetTeamByNumber((int)blue3Team.Value);
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
        matches.Update(m);
      else
        matches.Add(m);

      matches.Save();

      Message("Match Set");
    }

    private void LoadMatchButton_Click(object sender, EventArgs e)
    {
      int num = (int)matchNumber.Value;
      var matches = from m in this.matches.GetAll()
                    where m.MatchNumber == num
                    select m;

      if(matches.Count() == 0)
      {
        Error("Info for match " + num + " could not be found");
        return;
      }

      var match = matches.ToArray()[0];

      red1Team.Value = teams.Get(match.R1TeamKey).TeamNumber;
      red2Team.Value = teams.Get(match.R2TeamKey).TeamNumber;
      red3Team.Value = teams.Get(match.R3TeamKey).TeamNumber;
      blue1Team.Value = teams.Get(match.B1TeamKey).TeamNumber;
      blue2Team.Value = teams.Get(match.B2TeamKey).TeamNumber;
      blue3Team.Value = teams.Get(match.B3TeamKey).TeamNumber;
    }
  }
}
