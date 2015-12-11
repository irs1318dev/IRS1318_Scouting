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
    Data.DataFile<Models.Test> test;
    public Form1()
    {
      InitializeComponent();

      string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.csv";
      test = new Data.DataFile<Models.Test>(path);
      Models.Test testData = new Models.Test();
      testData.MyProperty = 1000;
      testData.TheWortzmanOne = 2000;
      test.Add(testData);
      test.Add(new Models.Test() { MyProperty = 1, what = 2, theThird = 3, myAction = 4, TheWortzmanOne = 5});
      ulong idVal = test.Add(new Models.Test() { MyProperty = 5, what = 4, theThird = 3, myAction = 2, TheWortzmanOne = 99 });
      test.Remove(idVal);
      test.Add(new Models.Test() { MyProperty = 5, what = 4, theThird = 3, myAction = 2, TheWortzmanOne = 99 });

      if(test.Exists(10))
      {
        var model = test.Get(10);
        model.myAction = 9999999;
        test.Update(model);
      }

      test.Save();
    }
  }
}
