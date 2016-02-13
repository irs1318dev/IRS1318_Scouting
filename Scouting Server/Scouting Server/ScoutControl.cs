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
      teamNum.Text = "Team#: " + number;
    }

    public void SetTeamNumber(string number)
    {
      teamNum.Text = "Team#: " + number;
    }

    public void SetMatchNumber(int number)
    {
      matchNum.Text = "Match#: " + number;
    }

    public void SetMatchNumber(string number)
    {
      matchNum.Text = "Match#: " + number;
    }

    public void SetStatus(string scoutStatus)
    {
      status.Text = "Status: " + scoutStatus;
    }
  }
}
