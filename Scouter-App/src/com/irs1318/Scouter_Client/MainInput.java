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
    int currentCount = 0;
    int i;
    int page = 0;
    int column = 0;
    int scouter = -1;
    int lineLength = 0;
    int match = -1;
    int team = 0;
    String text;
    String lastMatch;
    String scoutName;
    String teamName = "";
    String[] changes = new String[6];
    boolean connected = false;
    boolean inRadio = false;
    boolean reverse = false;
    boolean loading = false;
    TCPClient client;
    TableLayout tableLayout;
    LinearLayout sideLayout;
    LinearLayout mainLayout;
    LinearLayout lineLayout;


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

            EditText editText = (EditText) findViewById(R.id.editText);
            text = String.valueOf(editText.getText());
            client = new TCPClient(11111, text);
            client.OnConnected.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    try {
                        client.SendPacket("Hello", String.valueOf(scouter));
                    } catch (Exception ie) {
                    }
                    connected = true;
                    setConnect();
                }
            });
            //When receiving data
            client.OnDataAvailable.add(new NetworkEvent() {
                @Override
                public void Call(TCPClient sender) {
                    NetworkPacket[] networkPackets = client.GetPackets();
                    for (i = 0; i < networkPackets.length; ++i) {
                        if (networkPackets[i].Name.equals("GameStart")) {
                            currentCount = 0;
                            objectNum = networkPackets[i].DataAsInt();
                            objectName = new String[objectNum];
                            objectType = new int[objectNum];
                            objectValue = new int[objectNum];
                            page = 0;
                        }
                        if (networkPackets[i].Name.equals("Game")) {
                            //Reading first Packets of data
                            objectName[currentCount] = networkPackets[i].Data.split(",")[0];
                            if(objectName[currentCount].contains("#")) objectName[currentCount] = objectName[currentCount].split("#")[0];
                            text = networkPackets[i].Data.split(",")[1];
                            objectType[currentCount] = Integer.valueOf(text);
                            if (objectType[currentCount] == 1) page++;
                            currentCount++;
                        }
                        if (networkPackets[i].Name.equals("Match")) {
                            if (!networkPackets[i].Data.equals(lastMatch)) {
                                if(match != -1) page = 0;
                                match = Integer.valueOf(networkPackets[i].Data.split(",")[0]);
                                team = Integer.valueOf(networkPackets[i].Data.split(",")[1]);
                                teamName = networkPackets[i].Data.split(",")[2];
                                lastMatch = networkPackets[i].Data;
                                loading = false;

                                Handler mainHandle = new Handler(getMainLooper());
                                mainHandle.post(new Runnable() {
                                    @Override
                                    public void run() {
                                        TextView textView = (TextView) findViewById(R.id.teamName);
                                        textView.setText(team + " " + teamName + " " + scoutName);
                                        for (int j = i; j < objectNum; j++) objectValue[j] = 0;

                                        //Showing required parts
                                        Button button = (Button) findViewById(R.id.NextPage);
                                        button.setText("Next Page -->");
                                        button.setVisibility(View.VISIBLE);
                                        findViewById(R.id.Loading).setVisibility(View.GONE);
                                        findViewById(R.id.LastPage).setVisibility(View.GONE);
                                        findViewById(R.id.Reverse).setVisibility(View.VISIBLE);
                                        findViewById(R.id.Refresh).setVisibility(View.VISIBLE);
                                        loadObjects(null);
                                    }
                                });
                                try {
                                    if (connected)
                                        client.SendPacket("Page", scouter + ",0" + "," + match + "," + team);
                                } catch (IOException ie) {
                                }
                            }
                        }
                        if (networkPackets[i].Name.equals("DefenseInfo")) {
                            if (scouter > 2) text = networkPackets[i].Data.split("&")[0];
                            else text = networkPackets[i].Data.split("&")[1];
                            for (int j = 0; j < changes.length; j++) {
                                changes[j] = text.split(",")[j];
                            }
                            Handler mainHandle = new Handler(getMainLooper());
                            mainHandle.post(new Runnable() {
                                @Override
                                public void run() {
                                    loadObjects(null);
                                }
                            });
                        }
                        if (networkPackets[i].Name.equals("GameEnd")) {
                            pageId = new int[page + 1];
                            page = 0;
                            if (match == -1) loading = true;
                            setConnect();
                            Handler mainHandle = new Handler(getMainLooper());
                            mainHandle.post(new Runnable() {
                                @Override
                                public void run() {
                                    findViewById(R.id.startLayout).setVisibility(View.GONE);
                                    findViewById(R.id.TopLine).setVisibility(View.VISIBLE);
                                    if (loading)
                                        findViewById(R.id.Loading).setVisibility(View.VISIBLE);
                                }
                            });
                        }
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
            try {
                client.Connect();
            } catch (Exception e) {
            }
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

    public void loadObjects(View v) {
        //Removing old layout
        LinearLayout linearLayout = (LinearLayout) findViewById(R.id.mainLayout);
        linearLayout.removeView(mainLayout);

        //Adding essential variables
        linearLayout = (LinearLayout) findViewById(R.id.mainLayout);
        mainLayout = new LinearLayout(this);
        mainLayout.setGravity(1);
        linearLayout.addView(mainLayout);

        sideLayout = new LinearLayout(this);
        tableLayout = new TableLayout(this);

        makeLine();
        int newPage = 0;
        text = objectName[0];

        //Creating actual form
        for (i = 0; i < objectNum; i++) {
            text = objectName[i];
            switch (objectType[i]) {
                case 1:
                    //Page
                    linearLayout = new LinearLayout(this);
                    linearLayout.setId(i);
                    linearLayout.setGravity(1);
                    if(newPage != page) linearLayout.setVisibility(View.GONE);
                    mainLayout.addView(linearLayout);

                    //Adjusting variables
                    pageId[newPage] = i;
                    newPage++;
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
                    TextView textView = new TextView(this);
                    makeView(textView, sideLayout);
                    textView.setTextSize(25);
                    textView.setTextColor(Color.rgb(249,178,52));

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
                    if(objectValue[i] == 1) aSwitch.setChecked(true);
                    aSwitch.setGravity(1);
                    makeView(aSwitch, switchLayout);
                    break;
                case 4:
                    //Count
                    Button button = new Button(this);
                    button.setOnClickListener(clickListener);
                    button.setId(i);
                    text = objectName[i] + ": " + objectValue[i];
                    makeView(button, lineLayout);
                    break;
                case 5:
                    //Choice
                    //Choice Group
                    LinearLayout radioGroup = new LinearLayout(this);
                    radioGroup.setOnClickListener(clickListener);
                    radioGroup.setId(i + objectNum);
                    radioGroup.setOrientation(LinearLayout.HORIZONTAL);
                    lineLayout.addView(radioGroup);

                    //Button label
                    textView = new TextView(this);
                    makeView(textView,radioGroup);
                    column--;
                    text = "";

                    //Button
                    RadioButton radioButton = new RadioButton(this);
                    radioButton.setId(i);
                    if(objectValue[i] == 1) radioButton.setChecked(true);
                    radioButton.setOnClickListener(clickListener);
                    makeView(radioButton, radioGroup);
                    break;
                case 6:
                    //Line
                    //New table
                    tableLayout = new TableLayout(this);
                    sideLayout.addView(tableLayout);

                    //New line
                    lineLength = Integer.valueOf(objectName[i]);
                    makeLine();
                    break;
                case 7:
                    //Label
                    textView = new TextView(this);
                    makeView(textView, lineLayout);
                    textView.setTextSize(25);
                    textView.setTextColor(Color.rgb(249,178,52));
                    break;
                case 8:
                    //Change
                    textView = new TextView(this);
                    text = changes[Integer.valueOf(objectName[i]) - 1];
                    makeView(textView, lineLayout);
                    textView.setTextColor(Color.rgb(249,178,52));
                    break;
            }
        }
        i = 0;

        //Changing Title
        TextView textView = (TextView) findViewById(R.id.PageText);
        textView.setText(objectName[pageId[page]] + " Match: " + match);
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
        textView.setHighlightColor(Color.rgb(54,179,222));
        textView.setTextColor(Color.LTGRAY);
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
        if (v.getId() == R.id.NextPage) {
            page++;
            if(objectName[pageId[page]].contains("?")) page++;
        } else if (v.getId() == R.id.LastPage) {
            page--;
            if(objectName[pageId[page]].contains("?")) page--;
        }
        findViewById(pageId[page]).setVisibility(View.VISIBLE);

        //Displaying correct buttons
        findViewById(R.id.LastPage).setVisibility(View.VISIBLE);
        findViewById(R.id.NextPage).setVisibility(View.VISIBLE);
        if (page == 0 || page == pageId.length - 1) v.setVisibility(View.GONE);
        Button button = (Button) findViewById(R.id.NextPage);
        if(page == pageId.length - 2) button.setText("Next Match -->");
        else button.setText("Next Page -->");


        //Changing title
        TextView textView = (TextView) findViewById(R.id.PageText);
        if(page == pageId.length - 1) {
            textView.setText("Match: " + match);
            findViewById(R.id.Loading).setVisibility(View.VISIBLE);
            findViewById(R.id.Reverse).setVisibility(View.GONE);
            mainLayout.setVisibility(View.GONE);
            loading = true;
        }
        else {
            textView.setText(objectName[pageId[page]] + " Match: " + match);
            mainLayout.setVisibility(View.VISIBLE);
            findViewById(R.id.Loading).setVisibility(View.GONE);
            findViewById(R.id.Reverse).setVisibility(View.VISIBLE);
            loading = false;
        }


        //Sending page update
        try {
            if (connected) client.SendPacket("Page", scouter + "," + page + "," + match + "," + team);
        } catch (IOException ie) {
        }
    }

    public void reverse(View v) {
        Switch aSwitch = (Switch) findViewById(R.id.Reverse);
        if(!reverse) reverse = true;
        else reverse = false;
        aSwitch.setChecked(reverse);
    }

    public void onBackPressed() {
        try {
            if (connected) client.Disconnect();
        } catch (Exception e) {
        }
    }

    Button.OnClickListener clickListener = new Button.OnClickListener() {
        @Override
        public void onClick(View v) {

            //Finding clicked object
            i = v.getId();
            if(i >= objectNum) i = i - objectNum;
            boolean changed = false;

            //Making background changes
            switch (objectType[i]) {
                case 3:
                    //Switch
                    Switch aSwitch = (Switch) findViewById(i);
                    if(objectValue[i] == 1) {
                        aSwitch.setChecked(false);
                        objectValue[i] = 0;
                        changed = true;
                        reverse = true;
                    }
                    else if(!reverse) {
                        aSwitch.setChecked(true);
                        objectValue[i] = 1;
                        changed = true;
                    }
                    break;
                case 4:
                    //Count
                    Button button = (Button) v;
                    if(!reverse) {
                        objectValue[i]++;
                        changed = true;
                    } else if(objectValue[i] > 0) {
                        objectValue[i]--;
                        changed = true;
                    }
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
                        objectValue[j] = 0;
                        if(i == j && !reverse) {
                            radioButton.setChecked(true);
                            changed = true;
                        } else if(radioButton.isChecked()) {
                            radioButton.setChecked(false);
                            try {
                                if (connected) client.SendPacket("Undo", scouter + "," + j);
                            } catch (IOException ie) {}
                        }
                        j--;
                    }
                    objectValue[i] = 1;
                    break;
            }
            if(reverse) text = "Undo";
            else text = "Event";
            //Notifying server of change
            try {
                if (connected && changed) client.SendPacket(text, scouter + "," + i);
            } catch (IOException ie) {
            }
            Switch aSwitch = (Switch) findViewById(R.id.Reverse);
            reverse = aSwitch.isChecked();
        }
    };
}
