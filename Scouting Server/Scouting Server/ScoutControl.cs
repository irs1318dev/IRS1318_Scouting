using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scouting_Server
{
  public partial class ScoutControl : UserControl
  {
    public ScoutControl()
    {
      InitializeComponent();
    }

    public void SetTeamNumber(int number)
    {
      if(teamNum.InvokeRequired)
      {
        teamNum.Invoke(new MethodInvoker(() => {
          teamNum.Text = "Team#: " + number;
        }));
      }
      else
        teamNum.Text = "Team#: " + number;
    }

    public void SetTeamNumber(string number)
    {
      if (teamNum.InvokeRequired)
      {
        teamNum.Invoke(new MethodInvoker(() => {
          teamNum.Text = "Team#: " + number;
        }));
      }
      else
        teamNum.Text = "Team#: " + number;
    }

    public void SetMatchNumber(int number)
    {
      if (teamNum.InvokeRequired)
      {
        teamNum.Invoke(new MethodInvoker(() => {
          matchNum.Text = "Match#: " + number;
        }));
      }
      else
        matchNum.Text = "Match#: " + number;
    }

    public void SetMatchNumber(string number)
    {
      if (teamNum.InvokeRequired)
      {
        teamNum.Invoke(new MethodInvoker(() => {
          matchNum.Text = "Match#: " + number;
        }));
      }
      else
        matchNum.Text = "Match#: " + number;
    }

    public void SetStatus(string scoutStatus)
    {
      if (teamNum.InvokeRequired)
      {
        teamNum.Invoke(new MethodInvoker(() => {
          status.Text = "Status: " + scoutStatus;
        }));
      }
      else
        status.Text = "Status: " + scoutStatus;
    }

    public void SetLastIP(string ip)
    {
      if (teamNum.InvokeRequired)
      {
        teamNum.Invoke(new MethodInvoker(() => {
          LastIP.Text = "LastIP: " + ip;
        }));
      }
      else
        LastIP.Text = "LastIP: " + ip;
    }
  }
}
