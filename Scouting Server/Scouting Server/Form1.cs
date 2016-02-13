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
        Data.DataFile<Models.Test> test;
        Net.NetworkServer serv;
        List<String> objectName = new List<String>();
        List<int> objectType = new List<int>();
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
            doc.Load("path.xml");
            string pageName = "";
            XmlElement v = doc.DocumentElement;
            XmlNodeList mainlist = v.GetElementsByTagName("Category");
            for (int i = 0; i < mainlist.Count; i++) {
                XmlNode category = mainlist[i];
                if (pageName != category.Attributes["Page"].Value) {
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

            objectsText.Text = objectType.Count.ToString() + " Objects Included";
            devicesText.Text = devices + " Devices Connected";
            
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
            for(int i = 0; i < objectType.Count; i++)
                {
                serv.SendPacket(objectName[i], objectType[i].ToString(), client);
                }
            devices++;
            devicesText.Text = devices + " Devices Connected";
            }

        public void loadObjects(XmlElement category)
        {
            XmlNodeList actions = category.GetElementsByTagName("Action");
            for (int i = 0; i < actions.Count;i++)
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
}
}
