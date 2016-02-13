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
using System.Xml;

namespace Scouting_Server
{
  public partial class Form1 : Form
  {
    Data.DataFile<Models.Test> test;
    Net.NetworkServer serv;
    int i = 0;

    public Form1()
    {
      InitializeComponent();

      //string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.csv";
      //test = new Data.DataFile<Models.Test>(path);
      //Models.Test testData = new Models.Test();
      //testData.MyProperty = 1000;
      //testData.TheWortzmanOne = 2000;
      //test.Add(testData);
      //test.Add(new Models.Test() { MyProperty = 1, what = 2, theThird = 3, myAction = 4, TheWortzmanOne = 5 });
      //ulong idVal = test.Add(new Models.Test() { MyProperty = 5, what = 4, theThird = 3, myAction = 2, TheWortzmanOne = 99 });
      //test.Remove(idVal);
      //test.Add(new Models.Test() { MyProperty = 5, what = 4, theThird = 3, myAction = 2, TheWortzmanOne = 99 });


      //if (test.Exists(10))
      //{
      //  var model = test.Get(10);
      //  model.myAction = 9999999;
      //  test.Update(model);
      //}

      //test.Save();

      ////network
      //serv = new Net.NetworkServer(444);
      //serv.Connected += Serv_Connected;
      //serv.DataAvailable += Serv_DataAvailable;


      XmlDocument doc = new XmlDocument();
      doc.Load("path");
      XmlElement v = doc.DocumentElement;
      XmlNodeList mainlist = v.GetElementsByTagName("Auto");
      XmlNode mainnode = mainlist[0];
      XmlElement catagory = mainnode["Offence"];
      loadObjects(catagory, "Auto", "Offense");
      catagory = mainnode["Defence"];
      loadObjects(catagory, null, "Defence");
      mainlist = v.GetElementsByTagName("Teleop");
      mainnode = mainlist[0];
      catagory = mainnode["Offense"];
      loadObjects(catagory, "Teleop", "Offense");
      catagory = mainnode["Defence"];
      loadObjects(catagory, null, "Defence");
    }

    public void loadObjects(XmlElement catagory, String pageName, String catagoryName)
    {
      XmlNodeList actions = catagory.GetElementsByTagName("Action");
      String[] objectName = new String[actions.Count];
      int[] objectType = new int[actions.Count];
      if (pageName != null)
      {
        if (i != 0)
        {
          objectType[i] = 7;
          i++;
        }
        objectName[i] = pageName;
        i++;
      }
      objectName[i] = catagoryName;
      objectType[i] = 2;
      for (i++; i < actions.Count;)
      {
        objectName[i] = actions[i].Attributes["Name"].ToString();
        switch (actions[i].Attributes["Type"].ToString())
        {
          case "Check":
            objectType[i] = 3;
            break;
          case "Counter":
            objectType[i] = 4;
            break;
          case "Slider":
            objectType[i] = 5;
            break;
          case "Choice":
            objectType[i] = 6;
            break;
          case "Text":
            objectType[i] = 8;
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
  }
}
