package com.irs1318.Scouter_Client;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;
import com.irs1318.Scouter_Client.Net.*;

import java.io.IOException;

public class MainInput extends Activity {
    //Basic variables
    int objectNum;
    int i;
    int page = 0;
    int column = 0;
    int scouter = 0;
    int lineLength = 5;
    int match = 0;
    String text;
    String team = "1318 IRS";
    boolean connected = false;
    boolean inRadio = false;
    boolean reverse = false;
    TCPClient client;
    TableLayout tableLayout;
    LinearLayout sideLayout;
    LinearLayout lineLayout;
    LinearLayout linearLayout;


    //Complex variables
    int[] pageId;
    int[] objectType;
    int[] objectValue;
    String[] objectName;

    //Setting up the Activity
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        if(getActionBar() != null) getActionBar().hide();
    }

    //Connecting to PC
    public void connect(View v) {
        setConnect();
        if (!connected) {
            switch (v.getId()) {
                case R.id.S0:
                    scouter = 0;
                    break;
                case R.id.S1:
                    scouter = 1;
                    break;
                case R.id.S2:
                    scouter = 2;
                    break;
                case R.id.S3:
                    scouter = 3;
                    break;
                case R.id.S4:
                    scouter = 4;
                    break;
                case R.id.S5:
                    scouter = 5;
                    break;
            }
            EditText editText = (EditText) findViewById(R.id.editText);
            client = new TCPClient(11111, editText.getText().toString());
            client.OnConnected.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    try {
                        client.SendPacket("Hello", String.valueOf(scouter));
                    } catch (IOException ie) {
                        int gfd =3;
                    }
                    connected = true;
                    setConnect();
                }
            });
            try {
                client.Connect();
            } catch (Exception e) {
                e.toString();
            }
            //When receiving data
            client.OnDataAvailable.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    NetworkPacket[] networkPackets = client.GetPackets();
                    if (networkPackets[0].Name.equals("Game")) {
                        //Reading first Packets of data
                        objectNum = networkPackets.length;
                        objectName = new String[objectNum];
                        objectType = new int[objectNum];
                        objectValue = new int[objectNum];
                        for (i = 0; i < networkPackets.length && networkPackets[0].Name.equals("Game"); i++) {
                            objectName[i] = networkPackets[i].Data.split(",")[0];
                            text = networkPackets[i].Data.split(",")[1];
                            objectType[i] = Integer.valueOf(text);
                            if (objectType[i] == 1) page++;
                        }
                        pageId = new int[page];
                        connected = true;
                        setConnect();

                        Handler mainHandle = new Handler(getMainLooper());

                        mainHandle.post(new Runnable() {
                            @Override
                            public void run() {

                                loadObjects();
                            }
                        });
                    }
                }
            });
            client.OnDisconnected.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    connected = false;
                    setConnect();
                }
            });
        }
    }

    public void setConnect() {
        Handler mainHandle = new Handler(getMainLooper());

        mainHandle.post(new Runnable() {
            @Override
            public void run() {
                RadioButton radioButton = (RadioButton) findViewById(R.id.Connect);
                radioButton.setChecked(connected);
            }
        });
    }

    //Setting alternate values for testing
    public void noConnect(View v) {
        objectNum = 100;
        i = 0;
        int j;
        pageId = new int[2];
        objectName = new String[objectNum];
        objectType = new int[objectNum];
        objectValue = new int[objectNum];
        makeObject("Auto",1);
        makeObject("OuterWorks",2);
        makeObject("Low Bar",7);
        for(j = 0; j < 4; j++) makeObject("Slot " + (j + 1),7);
        for(j = 0; j < 5; j++) makeObject("Start",5);
        for(j = 0; j < 5; j++) makeObject("Cross",4);
        for(j = 0; j < 5; j++) makeObject("End",5);
        makeObject("2",6);
        makeObject("Spy",3);
        makeObject("Reach",3);
        makeObject("Fouls",7);
        makeObject("2",6);
        makeObject("Foul",4);
        makeObject("Tech Foul",4);
        makeObject("Scoring",2);
        makeObject("2",6);
        makeObject("Passage High",4);
        makeObject("Passage Low",4);
        makeObject("Center High",4);
        makeObject("Miss",4);
        makeObject("Bar High",4);
        makeObject("Bar Low",4);
        makeObject("Possession",7);
        makeObject("1",6);
        makeObject("Start with ball",3);
        makeObject("End with ball",3);
        makeObject("Teleop",1);
        makeObject("OuterWorks",2);
        makeObject("5",6);
        makeObject("Low Bar",7);
        for(j = 0; j < 4; j++) makeObject("Slot " + (j + 1),7);
        for(j = 0; j < 5; j++) makeObject("Cross",4);
        for(j = 0; j < 5; j++) makeObject("Help",4);
        makeObject("Defending",7);
        makeObject("4",6);
        makeObject("Pin",4);
        makeObject("Block Shot",4);
        makeObject("Take Ball",4);
        makeObject("Block Cross",4);
        makeObject("Fouls",7);
        makeObject("2",6);
        makeObject("Foul",4);
        makeObject("Tech Foul",4);
        makeObject("Scoring",2);
        makeObject("2",6);
        makeObject("Passage High",4);
        makeObject("Passage Low",4);
        makeObject("Center High",4);
        makeObject("Miss",4);
        makeObject("Bar High",4);
        makeObject("Bar Low",4);
        makeObject("Retrieval",7);
        makeObject("3",6);
        makeObject("Castle High",4);
        makeObject("Castle Low ",4);
        makeObject("Ground",4);
        makeObject("End Game",7);
        makeObject("2",6);
        makeObject("Challenge",5);
        makeObject("Scale",5);
        loadObjects();
    }

    public void makeObject(String name, int type) {
        objectName[i] = name;
        objectType[i] = type;
        i++;
    }

    public void loadObjects() {
        //Showing required parts
        findViewById(R.id.startLayout).setVisibility(View.GONE);
        findViewById(R.id.next).setVisibility(View.VISIBLE);
        findViewById(R.id.Reverse).setVisibility(View.VISIBLE);

        //Adding essential variables
        LinearLayout mainLayout = (LinearLayout) findViewById(R.id.mainLayout);
        LinearLayout radioGroup = new LinearLayout(this);
        linearLayout = new LinearLayout(this);
        sideLayout = new LinearLayout(this);
        tableLayout = new TableLayout(this);
        int currentRadio = 0;
        makeLine();
        page = 0;

        //Changing Title
        TextView textView = (TextView) findViewById(R.id.PageText);
        textView.setText(objectName[0]);

        //Creating actual form
        for (i = 0; i < objectNum; i++) {
            text = objectName[i];
            switch (objectType[i]) {
                case 1:
                    //Page
                    linearLayout = new LinearLayout(this);
                    linearLayout.setId(i);
                    linearLayout.setGravity(1);
                    if (page != 0) linearLayout.setVisibility(View.GONE);
                    mainLayout.addView(linearLayout);

                    //Adjusting variables
                    pageId[page] = i;
                    page++;
                    break;
                case 2:
                    //Category
                    //First Divider
                    TextView divider = new TextView(this);
                    divider.setWidth(5);
                    divider.setBackgroundColor(Color.LTGRAY);
                    divider.setHeight(650);
                    linearLayout.addView(divider);
                    Space space = new Space(this);
                    space.setMinimumWidth(5);
                    linearLayout.addView(space);

                    sideLayout = new LinearLayout(this);
                    sideLayout.setGravity(1);
                    sideLayout.setOrientation(LinearLayout.VERTICAL);
                    linearLayout.addView(sideLayout);

                    //Second divider
                    space = new Space(this);
                    space.setMinimumWidth(5);
                    linearLayout.addView(space);
                    divider = new TextView(this);
                    divider.setWidth(5);
                    divider.setBackgroundColor(Color.LTGRAY);
                    divider.setHeight(650);
                    linearLayout.addView(divider);


                    //Labelling
                    textView = new TextView(this);
                    textView.setGravity(1);
                    makeView(textView, sideLayout);
                    textView.setTextSize(25);

                    tableLayout = new TableLayout(this);
                    sideLayout.addView(tableLayout);
                    makeLine();

                    break;

                case 3:
                    //Switch
                    LinearLayout switchLayout = new LinearLayout(this);
                    switchLayout.setOrientation(LinearLayout.VERTICAL);
                    switchLayout.setId(i + objectNum);
                    switchLayout.setOnClickListener(clickListener);
                    lineLayout.addView(switchLayout);
                    textView = new TextView(this);
                    makeView(textView,switchLayout);
                    column--;
                    text = "";
                    Switch aSwitch = new Switch(this);
                    aSwitch.setOnClickListener(clickListener);
                    aSwitch.setId(i);
                    aSwitch.setGravity(1);
                    makeView(aSwitch, switchLayout);
                    break;
                case 4:
                    //Count
                    Button button = new Button(this);
                    button.setOnClickListener(clickListener);
                    button.setId(i);
                    text = objectName[i] + ": 0";
                    makeView(button, lineLayout);
                    break;
                case 5:
                    //Choice
                    radioGroup = new LinearLayout(this);
                    radioGroup.setOnClickListener(clickListener);
                    radioGroup.setId(i + objectNum);
                    radioGroup.setOrientation(LinearLayout.HORIZONTAL);
                    lineLayout.addView(radioGroup);
                    textView = new TextView(this);
                    makeView(textView,radioGroup);
                    column--;
                    text = "";
                    RadioButton radioButton = new RadioButton(this);
                    radioButton.setId(i);
                    radioButton.setOnClickListener(clickListener);
                    makeView(radioButton, radioGroup);
                    break;
                case 6:
                    //Line
                    tableLayout = new TableLayout(this);
                    sideLayout.addView(tableLayout);

                    lineLength = Integer.valueOf(objectName[i]);
                    makeLine();
                    break;
                case 7:
                    //Label
                    textView = new TextView(this);
                    textView.setGravity(1);
                    makeView(textView, lineLayout);
                    textView.setTextSize(25);
                    break;
            }
        }
        i = 0;
        page = 0;
    }

    //Creating a grid for other objects
    public void makeLine() {

        //New line
        lineLayout = new TableRow(this);
        lineLayout.setGravity(1);
        tableLayout.addView(lineLayout);

        //Resetting other variables
        column = 0;
        inRadio = false;
    }

    //Finalizing the object
    public void makeView(TextView textView, ViewGroup viewGroup) {
        //Formatting and adding object
        textView.setText(text);
        textView.setTextSize(20);
        textView.setGravity(1);
        textView.setTextColor(Color.rgb(255,255,77));
        viewGroup.addView(textView);

        //Checking for end of row
        if(objectType[i] > 2) column++;
        else column = 0;
        if(column == lineLength) makeLine();
    }

    //Changing page
    public void pageSwap(View v) {
        //Displaying correct page
        findViewById(pageId[page]).setVisibility(View.GONE);
        if (v.getId() == R.id.next) page++;
        if (v.getId() == R.id.last) page--;
        findViewById(pageId[page]).setVisibility(View.VISIBLE);

        //Displaying correct buttons
        findViewById(R.id.last).setVisibility(View.VISIBLE);
        findViewById(R.id.next).setVisibility(View.VISIBLE);
        if (page == 0 || page == pageId.length - 1) v.setVisibility(View.INVISIBLE);

        //Changing title
        TextView textView = (TextView) findViewById(R.id.PageText);
        textView.setText(objectName[pageId[page]]);

        //Sending page update
        try {
            if (connected) client.SendPacket("Page", scouter + "," + page + "," + match + "," + team);
        } catch (IOException ie) {
        }
    }

    public void reverse(View v) {
        Switch aSwitch = (Switch) v;
        reverse = aSwitch.isChecked();
    }

    Button.OnClickListener clickListener = new Button.OnClickListener() {
        @Override
        public void onClick(View v) {

            //Finding clicked object
            i = v.getId();
            if(i >= objectNum) i = i - objectNum;

            //Making background changes
            switch (objectType[i]) {
                case 3:
                    //Switch
                    Switch aSwitch = (Switch) findViewById(i);
                    if(objectValue[i] == 1) {
                        aSwitch.setChecked(false);
                        objectValue[i] = 0;
                    }
                    else {
                        aSwitch.setChecked(true);
                        objectValue[i] = 1;
                    }
                    break;
                case 4:
                    //Count
                    Button button = (Button) v;
                    if(!reverse) objectValue[i]++;
                    else if(objectValue[i] > 0) objectValue[i]--;
                    text = objectName[i] + ": " + objectValue[i];
                    button.setText(text);
                    break;
                case 5:
                    //Choice
                    int j = i;
                    while(objectType[j] == 5) j++;
                    j--;
                    while(objectType[j] == 5) {
                        RadioButton radioButton = (RadioButton) findViewById(j);
                        radioButton.setChecked(false);
                        objectValue[j] = 0;
                        if(i == j && !reverse) radioButton.setChecked(true);
                        j--;
                    }
                    objectValue[i] = 1;
                    break;
            }
            if(reverse) text = "Undo";
            else text = "Event";
            //Notifying server of change
            try {
                if (connected) client.SendPacket(text, scouter + "," + i);
            } catch (IOException ie) {
            }
        }
    };
}