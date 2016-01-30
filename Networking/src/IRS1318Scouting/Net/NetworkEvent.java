package IRS1318Scouting.Net;

@FunctionalInterface
public interface NetworkEvent
{
  void Call(TCPClient sender) throws Exception;
}
