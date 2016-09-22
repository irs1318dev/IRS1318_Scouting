package com.irs1318.Scouter_Client;

import android.app.Activity;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.*;

import com.irs1318.Scouter_Client.Net.*;

import java.util.ArrayList;
import java.util.List;

public class MainInput extends Activity {
    //Basic variables
    private int objectNum;
    private int currentCount = 0;
    private int i;
    private int page = 0;
    private int scouter = -1;
    private int match = -1;
    private int team = 0;
    private int[] objectType;
    private int[] objectValue;
    private String lastMatch = "0,0,";
    private String scoutName;
    private String teamName = "";
    private String[] objectName;
    private boolean connected = false;
    private boolean loading = true;
    private boolean newMatch = true;
    private TCPClient client;
    private ScoutForm scoutForm;
    LinearLayout mainLayout;


    //Setting up the Activity
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        if(getActionBar() != null) getActionBar().hide();

        scoutForm = new ScoutForm();
        scoutForm.context = this;
        scoutForm.mainInput = this;
        scoutForm.dataLog = new ArrayList<>();

    }

    //Connecting to PC
    public void connect(View v) {
        if(!connected) {
            //Set current scouter
            switch(v.getId()) {
                case R.id.S0:
                    scouter = 0;
                    scoutName = "(Red 1)";
                    break;
                case R.id.S1:
                    scouter = 1;
                    scoutName = "(Red 2)";
                    break;
                case R.id.S2:
                    scouter = 2;
                    scoutName = "(Red 3)";
                    break;
                case R.id.S3:
                    scouter = 3;
                    scoutName = "(Blue 1)";
                    break;
                case R.id.S4:
                    scouter = 4;
                    scoutName = "(Blue 2)";
                    break;
                case R.id.S5:
                    scouter = 5;
                    scoutName = "(Blue 3)";
                    break;
            }

            //Start connection
            EditText editText = (EditText) findViewById(R.id.editText);
            String text = String.valueOf(editText.getText());
            client = new TCPClient(11111, text);

            client.OnConnected.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    //Send startup messages
                    try {
                        client.SendPacket("Hello", String.valueOf(scouter));
                        client.SendPacket("Page", scouter + "," + page + "," + match + "," + team);
                    } catch(Exception ie) {//Check
                    }
                    connected = true;

                    //Update interface
                    Handler mainHandle = new Handler(getMainLooper());
                    mainHandle.post(new Runnable() {
                        @Override
                        public void run() {
                            //Show connected status
                            RadioButton radioButton = (RadioButton) findViewById(R.id.Connect);
                            radioButton.setChecked(true);
                        }
                    });
                }
            });

            //When receiving data
            client.OnDataAvailable.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    NetworkPacket[] networkPackets = client.GetPackets();
                    for(NetworkPacket networkPacket : networkPackets) {
                        if(networkPacket.Name.equals("GameStart") && loading) {
                            //Preparing to update interface
                            currentCount = 0;
                            objectNum = networkPacket.DataAsInt();
                            objectName = new String[objectNum];
                            objectType = new int[objectNum];
                            if(match == -1) objectValue = new int[objectNum];
                            i = 0;

                            Handler mainHandle = new Handler(getMainLooper());
                            mainHandle.post(new Runnable() {
                                @Override
                                public void run() {
                                    findViewById(R.id.startLayout).setVisibility(View.GONE);
                                    findViewById(R.id.TopLine).setVisibility(View.VISIBLE);
                                    findViewById(R.id.Testing).setVisibility(View.GONE);
                                    if(loading) findViewById(R.id.Loading).setVisibility(View.VISIBLE);
                                }
                            });
                        } else if(networkPacket.Name.equals("Game") && loading) {
                            //Reading first Packets of data
                            objectName[currentCount] = networkPacket.Data.split(",")[0];
                            objectType[currentCount] = Integer.valueOf(networkPacket.Data.split(",")[1]);
                            if(objectType[currentCount] == 1) i++;
                            currentCount++;
                        } else if(networkPacket.Name.equals("GameEnd") && loading) {
                            //finalize interface
                            scoutForm.pageId = new int[i + 1];
                            if(match == -1) loading = true;
                        } else if(networkPacket.Name.equals("Match")) {
                            if(!networkPacket.Data.contains(lastMatch)) {
                                //load up match details
                                if(match != -1) page = 0;
                                match = Integer.valueOf(networkPacket.Data.split(",")[0]);
                                team = Integer.valueOf(networkPacket.Data.split(",")[1]);
                                teamName = networkPacket.Data.split(",")[2];
                                lastMatch = networkPacket.Data;
                                loading = false;
                                newMatch = true;
                                scoutForm.dataLog.clear();

                                //Clearing screen
                                Handler mainHandle = new Handler(getMainLooper());
                                mainHandle.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        TextView textView = (TextView) findViewById(R.id.teamName);
                                        String t = team + " " + teamName + " " + scoutName;
                                        textView.setText(t);

                                        //Showing required parts
                                        Button button = (Button) findViewById(R.id.NextPage);
                                        t = "Next Page -->";
                                        button.setText(t);
                                        button.setVisibility(View.VISIBLE);
                                        findViewById(R.id.Loading).setVisibility(View.GONE);
                                        findViewById(R.id.LastPage).setVisibility(View.GONE);
                                        findViewById(R.id.Reverse).setVisibility(View.VISIBLE);
                                        findViewById(R.id.Refresh).setVisibility(View.VISIBLE);
                                        loadObjects(null);
                                    }
                                });

                                try {
                                    client.SendPacket("Page", scouter + ",0" + "," + match + "," + team);
                                } catch(Exception ie) {//Check
                                }
                            }
                        } else if(networkPacket.Name.equals("MatchData")) {
                            if(newMatch) {
                                //reload old data
                                objectValue = new int[objectNum];
                                newMatch = false;
                                for(String s : networkPacket.Data.split("&")[1].split(",")) objectValue[Integer.valueOf(s.split(":")[0])] = Integer.valueOf(s.split(":")[1]);

                                //Refresh screen
                                Handler mainHandle = new Handler(getMainLooper());
                                mainHandle.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        loadObjects(null);
                                    }
                                });
                            }
                        }
                        send();
                    }
                }
            });
            client.OnDisconnected.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    connected = false;
                    //Set connect button to false
                    Handler mainHandle = new Handler(getMainLooper());
                    mainHandle.post(new Runnable() {
                        @Override
                        public void run() {
                            RadioButton radioButton = (RadioButton) findViewById(R.id.Connect);
                            radioButton.setChecked(false);
                        }
                    });
                }
            });

            //Actually connect
            try {
                client.Connect();
            } catch(Exception e) {//Check
            }
        }
    }

    private void send() {
        //Send button presses to server
        List<ButtonPress> dataLog = scoutForm.dataLog;
        if(dataLog.size() > 0) {
            try {
                for(ButtonPress p : dataLog) client.SendPacket(p.Name, scouter + "," + p.Data);
            } catch(Exception ie) {//Check
            }
            scoutForm.dataLog.clear();
        }
    }

    public void loadObjects(View v) {
        //Start showing form
        scoutForm.objectName = objectName;
        scoutForm.objectType = objectType;
        scoutForm.objectValue = objectValue;
        scoutForm.loadObjects(page);

        //Changing Title
        TextView textView = (TextView) findViewById(R.id.PageText);
        String t = objectName[scoutForm.pageId[page]] + " Match: " + match;
        textView.setText(t);
    }

    //Changing page
    public void pageSwap(View v) {
        int[] pageId = scoutForm.pageId;

        //Hide current page
        findViewById(pageId[page]).setVisibility(View.GONE);

        //Choose next page
        if(v.getId() == R.id.NextPage) {
            page++;
            if(objectName[pageId[page]].contains("?") && scouter != 2 && scouter != 5) page++;
        } else if(v.getId() == R.id.LastPage) {
            page--;
            if(objectName[pageId[page]].contains("?") && scouter != 2 && scouter != 5) page--;
        }

        //Show new page
        findViewById(pageId[page]).setVisibility(View.VISIBLE);

        //Displaying correct buttons
        findViewById(R.id.LastPage).setVisibility(View.VISIBLE);
        findViewById(R.id.NextPage).setVisibility(View.VISIBLE);
        if(page == 0 || page == pageId.length - 1) v.setVisibility(View.GONE);

        //Changing title
        TextView textView = (TextView) findViewById(R.id.PageText);
        if(page == pageId.length - 1) {
            String t = "Match: " + match;
            textView.setText(t);
            findViewById(R.id.Loading).setVisibility(View.VISIBLE);
            findViewById(R.id.Reverse).setVisibility(View.GONE);
            mainLayout.setVisibility(View.GONE);
            loading = true;
        } else {
            String t = objectName[pageId[page]] + " Match: " + match;
            textView.setText(t);
            mainLayout.setVisibility(View.VISIBLE);
            findViewById(R.id.Loading).setVisibility(View.GONE);
            findViewById(R.id.Reverse).setVisibility(View.VISIBLE);
            loading = false;
        }


        //Sending page update
        try {
            client.SendPacket("Page", scouter + "," + page + "," + match + "," + team);
        } catch(Exception ie) {//Check
        }
    }

    public void reverse(View v) {
        //Toggle reverse mode
        Switch aSwitch = (Switch) findViewById(R.id.Reverse);
        scoutForm.reverse = !scoutForm.reverse;
        aSwitch.setChecked(scoutForm.reverse);
    }

    public void onBackPressed() {
        //Disconnect from server
        try {
            if(connected) client.Disconnect();
        } catch(Exception e) {//Check
        }
    }

    public void TestLayout(View v) {
        objectNum = 10;
        scoutForm.pageId = new int[2];

        String[] name = {"Main page", "Column 1", "2", "Switch 1", "Button 1", "Switch 2", "Button 2", "Column 2", "Choice 1", "Choice 2", "Second page", "slider bar", "5"};
        int[] type = {1, 2, 6, 3, 4, 3, 4, 2, 5, 5, 1, 2, 8};

        objectName = name;
        objectType = type;
        objectValue = new int[objectNum];

        findViewById(R.id.startLayout).setVisibility(View.GONE);
        findViewById(R.id.TopLine).setVisibility(View.VISIBLE);
        Button button = (Button) findViewById(R.id.NextPage);
        String t = "Next Page -->";
        button.setText(t);
        button.setVisibility(View.VISIBLE);
        findViewById(R.id.Reverse).setVisibility(View.VISIBLE);
        findViewById(R.id.Refresh).setVisibility(View.VISIBLE);
        loadObjects(null);
    }
}
