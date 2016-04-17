package com.irs1318.Scouter_Client.Net;

import java.util.ArrayList;

public class NetworkPacket
{

  static final NetworkPacket GOODBYE = new NetworkPacket("Go000dByeImyasdkhvbkeaidvubclasdkjbuaeuirla", "");

  public String Name;
  public String Data;

  public double DataAsDouble()
  {
    return Double.parseDouble(Data);
  }

  public int DataAsInt()
  {
    return Integer.parseInt(Data);
  }

  public boolean DataAsBool()
  {
    return Boolean.parseBoolean(Data);
  }

  public NetworkPacket(String name, String data)
  {
    Name = name;
    Data = data;
  }

  public static NetworkPacket[] GetPackets(byte[] buffer)
  {
    ArrayList<NetworkPacket> packets = new ArrayList<>();
    ArrayList<String> bufferData = new ArrayList<>();
    String currentData = "";
    for (byte b : buffer)
    {
      char input = (char)b;
      if (input == (char) 0)
      {
        if(!currentData.isEmpty())
            bufferData.add(currentData);
        currentData = "";
      }
      else
        currentData += input;
    }

    //Right here is the error that breaks everything hurry I need it done please!!!!!!! <----- <------ <<<<<<<<<<<<
    for (int i = 0; i < bufferData.size() - 1; i += 2)
    {
      //When it is odd the index gets out of range
      packets.add(new NetworkPacket(bufferData.get(i), bufferData.get(i + 1)));
    }
    NetworkPacket[] arr = new NetworkPacket[packets.size()];
    return packets.toArray(arr);
  }

  @Override
  public String toString()
  {
    return Name + '{' + Data + '}';
  }

  public byte[] ToByteBuffer()
  {
    String str = Name + (char) (0) + Data + (char) (0);
    return str.getBytes();
  }
}
