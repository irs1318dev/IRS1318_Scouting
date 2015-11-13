using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scouting_Server
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.csv";
      Data.DataFile<Models.Test> test = new Data.DataFile<Models.Test>(path);
      test.WriteRow(new Models.Test() { MyProperty = 1, what = 2, theThird = 3, myAction = 4, TheWortzmanOne = 5});
      test.WriteRow(new Models.Test() { MyProperty = 5, what = 4, theThird = 3, myAction = 2, TheWortzmanOne = 99 });
    }
  }
}
