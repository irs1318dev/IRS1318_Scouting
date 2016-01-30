using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scouting_Server.Net
{
  public class NetworkServer
  {
    private TcpListener listen;

    public List<TcpClient> Clients { get; private set; }
    public int Port { get; set; }
    private List<NetworkPacket> Packets { get; set; }
    

    private bool running;

    /// <summary>
    /// Called when a new connection is established. Passes the new client as sender
    /// </summary>
    public event ConnectionEvent Connected;
    /// <summary>
    /// Called when a connection is closed. Passes the client as sender
    /// </summary>
    public event ConnectionEvent Disconnected;
    /// <summary>
    /// Called when Data is available from a client. passes the Network server as sender
    /// </summary>
    public event DataAvailableEvent DataAvailable;
    
    /// <summary>
    /// disconnects a client from the server
    /// </summary>
    /// <param name="client">the client to be disconnected</param>
    public void DisconnectClient(TcpClient client, bool sendGoodbye = true)
    {
      if(sendGoodbye)
        SendPacket(NetworkPacket.goodbye, client);

      if (Disconnected != null)
        Disconnected(client);

      Clients.Remove(client);
    }

    /// <summary>
    /// disconnects a client from the server
    /// </summary>
    /// <param name="clientNumber">the number of the client in the clients list</param>
    public void DisconnectClient(int clientNumber)
    {
      DisconnectClient(Clients[clientNumber]);
    }

    /// <summary>
    /// disconnects all clients
    /// </summary>
    public void DisconnectClients()
    {
      while (Clients.Count > 0)
      {
        DisconnectClient(0);
      }
    }

    public NetworkPacket[] GetPackets()
    {
      NetworkPacket[] packets = Packets.ToArray();
      Packets.Clear();
      return packets;
    }

    /// <summary>
    /// shuts down the server. a new one is required for future use.
    /// </summary>
    public void Shutdown()
    {
      DisconnectClients();
      running = false;
      listen.Stop();
    }

    private void Run(object state)
    {
      listen.Start();
      while(running)
      {
        if(listen.Pending())
        {
          TcpClient client = listen.AcceptTcpClient();
          Clients.Add(client);
          if (Connected != null)
            Connected(client);
        }

        for (int i = 0; i < Clients.Count; ++i)
        {
          if(!Clients[i].Connected)
          {
            Clients.RemoveAt(i);
            --i;
            continue;
          }
          if(Clients[i].Available > 0)
          {
            StreamReader reader = new StreamReader(Clients[i].GetStream());
            char[] buffer = new char[Clients[i].Available];

            reader.Read(buffer, 0, Clients[i].Available);

            NetworkPacket[] packets = NetworkPacket.GetPackets(buffer);

            foreach(var p in packets)
            {
              if(p.Name == NetworkPacket.goodbye.Name)
              {
                if (Disconnected != null)
                  Disconnected(Clients[i]);
                Clients.RemoveAt(i);
                --i;
                break;
              }
              p.Sender = Clients[i];
              Packets.Add(p);
            }

            DataAvailable(this);
          }
        }
        Thread.Sleep(100);
      }
    }

    public void Start(short port)
    {
      running = true;
      listen.Start();
      ThreadPool.QueueUserWorkItem(Run);
    }

    #region send


    /// <summary>
    /// Sends a group of packets across the network to a specific client
    /// </summary>
    /// <param name="packets">packets to send</param>
    public void SendPackets(NetworkPacket[] packets, TcpClient client)
    {
      try
      {
        foreach (var packet in packets)
        {
          byte[] buff = packet.ToByteBuffer();
          client.GetStream().Write(buff, 0, buff.Length);
        }
      }
      catch (IOException)
      {
        DisconnectClient(client, false);
      }
      catch (ObjectDisposedException)
      {
        DisconnectClient(client, false);
      }
    }

    /// <summary>
    /// Sends a group of packets across the network to a specific client
    /// </summary>
    /// <param name="packets">packets to send</param>
    public void SendPackets(List<NetworkPacket> packets, TcpClient client)
    {
      SendPackets(packets.ToArray(), client);
    }

    /// <summary>
    /// Sends a packet across the network to a specific client
    /// </summary>
    /// <param name="packet">packet to send</param>
    public void SendPacket(NetworkPacket packet, TcpClient client)
    {
      SendPackets(new NetworkPacket[] { packet }, client);
    }

    /// <summary>
    /// Sends a packet across the network to a specific client
    /// </summary>
    /// <param name="name">the name of the packet</param>
    /// <param name="data">the data of the packet</param>
    public void SendPacket(string name, string data, TcpClient client)
    {
      SendPacket(new NetworkPacket(name, data), client);
    }

    /// <summary>
    /// Sends a group of packets across the network to a specific client
    /// </summary>
    /// <param name="packets">packets to send</param>
    public void SendPackets(NetworkPacket[] packets, int ID)
    {
      SendPackets(packets, Clients[ID]);
    }

    /// <summary>
    /// Sends a group of packets across the network to a specific client
    /// </summary>
    /// <param name="packets">packets to send</param>
    public void SendPackets(List<NetworkPacket> packets, int ID)
    {
      SendPackets(packets.ToArray(), ID);
    }

    /// <summary>
    /// Sends a packet across the network to a specific client
    /// </summary>
    /// <param name="packet">packet to send</param>
    public void SendPacket(NetworkPacket packet, int ID)
    {
      SendPackets(new NetworkPacket[] { packet }, ID);
    }

    /// <summary>
    /// Sends a packet across the network to a specific client
    /// </summary>
    /// <param name="name">the name of the packet</param>
    /// <param name="data">the data of the packet</param>
    public void SendPacket(string name, string data, int ID)
    {
      SendPacket(new NetworkPacket(name, data), ID);
    }


    /// <summary>
    /// Sends a group of packets across the network to all clients
    /// </summary>
    /// <param name="packets">packets to send</param>
    public void SendPackets(NetworkPacket[] packets)
    {
      int size = Clients.Count;
      for(int i = 0; i < size; ++i)
      {
        SendPackets(packets, Clients[i]);
        if(Clients.Count < size)
        {
          i -= size - Clients.Count;
          size = Clients.Count;
        }
      }
    }

    /// <summary>
    /// Sends a group of packets across the network to all clients
    /// </summary>
    /// <param name="packets">packets to send</param>
    public void SendPackets(List<NetworkPacket> packets)
    {
      SendPackets(packets.ToArray());
    }

    /// <summary>
    /// Sends a packet across the network to all clients
    /// </summary>
    /// <param name="packet">packet to send</param>
    public void SendPacket(NetworkPacket packet)
    {
      SendPackets(new NetworkPacket[] { packet });
    }

    /// <summary>
    /// Sends a packet across the network to all clients
    /// </summary>
    /// <param name="name">the name of the packet</param>
    /// <param name="data">the data of the packet</param>
    public void SendPacket(string name, string data)
    {
      SendPacket(new NetworkPacket(name, data));
    }

    #endregion

    /// <summary>
    /// Creates a Network client that extends the TcpClient class
    /// </summary>
    /// <param name="address">The Address you intend to connect to</param>
    /// <param name="port">The port on which you intend to connect</param>
    public NetworkServer(int port)
    {
      listen = new TcpListener(IPAddress.Any, port);
      Clients = new List<TcpClient>();
      Packets = new List<NetworkPacket>();
    }

    ~NetworkServer()
    {
      if (Clients != null)
        Shutdown();
    }
  }

  public delegate void ConnectionEvent(object sender);
  public delegate void DataAvailableEvent(object sender);
}
