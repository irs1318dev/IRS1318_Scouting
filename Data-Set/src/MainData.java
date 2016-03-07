import Net.*;

import java.io.Console;
import java.io.SyncFailedException;
import java.util.Scanner;

public class MainData {
    int i = 0;
    int[] objectType;
    String text;
    String[] objectName;
    boolean connected = false;
    TCPClient client;

    public void run() {
        if(connected) saveData();
        else {
            System.out.print("Enter Server Address:");
            Scanner scanner = new Scanner(System.in);
            text = scanner.nextLine();
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
        run();
    }

    public void saveData() {
        for(i = 0; i < objectName.length; i++) {
            System.out.print(objectName[i]);
        }
    }
}
