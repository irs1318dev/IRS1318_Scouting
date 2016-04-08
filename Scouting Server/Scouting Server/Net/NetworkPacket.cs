<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scouting_Server.Net
{
  public class NetworkPacket
  {
    internal static readonly NetworkPacket goodbye = new NetworkPacket("Go000dByeImyasdkhvbkeaidvubclasdkjbuaeuirla", "");

    /// <summary>
    /// The name of the packet, could be used for sorting information
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The actual data inside the packet.
    /// </summary>
    public string Data { get; set; }
    public TcpClient Sender { get; set; }
    /// <summary>
    /// Returns the data assuming that it is a double
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.FormatException"/>
    /// <exception cref="System.OverflowException"/>
    public double DataAsDouble
    {
      get
      {
        return Double.Parse(Data);
      }
    }
    /// <summary>
    /// Returns the data assuming that it is an int
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.FormatException"/>
    /// <exception cref="System.OverflowException"/>
    public int DataAsInt
    {
      get
      {
        return Int32.Parse(Data);
      }
    }
    /// <summary>
    /// Returns the data assuming it is a Boolean
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.FormatException"/>
    public bool DataAsBool
    {
      get
      {
        return Boolean.Parse(Data);
      }
    }

    /// <summary>
    /// Converts the data to any format with a Parse(string) method
    /// </summary>
    /// <typeparam name="T">any class that has a Parse(string) method</typeparam>
    /// <exception cref="System.InvalidOperaionException"/>
    public T GetDataAs<T>()
    {
      try
      {
        Type type = typeof(T);
        MethodInfo m = type.GetMethod("Parse", new Type[] { typeof(string) });
        return (T)m.Invoke(m, new object[] { Data });
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Data cannot be cast to " + typeof(T).FullName, ex);
      }
    }

    public NetworkPacket(string name, string data = "")
    {
      Name = name;
      Data = data;
    }

    public static NetworkPacket[] GetPackets(char[] buffer)
    {
      List<NetworkPacket> packets = new List<NetworkPacket>();
      List<string> bufferData = new List<string>();
      string currentData = "";
      foreach (char b in buffer)
      {
        char input = b;
        if (input == (char)0)
        {
          bufferData.Add(currentData);
          currentData = "";
        }
        else
          currentData += input;
      }
      for (int i = 0; i < bufferData.Count; i += 2)
      {
        packets.Add(new NetworkPacket(bufferData[i], bufferData[i + 1]));
      }
      return packets.ToArray();
    }

    public override string ToString()
    {
      return Name + '{' + Data + '}';
    }

    public byte[] ToByteBuffer()
    {
      string str = Name + (char)(0) + Data + (char)(0);
      ASCIIEncoding ascii = new ASCIIEncoding();
      return ascii.GetBytes(str);
    }
  }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scouting_Server.Net
{
  public class NetworkPacket
  {
    internal static readonly NetworkPacket goodbye = new NetworkPacket("Go000dByeImyasdkhvbkeaidvubclasdkjbuaeuirla", "");

    /// <summary>
    /// The name of the packet, could be used for sorting information
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The actual data inside the packet.
    /// </summary>
    public string Data { get; set; }
    public TcpClient Sender { get; set; }
    /// <summary>
    /// Returns the data assuming that it is a double
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.FormatException"/>
    /// <exception cref="System.OverflowException"/>
    public double DataAsDouble
    {
      get
      {
        return Double.Parse(Data);
      }
    }
    /// <summary>
    /// Returns the data assuming that it is an int
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.FormatException"/>
    /// <exception cref="System.OverflowException"/>
    public int DataAsInt
    {
      get
      {
        return Int32.Parse(Data);
      }
    }
    /// <summary>
    /// Returns the data assuming it is a Boolean
    /// </summary>
    /// <exception cref="System.ArgumentNullException"/>
    /// <exception cref="System.FormatException"/>
    public bool DataAsBool
    {
      get
      {
        return Boolean.Parse(Data);
      }
    }

    /// <summary>
    /// Converts the data to any format with a Parse(string) method
    /// </summary>
    /// <typeparam name="T">any class that has a Parse(string) method</typeparam>
    /// <exception cref="System.InvalidOperaionException"/>
    public T GetDataAs<T>()
    {
      try
      {
        Type type = typeof(T);
        MethodInfo m = type.GetMethod("Parse", new Type[] { typeof(string) });
        return (T)m.Invoke(m, new object[] { Data });
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Data cannot be cast to " + typeof(T).FullName, ex);
      }
    }

    public NetworkPacket(string name, string data = "")
    {
      Name = name;
      Data = data;
    }

    public static NetworkPacket[] GetPackets(char[] buffer)
    {
      List<NetworkPacket> packets = new List<NetworkPacket>();
      List<string> bufferData = new List<string>();
      string currentData = "";
      foreach (char b in buffer)
      {
        char input = b;
        if (input == (char)0)
        {
          bufferData.Add(currentData);
          currentData = "";
        }
        else
          currentData += input;
      }
      for (int i = 0; i < bufferData.Count; i += 2)
      {
        packets.Add(new NetworkPacket(bufferData[i], bufferData[i + 1]));
      }
      return packets.ToArray();
    }

    public override string ToString()
    {
      return Name + '{' + Data + '}';
    }

    public byte[] ToByteBuffer()
    {
      //todo replacing values like this is bad!
      string str = Name.Replace('\x0', ' ') + (char)(0) + Data.Replace('\x0', ' ') + (char)(0);
      ASCIIEncoding ascii = new ASCIIEncoding();
      return ascii.GetBytes(str);
    }
  }
}
>>>>>>> branch 'master' of https://github.com/irs1318dev/IRS1318_Scouting.git
