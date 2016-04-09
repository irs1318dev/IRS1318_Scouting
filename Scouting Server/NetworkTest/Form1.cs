using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scouting_Server.Net;
using System.Net.Sockets;

namespace NetworkTest
{
  public partial class Form1 : Form
  {
    NetworkServer Server;

    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Server.SendPacket("j", textBox1.Text);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      Server = new NetworkServer(444);
      Server.Connected += Server_Connected;
      Server.Disconnected += Server_Disconnected;
      Server.DataAvailable += Server_DataAvailable;
      Server.Start(444);
    }

    private void Server_DataAvailable(object sender)
    {
      foreach (NetworkPacket p in Server.GetPackets())
      {
        if(p.Name == "j")
        {
          richTextBox1.Invoke(new MethodInvoker(() => { richTextBox1.AppendText("\n" + p.Sender.Client.RemoteEndPoint.ToString() + ": " + p.Data); }));
        }
      }
    }

    private void Server_Disconnected(object sender)
    {
      TcpClient client = (TcpClient)sender;

      try
      {
        richTextBox1.Invoke(new MethodInvoker(() => { richTextBox1.AppendText("\n" + client.Client.RemoteEndPoint.ToString() + " Disconnected"); }));
      }
      catch { }
    }

    private void Server_Connected(object sender)
    {
      TcpClient client = (TcpClient)sender;

      richTextBox1.Invoke(new MethodInvoker(() => { richTextBox1.AppendText("\n" + client.Client.RemoteEndPoint.ToString() + " Connected"); }));
    }
  }
}
