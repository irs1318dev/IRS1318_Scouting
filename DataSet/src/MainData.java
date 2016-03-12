import Net.*;

import java.io.*;
import java.util.List;
import java.util.Scanner;

public class MainData
{
    int i = 0;
    int j = 0;
    int inMatch = 0;
    int[] objectType;
    String text;
    String[] objectName;
    List<Integer> teams;
    List<List<Integer[]>> dataValue;
    boolean connected = false;
    TCPClient client;

    public void run(boolean first)
	{
        if (connected) saveData();
        else
		{
			if (!first) System.out.println("No connection found");
            System.out.println("Enter Server Address:");
            Scanner scanner = new Scanner(System.in);
            text = scanner.nextLine();
            try {
                connect();
            }catch (Exception e) {}
        }
    }

    public void connect()
	{
        client = new TCPClient(11111, text);
        client.OnConnected.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender)
				{
					connected = true;
				}
			});
		client.OnDisconnected.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender)
				{
					connected = false;
				}
			});
        client.OnDataAvailable.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender)
				{
					boolean gamePackets = false;
					NetworkPacket[] networkPackets = client.GetPackets();
					for (i = 0; i < networkPackets.length; ++i)
					{
						if (networkPackets[i].Name.equals("Game"))
						{
							if (!gamePackets)
							{
								objectName = new String[networkPackets.length];
								objectType = new int[networkPackets.length];
								gamePackets = true;
							}
							objectName[i] = networkPackets[i].Data.split(",")[0];
							text = networkPackets[i].Data.split(",")[1];
							objectType[i] = Integer.valueOf(text);
						}
                        if (networkPackets[i].Name.equals("MatchData"))
                        {
                            int team = Integer.valueOf(networkPackets[i].Data.split("&")[0].split(",")[0]);
                            if(!teams.contains(team)) teams.add(team);
                            inMatch++;
                            if(inMatch > 6) inMatch = 1;
                            if(inMatch > 3) text = "Blue";
                            else text = "Red";
                            try {
                                File file = new File("Matches.csv");
                                FileWriter fileWriter = new FileWriter(file);
                                fileWriter.write(networkPackets[i].Data.split("&")[0].split(",")[1] + "," + team + "," + text + "\n");
                                fileWriter.close();
                            }catch (IOException e) {}

                            String[] data = networkPackets[i].Data.split("&")[1].split(",");
                            Integer[] values = new Integer[data.length];
                            for(j = 0; j < data.length; j++) values[Integer.valueOf(data[j].split(":")[0])] = Integer.valueOf(data[j].split(":")[1]);

                            dataValue.get(teams.indexOf(team)).add(values);
                        }
					}
				}
			});
        try {
            client.Connect();
        }catch (Exception e) {}
        run(false);
    }

	public void saveData()
	{
        try {
            File file = new File("Data.csv");
            file.createNewFile();
            FileWriter fileWriter = new FileWriter(file);
            fileWriter.write("team,");

            for (i = 0; i < objectName.length; i++) fileWriter.write(objectName[i] + ",");

            for(i = 0; i < teams.size(); i++) {
                fileWriter.write("\n");
                fileWriter.write(teams.get(i) + ",");
                List<Integer[]> matchList = dataValue.get(i);
                for(j = 0; j < objectName.length; j++) fileWriter.write((matchList.get(matchList.size() - 1)[j] + matchList.get(matchList.size() - 2)[j] + matchList.get(matchList.size() - 3)[j]) + ",");
            }
            fileWriter.close();
        }catch (IOException e) {}
	}
}
