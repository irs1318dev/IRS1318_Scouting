package com.irs1318.Scouter_Client.Net;

public interface NetworkEvent
{
  void Call(TCPClient sender) throws Exception;
}
