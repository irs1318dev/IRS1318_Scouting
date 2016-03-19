import Net.*;

import java.io.*;
import java.util.List;
import java.util.Scanner;
import java.util.*;
import java.util.logging.Handler;
import java.util.logging.LogRecord;

public class MainData {
    int i = 0;
    int uses = 3;
    int inMatch = 0;
    int currentCount = 0;
    int startMatch;
    int[] objectType;
    File matches;
    String text;
    String defences;
	String[] objectName;
    List<String> columnNames;
    List<Integer> teams;
    List<List<Integer[][]>> dataValue;
    boolean connected = false;
    boolean eachMatch = true;
    TCPClient client;
    
    public void start() {
        System.out.println("start at match:");
        Scanner scanner = new Scanner(System.in);
        text = scanner.nextLine();
        startMatch = Integer.valueOf(text);
        if(startMatch == 0) {
            eachMatch = false;
            uses = 1;
        }
        run(true);
    }

    public void run(boolean first) {
        if(!first) System.out.println("Testing...");
        if(!connected) {
            System.out.println("Enter Server Address:");
            Scanner scanner = new Scanner(System.in);
            text = scanner.nextLine();
            try {
                connect();
            } catch (Exception e) {
            }
        }
    }

    public void connect() {
        client = new TCPClient(11111, text);
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
					for (i = 0; i < networkPackets.length; ++i) {
                        if(networkPackets[i].Name.equals("GameStart")) {
							objectName = new String[networkPackets[i].DataAsInt()];
							objectType = new int[networkPackets[i].DataAsInt()];
                            currentCount = 0;
                            if(!eachMatch) try {
                                matches = new File("Matches.csv");
                                FileWriter fileWriter = new FileWriter(matches);
                                fileWriter.write("Match,Team,Side,\n");
                                fileWriter.close();
                            }catch (IOException e) {}
                            System.out.println("Loading Game");
                        }
						if(networkPackets[i].Name.equals("Game")) {
                            System.out.print(".");
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
                        if(networkPackets[i].Name.equals("GameEnd")) {
                            System.out.println("Done");
                            try {
                                client.SendPacket("GetData", "");
                            } catch(IOException e) {}
                        }
						if(networkPackets[i].Name.equals("DefenceInfo")) {
                            System.out.println("Loading Match " + networkPackets[i].Data.split("&")[0]);
							if (inMatch > 2) defences = networkPackets[i].Data.split("&")[1];
                            else defences = networkPackets[i].Data.split("&")[2];
						}
                        if (networkPackets[i].Name.equals("MatchData")) {
                            System.out.print(" .");
                            int team = Integer.valueOf(networkPackets[i].Data.split("&")[0].split(",")[0]);
                            int match = Integer.valueOf(networkPackets[i].Data.split("&")[0].split(",")[1]);
                            if(!teams.contains(team)) teams.add(team);
                            inMatch++;
                            if(inMatch > 6) inMatch = 1;

                            String[] data = networkPackets[i].Data.split("&")[1].split(",");
                            Integer[][] values = new Integer[data.length][2];
                            values[0][1] = match;
                            for(int j = 0; j < data.length; j++) {
                                String name = objectName[Integer.valueOf(data[j].split(":")[0])];
                                if(name.contains("#")) name = name.split("#")[0] + ":" + defences.split(",")[Integer.valueOf(name.split("#")[1])];
								if(!columnNames.contains(name)) columnNames.add(name);
								values[columnNames.indexOf(name)][0] = Integer.valueOf(data[j].split(":")[1]);
							}

                            dataValue.get(teams.indexOf(team)).add(values);
							
							if(inMatch > 3) text = "Blue";
                            else text = "Red";
                            if(!eachMatch) try {
                                FileWriter fileWriter = new FileWriter(matches);
                                fileWriter.write(match + "," + team + "," + text + "\n");
                                fileWriter.close();
                            }catch (IOException e) {}
                        }
                        if(networkPackets[i].Name.equals("MatchEnd")) {
                            System.out.println("Done");
                            System.out.println("Printing Data");
                            saveData();
                        }
					}
                    run(false);
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

            for (i = 0; i < columnNames.size(); i++) columnNames.set(i,columnNames.get(i) + i);
            Collections.sort(columnNames);
            for (i = 0; i < columnNames.size(); i++) fileWriter.write((columnNames.get(i).subSequence(0,columnNames.get(i).length() - 2)) + ",");
            for(i = 0; i < teams.size(); i++) {
                List<Integer[][]> matchList = dataValue.get(i);
                if(eachMatch) for(int m = startMatch; m < matchList.size(); m++) {
                    fileWriter.write("\n" + teams.get(i) + "," + matchList.get(m)[0][0]);
                    writeLine(matchList,fileWriter,m);
                }
                if(!eachMatch) {
                    fileWriter.write("\n" + teams.get(i) + ",");
                    writeLine(matchList,fileWriter,0);
                }
            }
            fileWriter.close();
        }catch (IOException e) {}
        System.out.println("Done");
        System.out.println("Update Complete: Restarting");
        try {
            client.Disconnect();
        } catch (Exception e) {}
        start();
	}

    public void writeLine(List<Integer[][]> matchList,FileWriter fileWriter, int m) {
        try {
            for(int j = 1; j < columnNames.size(); j++) {
                text = columnNames.get(j);
                int id = text.charAt(text.length() - 1);
                int value = 0;
                if(eachMatch) value = matchList.get(m)[id][0];
                else for(int l = 0; l <= uses; l++) if(matchList.size() > l) value += matchList.get(matchList.size() - l)[id][0];
                fileWriter.write(value + ",");
            }
        } catch (IOException e) {}
    }
}
