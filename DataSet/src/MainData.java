import Net.*;

import java.io.*;
import java.util.List;
import java.util.Scanner;
import java.util.*;

public class MainData {
    int i = 0;
    int match;
    int position = 0;
    int[] objectType;
    FileWriter fileWriter;
    String text;
    String defences;
	String[] objectName;
    List<String> columnNames = new ArrayList<>();
    List<String> matches = new ArrayList<>();
    List<Integer[]> dataValue = new ArrayList<>();
    boolean connected = false;
    TCPClient client;

    public void run() {
        System.out.println("Testing...");
        if(!connected) {
            System.out.println("Enter Server Address:");
            Scanner scanner = new Scanner(System.in);
            text = scanner.nextLine();
            try {
                connect(text);
            } catch (Exception e) {
            }
        }
    }

    public void connect(String Address) {
        client = new TCPClient(11111, Address);
        client.OnConnected.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender) {
					connected = true;
                    System.out.println("Connected");
				}
			});
		client.OnDisconnected.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender) {
					connected = false;
                    System.out.println("Disconnected");
				}
			});
        client.OnDataAvailable.add(new NetworkEvent() {
				@Override
				public void Call(TCPClient sender) {
					NetworkPacket[] networkPackets = client.GetPackets();
                    int object = 0;
					for (i = 0; i < networkPackets.length; ++i) {
                        System.out.println(networkPackets[i]);
                        if(networkPackets[i].Name.equals("GameStart")) {
							objectName = new String[networkPackets[i].DataAsInt()];
							objectType = new int[networkPackets[i].DataAsInt()];
                            object = 0;
                            System.out.println("Loading Game");
                        }
						if(networkPackets[i].Name.equals("Game")) {
                            switch(Integer.valueOf(networkPackets[i].Data.split(",")[1])) {
                                case 1:
                                    text = "(" + networkPackets[i].Data.split(",")[0].charAt(0) + ")";
                                    break;
                                case 3:case 4:case 5:
                                    String data = text + networkPackets[i].Data.split(",")[0];
                                    objectName[object] = data;
									objectType[object] = Integer.valueOf(networkPackets[i].Data.split(",")[1]);
									break;
                                case 10:
                                    defences = networkPackets[i].Data.split(",")[0];
                                    break;
                            }
                            object++;
						}
                        if(networkPackets[i].Name.equals("GameEnd")) {
                            System.out.println("Done");
                            try {
                                client.SendPacket("GetData", " ");
                            } catch (IOException e) {
                            }
                            columnNames.add("Entered");
                            for (int j = 0; j < objectName.length; j++)
                                if (objectName[j] != null) {
                                    String name = objectName[j];
                                    if(name.contains("#"))
                                        for (int l = 0; l < defences.split("&").length; l++) {
                                            name = objectName[j].split("#")[0] + ":" + defences.split("&")[l];
                                            if(!columnNames.contains(name)) columnNames.add(name);
                                        }
                                    else if(!columnNames.contains(name)) columnNames.add(name);
                                }
                        }
						if(networkPackets[i].Name.equals("DefenseInfo")) {
                            match = Integer.valueOf(networkPackets[i].Data.split("&")[0]);
                            System.out.println("Loading Match " + match);
                            if (position > 2) defences = networkPackets[i].Data.split("&")[1];
                            else defences = networkPackets[i].Data.split("&")[2];
						}
                        if (networkPackets[i].Name.equals("MatchData")) {
                            int team = Integer.valueOf(networkPackets[i].Data.split("&")[0].split(",")[1]);
                            position++;
                            if (position > 6) position = 1;
                            if(position > 3) text = "Blue " + (position - 3);
                            else text = "Red " + position;
                            matches.add(match + "," + team + "," + text + ",");
                            String[] data = networkPackets[i].Data.split("&")[1].split(",");
                            Integer[] values = new Integer[data.length + columnNames.size()];
                            if(data.length > 0) values[0] = 1;
                            for (int j = 0; j < data.length; j++) {
                                int id = Integer.valueOf(data[j].split(":")[0]);
                                String name = objectName[id];
                                if(name != null) {
                                    if (name.contains("#")) name = name.split("#")[0] + ":" + defences.split(",")[Integer.valueOf(name.split("#")[1]) - 1];
                                    if (!columnNames.contains(name)) columnNames.add(name);
                                    values[columnNames.indexOf(name)] = Integer.valueOf(data[j].split(":")[1]);
                                }
                            }
                            dataValue.add(values);
                        }
                        if(networkPackets[i].Name.equals("MatchEnd")) {
                            System.out.println("Done");
                            System.out.println("Printing Match Data");
                            saveMatchData();
                        }
                    }
                }
            });
            try {
               client.Connect();
            }catch (Exception e) {}
            run();
        }

	public void saveMatchData() {
        try {
            File file = new File("MatchData.csv");
            fileWriter = new FileWriter(file);
            fileWriter.write("Match,Team,Alliance,");
            for (i = 0; i < columnNames.size(); i++)
                fileWriter.write(columnNames.get(i) + ",");
            for(i = 0; i < matches.size(); i++) {
                Integer[] matchList = dataValue.get(i);
                fileWriter.write("\n" + matches.get(i));
                for(int j = 0; j < columnNames.size(); j++) if(j < matchList.length && matchList[j] != null)
                    fileWriter.write(matchList[j] + ",");
                else fileWriter.write("0,");
            }
            fileWriter.close();
        }catch (IOException e) {}
        System.out.println("Done");
        try {
            client.Disconnect();
        } catch (Exception e) {}
        System.out.println("Printing Team Data");
        saveTeamData();
	}
    
    public void saveTeamData() {
        try {
            List<Integer> teamNumbers = new ArrayList<>();
            List<List<Integer>> teamValues = new ArrayList<>();
            File file = new File("TeamData.csv");
            fileWriter = new FileWriter(file);
            fileWriter.write("Team,");
            for (i = 0; i < columnNames.size(); i++)
                fileWriter.write(columnNames.get(i) + ",");
            for(i = 0; i < matches.size(); i++) {
                Integer[] matchList = dataValue.get(i);
                int team = Integer.valueOf(matches.get(i).split(",")[1]);
                if(!teamNumbers.contains(team)) teamNumbers.add(team);
                int id = teamNumbers.indexOf(team);
                for(int j = 0; j < matchList.length; j++) {
                    if(j > teamValues.get(id).size()) teamValues.get(id).add(matchList[j]);
                    else teamValues.get(id).set(j, teamValues.get(id).get(j) + matchList[j]);
                }
            }
            for(i = 0; i < teamValues.size(); i++) {
                fileWriter.write("\n" + teamNumbers.get(i) + ',');
                for(int j = 0; j < columnNames.size(); j++) if(j < teamValues.size())
                    fileWriter.write(teamValues.get(i).get(j) + ",");
                else fileWriter.write("0,");
            }
            fileWriter.close();
        }catch (IOException e) {}
        System.out.println("Done");
        try {
            client.Disconnect();
        } catch (Exception e) {}
        System.out.println("Update Complete");
        System.exit(0);
    }
}
