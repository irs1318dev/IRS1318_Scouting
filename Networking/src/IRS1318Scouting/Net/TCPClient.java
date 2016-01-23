package IRS1318Scouting.Net;

import java.net.Socket;
import java.util.ArrayList;

public class TCPClient
{
  private Socket Server;
  private ArrayList<NetworkPacket> Packets;
  public int Port;
  
  public TCPClient(int port)
  {
    Port = port;
    Packets = new ArrayList<>();
  }
  
  public NetworkPacket[] GetPackets()
  {
    return Packets.toArray(new NetworkPacket[Packets.size()]);
  }
}
