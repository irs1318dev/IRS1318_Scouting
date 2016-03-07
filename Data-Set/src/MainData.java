import Net.*;

import java.io.Console;

public class MainData {
    int i = 0;
    int[] objectType;
    String text;
    String[] objectName;
    boolean connected = false;
    Console console;
    TCPClient client;

    public void run(Console c) {
        console = c;
        if(connected) saveData();
        else {
            text = console.readLine("Enter Server Address:");
            connect();
        }
    }

    public void connect() {
        client = new TCPClient(11111, text);
        client.OnConnected.add(new NetworkEvent() {
            @Override
            public void Call(TCPClient sender) {
                connected = true;
            }
        });
        client.OnDataAvailable.add(new NetworkEvent() {
            @Override
            public void Call(TCPClient sender) {
                boolean gamePackets = false;
                NetworkPacket[] networkPackets = client.GetPackets();
                for (i = 0; i < networkPackets.length; ++i) {
                    if (networkPackets[i].Name.equals("Game")) {
                        if(!gamePackets) {
                            objectName = new String[networkPackets.length];
                            objectType = new int[networkPackets.length];
                            gamePackets = true;
                        }
                        objectName[i] = networkPackets[i].Data.split(",")[0];
                        text = networkPackets[i].Data.split(",")[1];
                        objectType[i] = Integer.valueOf(text);
                    }
                }
            }
        });
        try {
            client.Connect();
        } catch (Exception e) {
        }
        run(console);
    }

    public void saveData() {
        for(i = 0; i < objectName.length; i++) {
            console.printf(objectName[i]);
        }
    }
}
