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
import java.util.*;

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
    Stack<Integer> history = new Stack<>();
    Stack<Integer> radioHistory = new Stack<>();

    //Setting up the Activity
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        if(getActionBar() != null) getActionBar().hide();
    }

    //Connecting to PC
    public void connect(View v) {
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
                if (networkPackets[0].Name == "Game") {
                    //Reading first Packets of data
                    objectNum = networkPackets.length;
                    objectName = new String[objectNum];
                    objectType = new int[objectNum];
                    objectValue = new int[objectNum];
                    for (i = 0; i < networkPackets.length; i++) {
                        objectName[i] = networkPackets[i].Data.split(",")[0];
                        text = networkPackets[i].Data.split(",")[1];
                        objectType[i] = Integer.valueOf(text);
                        if (objectType[i] == 1) page++;
                    }
                    pageId = new int[page];
                    connected = true;

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
    }

    //Setting alternate values for testing
    public void noConnect(View v) {
        objectNum = 42;
        pageId = new int[3];
        objectName = new String[objectNum];
        objectType = new int[objectNum];
        objectValue = new int[objectNum];
        objectName[0] = "Auto";
        objectType[0] = 1;
        objectName[1] = "OuterWorks";
        objectType[1] = 2;
        objectName[2] = "Low Bar";
        objectType[2] = 8;
        for(i = 3; i < 7; i++) {
            objectName[i] = "Slot" + String.valueOf(i - 2);
            objectType[i] = 8;
        }
        for(i = 7; i < 12; i++) {
            objectName[i] = "Start";
            objectType[i] = 3;
        }
        for(i = 12; i < 17; i++) {
            objectName[i] = "Reach";
            objectType[i] = 3;
        }
        for(i = 17; i < 22; i++) {
            objectName[i] = "Cross";
            objectType[i] = 4;
        }
        for(i = 22; i < 27; i++) {
            objectName[i] = "back";
            objectType[i] = 4;
        }
        for(i = 27; i < 32; i++) {
            objectName[i] = "End";
            objectType[i] = 3;
        }
        objectName[32] = "Scoring";
        objectType[32] = 2;
        objectName[33] = "Start with ball";
        objectType[33] = 3;
        objectName[34] = "End with ball";
        objectType[34] = 3;
        objectName[35] = "3";
        objectType[35] = 7;
        objectName[36] = "Passage High";
        objectType[36] = 4;
        objectName[37] = "Center High";
        objectType[37] = 4;
        objectName[38] = "Low bar High";
        objectType[38] = 4;
        objectName[39] = "Passage Low";
        objectType[39] = 4;
        objectName[40] = "Miss";
        objectType[40] = 4;
        objectName[41] = "Low bar Low";
        objectType[41] = 4;



        loadObjects();
    }

    public void loadObjects() {
        //Showing required parts
        findViewById(R.id.startLayout).setVisibility(View.GONE);
        findViewById(R.id.next).setVisibility(View.VISIBLE);
        findViewById(R.id.undo).setVisibility(View.VISIBLE);

        //Adding essential variables
        LinearLayout mainLayout = (LinearLayout) findViewById(R.id.mainLayout);
        RadioGroup radioGroup = new RadioGroup(this);
        linearLayout = new LinearLayout(this);
        sideLayout = new LinearLayout(this);
        tableLayout = new TableLayout(this);
        int currentRadio = 0;
        makeLine();
        page = 0;

        //Creating actual form
        for (i = 0; i < objectNum; i++) {
            text = objectName[i];
            switch (objectType[i]) {
                case 1:
                    //Page
                    linearLayout = new LinearLayout(this);
                    linearLayout.setId(i);
                    if (page != 0) linearLayout.setVisibility(View.GONE);
                    mainLayout.addView(linearLayout);

                    //Adjusting variables
                    pageId[page] = i;
                    page++;
                    break;
                case 2:
                    //Category
                    sideLayout = new LinearLayout(this);
                    linearLayout.addView(sideLayout);

                    TextView textView = new TextView(this);
                    makeView(textView, sideLayout);

                    tableLayout = new TableLayout(this);
                    sideLayout.addView(tableLayout);
                    sideLayout.setOrientation(LinearLayout.VERTICAL);
                    makeLine();
                    break;

                case 3:
                    //Check
                    CheckBox checkBox = new CheckBox(this);
                    makeView(checkBox, lineLayout);
                    break;
                case 4:
                    //Count
                    Button button = new Button(this);
                    text = objectName[i] + ": 0";
                    makeView(button, lineLayout);
                    break;
                case 5:
                    //Choice
                    if (!inRadio) {
                        //Group for choices
                        radioGroup = new RadioGroup(this);
                        radioGroup.setOrientation(LinearLayout.HORIZONTAL);
                        linearLayout.addView(radioGroup);
                        inRadio = true;
                        currentRadio++;
                    }

                    //Choice
                    RadioButton radioButton = new RadioButton(this);
                    makeView(radioButton, lineLayout);
                    objectValue[i] = currentRadio;
                    break;
                case 6:
                    //Fade
                    radioButton = new RadioButton(this);
                    makeView(radioButton, lineLayout);
                    break;
                case 7:
                    //Line
                    lineLength = Integer.valueOf(objectName[i]);
                    makeLine();
                    break;
                case 8:
                    //Label
                    textView = new TextView(this);
                    makeView(textView, lineLayout);
                    break;
            }
        }
        i = 0;
        page = 0;
    }

    //Creating a grid for other objects
    public void makeLine() {
        //Adding more space
        Space space = new Space(this);
        space.setMinimumHeight(20);
        tableLayout.addView(space);

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
        textView.setId(i);
        textView.setText(text);
        if (objectType[i] > 2) textView.setOnClickListener(clickListener);
        textView.setTextSize(20);
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
        if (page == 0 || page == pageId.length - 1) v.setVisibility(View.INVISIBLE);
        else {
            findViewById(R.id.last).setVisibility(View.VISIBLE);
            findViewById(R.id.next).setVisibility(View.VISIBLE);
        }

        //Sending page update
        try {
            if (connected) client.SendPacket("Page", scouter + "," + page + "," + match);
        } catch (IOException ie) {
        }
    }

    public void undo(View v) {
        if (!history.empty()) {

            //Locating last change
            i = history.pop();

            //Changing display to remove change
            switch (objectType[i]) {
                case 3:
                    //Check
                    CheckBox checkBox = (CheckBox) findViewById(i);
                    if (checkBox.isChecked()) checkBox.setChecked(false);
                    else checkBox.setChecked(true);
                    break;
                case 4:
                    //Count
                    if (objectValue[i] > 0) {
                        Button button = (Button) findViewById(i);
                        objectValue[i]--;
                        text = objectName[i] + ": " + objectValue[i];
                        button.setText(text);
                    }
                    break;
                case 5:
                    //Choice
                    RadioButton radioButton = (RadioButton) findViewById(i);
                    radioButton.setChecked(false);
                    if (!radioHistory.empty()) {
                        radioHistory.pop();
                        if (!radioHistory.empty()) {
                            //Checking previous choice
                            Stack<Integer> radioHistoryClone = radioHistory;
                            int j = radioHistoryClone.peek();
                            while (objectValue[j] != objectValue[i] && !radioHistoryClone.empty())
                                j = radioHistoryClone.pop();
                            radioButton = (RadioButton) findViewById(j);
                            radioButton.setChecked(true);
                        }
                    }
                    break;
                case 6:
                    //Fade
                    radioButton = (RadioButton) findViewById(i);
                    radioButton.setChecked(false);
                    radioButton.setVisibility(View.VISIBLE);
            }

            //Sending undo message
            try {
                if (connected) client.SendPacket("Undo", String.valueOf(scouter));
            } catch (IOException ie) {
            }
        }
    }

    Button.OnClickListener clickListener = new Button.OnClickListener() {
        @Override
        public void onClick(View v) {

            //Finding clicked object
            i = v.getId();

            //Making background changes
            switch (objectType[i]) {
                case 3:
                    //Check
                    CheckBox checkBox = (CheckBox) v;
                    if (checkBox.isChecked()) objectValue[i] = 1;
                    else objectValue[i] = 0;
                    break;
                case 4:
                    //Count
                    Button button = (Button) v;
                    objectValue[i]++;
                    text = objectName[i] + ": " + objectValue[i];
                    button.setText(text);
                    break;
                case 5:
                    //Check
                    radioHistory.push(i);
                    break;
                case 6:
                    //Fade
                    v.setVisibility(View.GONE);
                    break;
            }

            //Notifying server of change
            try {
                if (connected) client.SendPacket("Event", scouter + "," + i);
            } catch (IOException ie) {
            }

            //Documenting change
            history.push(i);
        }
    };
}