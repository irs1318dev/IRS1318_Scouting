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
    String text;
	String[] objectName;
    boolean connected = false;
    TCPClient client;
    List<String> columnNames = new ArrayList<>();
    List<String> matches = new ArrayList<>();
    List<Integer[]> dataValue = new ArrayList<>();


    public void run() {
        System.out.println("Testing...");
        if(!connected) {
            System.out.println("Enter Server Address:");
            Scanner scanner = new Scanner(System.in);
            text = scanner.nextLine();
            scanner.close();
            try {
                connect(text);
            } catch (Exception e) {}
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
					for (NetworkPacket networkPacket : networkPackets) {
                        //System.out.println(networkPacket);
                        if(networkPacket.Name.equals("GameStart")) {
							objectName = new String[networkPacket.DataAsInt()];
							objectType = new int[networkPacket.DataAsInt()];
                            object = 0;
                            System.out.println("Loading Game");
                        }
						if(networkPacket.Name.equals("Game")) {
                            switch(Integer.valueOf(networkPacket.Data.split(",")[1])) {
                                case 1:
                                    text = "(" + networkPacket.Data.split(",")[0].charAt(0) + ")";
                                    break;
                                case 3:case 4:case 5:case 9:
                                    String data = text + networkPacket.Data.split(",")[0];
                                    objectName[object] = data;
									objectType[object] = Integer.valueOf(networkPacket.Data.split(",")[1]);
									break;
                            }
                            object++;
						}
                        if(networkPacket.Name.equals("GameEnd")) {
                            System.out.println("Done");
                            try {
                                client.SendPacket("GetData", " ");
                            } catch (IOException e) {
                            }
                            columnNames.add("Entered");
                            for(i = 0; i < objectName.length; i++)
                                if (objectName[i] != null) {
                                    String name = objectName[i];
                                    if(name.contains("$")) name = name.split("$")[0];
                                    else if(!columnNames.contains(name)) columnNames.add(name);
                                }
                        }
						if(networkPacket.Name.equals("MatchEnd")) {
                            System.out.println("Locating file");
                            File file = new File(networkPacket.Data);
                            try {
                                readFile(file);
                            } catch(FileNotFoundException e) {System.out.println("Not Found");}
                        }
                    }
                }
            });
        try {
            client.Connect();
        }catch (Exception e) {}
        run();
    }

    public void readFile(File file) throws FileNotFoundException{
            Scanner scanner = new Scanner(file);
            System.out.println("Found");
            position = 0;
            while(scanner.hasNextLine()) {
                String[] line = scanner.nextLine().split("&");
                if(position == 0) {
                    match = Integer.valueOf(line[0].split(",")[0]);
                    System.out.println("Loading Match " + match);
                    position++;
                } else {
                    int team = Integer.valueOf(line[0].split(",")[1]);
                    if (position > 3) text = "Blue " + (position - 3);
                    else text = "Red " + position;
                    int j = -1;
                    if(position > 3) i = 4;
                    position++;
                    if(position > 6) position = 0;
                    
                    matches.add(match + "," + team + "," + text + ",");
                    if(line.length > 1) {
                        String[] data = line[1].split(",");
                        Integer[] values = new Integer[data.length + columnNames.size()];
                        if (data.length > 0) values[0] = 1;
                        for (i = 0; i < data.length; i++) {
                            int id = Integer.valueOf(data[i].split(":")[0]);
                            String name = objectName[id];
                            boolean stay = true;
                            if (name != null && stay) {
                                if (!columnNames.contains(name)) columnNames.add(name);
                                values[columnNames.indexOf(name)] = Integer.valueOf(data[i].split(":")[1]);
                            }
                        }
                        dataValue.add(values);
                    } else dataValue.add(new Integer[0]);
                }
            }
            scanner.close();
            System.out.println("Done");
            System.out.println("Printing Match Data");
            saveMatchData();
    }

	public void saveMatchData() {
        try {
            File file = new File("MatchData.csv");
            FileWriter dataWriter = new FileWriter(file);
            dataWriter.write("Match,Team,Alliance,");
            for (String columnName : columnNames) {
                dataWriter.write(columnName + ",");
            }
            for(i = 0; i < matches.size(); i++) {
                Integer[] matchList = dataValue.get(i);
                dataWriter.write("\n" + matches.get(i));
                for(int j = 0; j < columnNames.size(); j++) {
                    if(j < matchList.length && matchList[j] != null) dataWriter.write(matchList[j] + ",");
                    else dataWriter.write("0,");
                }
            }
            dataWriter.close();
        }catch (IOException e) {}
        System.out.println("Done");
        System.exit(0);
	}
}
