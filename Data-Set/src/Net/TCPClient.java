package Net;

import java.io.IOException;
import java.io.InputStream;
import java.net.Socket;
import java.util.ArrayList;
import java.util.logging.Level;
import java.util.logging.Logger;

public class TCPClient
{

  private Socket Server;
  private ArrayList<NetworkPacket> Packets;
  public int Port;
  public String Address;
  public ArrayList<NetworkEvent> OnConnected;
  public ArrayList<NetworkEvent> OnDisconnected;
  public ArrayList<NetworkEvent> OnDataAvailable;
  private Thread RunThread;
    private Thread SendThread;
    private NetworkPacket[] SendArr;

  public TCPClient(int port, String address)
  {
    Port = port;
    Address = address;
    Packets = new ArrayList<>();
    RunThread = new Thread(new Runnable()
    {
        @Override
        public void run()
        {
            try
            {
                Server = new Socket(Address, Port);
                for(NetworkEvent e : OnConnected)
                    e.Call(null);
            }
            catch (Exception e)
            {
                e.toString();
                return;
            }
            InputStream input = null;
            try {
                input = Server.getInputStream();
                while (RunThread.isAlive())
                {
                    if(Server == null)
                    {
                        for(NetworkEvent e : OnDisconnected)
                            e.Call(null);
                        return;
                    }
                    if(Server.isClosed() || !Server.isConnected())
                    {
                        for(NetworkEvent e : OnDisconnected)
                            e.Call(null);
                        return;
                    }
                    
                    int available = input.available();
                    
                    if(available != 0)
                    {
                        try {
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
                                e.Call(null);
                        } catch (IOException ex) {

                            Logger.getLogger(TCPClient.class.getName()).log(Level.SEVERE, null, ex);
                            //todo crash spectacularly
                        } catch (Exception ex) {

                            Logger.getLogger(TCPClient.class.getName()).log(Level.SEVERE, null, ex);
                            //todo crash spectacularly
                        }
                    }
                    else
                    {
                        try
                        {
                            Thread.sleep(100);
                        }
                        catch (InterruptedException ex){}
                    }
                }   return;
            } catch (IOException ex) {
                Logger.getLogger(TCPClient.class.getName()).log(Level.SEVERE, null, ex);
            } finally {
                try {
                    input.close();
                } catch (IOException ex) {
                    Logger.getLogger(TCPClient.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        }
    }, "Network-Listen");
      SendThread = new Thread(new Runnable() {
          @Override
          public void run() {
              try
              {
                  NetworkPacket[] temparr = SendArr;
                  for (int i = 0; i < temparr.length; ++i)
                  {
                      byte[] buffer = temparr[i].ToByteBuffer();
                      Server.getOutputStream().write(buffer);
                  }
              }
              catch (IOException ex)
              {

              }
          }
      }, "Network-Send");
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


    RunThread.start();

  }

  public void Disconnect() throws Exception
  {
    if (Server == null)
      return;

    RunThread.interrupt();

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
    SendArr = packets;
    if(SendThread.isAlive())
        SendThread.stop();
    SendThread.run();
  }

  public void SendPacket(NetworkPacket packet) throws IOException
  {
    SendPackets(packet);
  }

  public void SendPacket(String name, String data) throws IOException
  {
    SendPackets(new NetworkPacket(name, data));
  }

  public NetworkPacket[] GetPackets()
  {
    NetworkPacket[] packets = Packets.toArray(new NetworkPacket[Packets.size()]);
    Packets.clear();
    return packets;
  }
}
