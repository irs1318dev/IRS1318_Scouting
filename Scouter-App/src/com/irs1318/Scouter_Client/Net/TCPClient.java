package com.irs1318.Scouter_Client.Net;

import java.io.*;
import java.net.Socket;
import java.util.ArrayList;
import java.util.concurrent.*;

public class TCPClient
{

  private Socket Server;
  private ArrayList<NetworkPacket> Packets;
  public int Port;
  public String Address;
  public ArrayList<NetworkEvent> OnConnected;
  public ArrayList<NetworkEvent> OnDisconnected;
  public ArrayList<NetworkEvent> OnDataAvailable;
  private Executor thread;
  private FutureTask RunThread;

  public TCPClient(int port, String address)
  {
    Port = port;
    address = Address;
    Packets = new ArrayList<>();
    RunThread = new FutureTask(() -> {
        return this.Run();
    });
    thread = Executors.newFixedThreadPool(1);
    OnConnected = new ArrayList<>();
    OnDisconnected = new ArrayList<>();
    OnDataAvailable = new ArrayList<>();
  }
  
  @Override
  protected void finalize() throws Throwable
  {
    super.finalize();
    
    Disconnect();
  }

  public void Connect() throws Exception
  {
    if (Server != null)
      Disconnect();
    Server = new Socket(Address, Port);

    thread.execute(RunThread);
    
    for(NetworkEvent e : OnConnected)
      e.Call(this);
  }

  public void Disconnect() throws Exception
  {
    if (Server == null)
      return;

    RunThread.cancel(false);

    try
    {
      Server.getOutputStream().write(NetworkPacket.GOODBYE.ToByteBuffer());
      Server.close();
    }
    catch (IOException ex){}

    Server = null;
    
    for(NetworkEvent e : OnDisconnected)
      e.Call(this);
  }

  public void SendPackets(NetworkPacket... packets) throws IOException
  {
    for (int i = 0; i < packets.length; ++i)
    {
      byte[] buffer = packets[i].ToByteBuffer();
      Server.getOutputStream().write(buffer);
    }
  }

  public void SendPacket(NetworkPacket packet) throws IOException
  {
    SendPackets(packet);
  }

  public void SendPacket(String name, String data) throws IOException
  {
    SendPackets(new NetworkPacket(name, data));
  }

  private Object Run() throws Exception
  {
    InputStream input = Server.getInputStream();
    while (!RunThread.isCancelled())
    {
      if(Server == null)
      {
        for(NetworkEvent e : OnDisconnected)
          e.Call(this);
        return null;
      }
      if(Server.isClosed() || !Server.isConnected())
      {
        for(NetworkEvent e : OnDisconnected)
          e.Call(this);
        return null;
      }
      
      int available = input.available();
      
      if(available != 0)
      {
        byte[] buffer = new byte[available];
        input.read(buffer, 0, available);
        NetworkPacket[] packets = NetworkPacket.GetPackets(buffer);
        
        for(NetworkPacket p : packets)
        {
          if(p.Name.equals(NetworkPacket.GOODBYE.Name))
          {
            Disconnect();
            
            break;
          }
          
          Packets.add(p);
        }
        
        for(NetworkEvent e : OnDataAvailable)
          e.Call(this);
      }
      else
      {
        try
        {
          Thread.sleep(100);
        }
        catch (InterruptedException ex){}
      }
    }
    return null;
  }

  public NetworkPacket[] GetPackets()
  {
    NetworkPacket[] packets = Packets.toArray(new NetworkPacket[Packets.size()]);
    Packets.clear();
    return packets;
  }
}
