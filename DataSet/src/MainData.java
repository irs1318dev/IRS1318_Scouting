import Net.*;

import java.io.*;
import java.util.List;
import java.util.Scanner;
import java.util.*;

public class MainData {
    int i = 0;
    int j = 0;
    int inMatch = 0;
    int currentCount = 0;
    int[] objectType;
    File matches;
    String text;
	String[] objectName;
    List<String> columnNames;
    List<Integer> teams;
    List<List<Integer[]>> dataValue;
    boolean connected = false;
    TCPClient client;

    public void run(boolean first) {
        if (connected) saveData();
        else {
			if (!first) System.out.println("No connection found");
            System.out.println("Enter Server Address:");
            Scanner scanner = new Scanner(System.in);
            text = scanner.nextLine();
            try {
                connect();
            }catch (Exception e) {}
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
		client.OnDisconnected.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender) {
					connected = false;
				}
			});
        client.OnDataAvailable.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender) {
					NetworkPacket[] networkPackets = client.GetPackets();
					for (i = 0; i < networkPackets.length; ++i) {
                        if (networkPackets[i].Name.equals("GameStart")) {
							objectName = new String[networkPackets[i].DataAsInt()];
							objectType = new int[networkPackets[i].DataAsInt()];
                            currentCount = 0;
                            try {
                                matches = new File("Matches.csv");
                                FileWriter fileWriter = new FileWriter(matches);
                                fileWriter.write("Match,Team,Side,\n");
                                fileWriter.close();
                            }catch (IOException e) {}
                        }
						if (networkPackets[i].Name.equals("Game")) {
                            switch(Integer.valueOf(networkPackets[i].Data.split(",")[1])) {
                                case 1:
                                    text = "(" + networkPackets[i].Data.split(",")[0].charAt(0) + ")";
                                    break;
                                case 3:case 4:case 5:
                                    text += networkPackets[i].Data.split(",")[0];
                                    objectName[currentCount] = text;
									objectType[currentCount] = Integer.valueOf(networkPackets[i].Data.split(",")[1]);
									currentCount++;
									break;
                            }
						}
						if(networkPackets[i].Name.equals("DefenceInfo")) {
							if (inMatch > 2) text = networkPackets[i].Data.split("&")[1];
                            else text = networkPackets[i].Data.split("&")[2];
						}
                        if (networkPackets[i].Name.equals("MatchData")) {
                            int team = Integer.valueOf(networkPackets[i].Data.split("&")[0].split(",")[0]);
                            if(!teams.contains(team)) teams.add(team);
                            inMatch++;
                            if(inMatch > 6) inMatch = 1;

                            String[] data = networkPackets[i].Data.split("&")[1].split(",");
                            Integer[] values = new Integer[data.length];
                            for(j = 0; j < data.length; j++) {
								int id = Integer.valueOf(data[j].split(":")[0]);
                                String name = objectName[id];
								if(!columnNames.contains(name)) columnNames.add(name);
								
								values[id] = Integer.valueOf(data[j].split(":")[1]);
							}

                            dataValue.get(teams.indexOf(team)).add(values);
							
							if(inMatch > 3) text = "Blue";
                            else text = "Red";
                            try {
                                FileWriter fileWriter = new FileWriter(matches);
                                fileWriter.write(networkPackets[i].Data.split("&")[0].split(",")[1] + "," + team + "," + text + "\n");
                                fileWriter.close();
                            }catch (IOException e) {}
                        }
					}
				}
			});
        try {
            client.Connect();
        }catch (Exception e) {}
        run(false);
    }

	public void saveData() {
        for (i = 0; i < objectName.length; i++) {
            System.out.println(objectName[i]);
        }
        try {
            File file = new File("Data.csv");
            FileWriter fileWriter = new FileWriter(file);
            fileWriter.write("team,");

            for (i = 0; i < objectName.length; i++) fileWriter.write(objectName[i] + ",");

            for(i = 0; i < teams.size(); i++) {
                fileWriter.write("\n" + teams.get(i) + ",");
                List<Integer[]> matchList = dataValue.get(i);
                for(j = 0; j < objectName.length; j++) fileWriter.write((matchList.get(matchList.size() - 1)[j] + matchList.get(matchList.size() - 2)[j] + matchList.get(matchList.size() - 3)[j]) + ",");
            }
            fileWriter.close();
        }catch (IOException e) {}
	}
}
