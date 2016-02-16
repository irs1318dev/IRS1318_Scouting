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
    int match = 0;
    String text;
    String team = "1318 IRS";
    boolean connected = false;
    boolean inRadio = false;
    TCPClient client;
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
        RelativeLayout relativeLayout = (RelativeLayout) findViewById(R.id.Title);
        relativeLayout.setBackgroundColor(Color.rgb(82,0,204));
        TextView last = (TextView) findViewById(R.id.last);
        last.setTextColor(Color.rgb(255,255,77));
        last.setTextSize(40);
        TextView next = (TextView) findViewById(R.id.next);
        next.setTextSize(40);
        next.setTextColor(Color.rgb(255,255,77));
        TextView undo = (TextView) findViewById(R.id.undo);
        undo.setTextSize(40);
        undo.setTextColor(Color.rgb(255,255,77));
        TextView teamNum = (TextView) findViewById(R.id.teamNum);
        teamNum.setTextColor(Color.rgb(255,255,77));
        teamNum.setTextSize(42);
    }

    //Connecting to PC
    public void connect(View v) {
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
        objectNum = 30;
        pageId = new int[3];
        objectName = new String[objectNum];
        objectType = new int[objectNum];
        objectValue = new int[objectNum];
        for (i = 0; i < objectNum; i++) {
            switch (i) {
                case 0:
                    objectName[i] = "First Page";
                    objectType[i] = 1;
                    break;
                case 1:
                    objectName[1] = "First Set";
                    objectType[1] = 2;
                    break;
                case 2:case 3:case 4:
                    objectName[i] = "Multiple Choice";
                    objectType[i] = 5;
                    break;
                case 5:
                    objectName[i] = "Second Set";
                    objectType[i] = 2;
                    break;
                case 6:case 7:
                    objectName[i] = "Buttons";
                    objectType[i] = 4;
                    break;
                case 8:
                    objectName[i] = "Second Page";
                    objectType[i] = 1;
                    break;
                case 9:
                    objectName[i] = "First Set";
                    objectType[i] = 2;
                    break;
                case 10:case 11:case 12:case 13:case 14:case 15:case 16:case 17:case 18:
                    objectName[i] = "Switches";
                    objectType[i] = 3;
                    break;
                case 19:
                    objectName[i] = "Third Page";
                    objectType[i] = 1;
                    break;
                case 20:
                    objectName[i] = "Team Select";
                    objectType[i] = 2;
                    break;
                case 21:case 22:case 23:case 24:case 25:case 26:case 27:case 28:case 29:
                    objectName[i] = "Team " + i + i;
                    objectType[i] = 6;
                    break;
            }
        }
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
        int currentRadio = 0;
        makeLine();
        page = 0;

        //Creating actual form
        for (i = 0; i < objectNum; i++) {
            text = objectName[i];
            switch (objectType[i]) {
                case 1:
                    //Page
                    //Increasing space between lines
                    Space space = new Space(this);
                    space.setMinimumHeight(20);
                    linearLayout.addView(space);

                    //Creating new page
                    linearLayout = new LinearLayout(this);
                    linearLayout.setId(i);
                    linearLayout.setOrientation(LinearLayout.VERTICAL);
                    if (page != 0) linearLayout.setVisibility(View.GONE);
                    mainLayout.addView(linearLayout);

                    //Adjusting variables
                    makeLine();
                    pageId[page] = i;
                    page++;
                    break;
                case 2:
                    //Category
                    TextView textView = new TextView(this);
                    textView.setGravity(1);
                    makeView(textView, linearLayout);
                    textView.setTextSize(textView.getTextSize() + 1);
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
                        lineLayout.addView(radioGroup);
                        inRadio = true;
                        currentRadio++;
                    }

                    //Choice
                    RadioButton radioButton = new RadioButton(this);
                    makeView(radioButton, radioGroup);
                    objectValue[i] = currentRadio;
                    break;
                case 6:
                    //Fade
                    radioButton = new RadioButton(this);
                    makeView(radioButton, lineLayout);
                    break;
                case 7:
                    //Line
                    makeLine();
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
        linearLayout.addView(space);

        //New line
        lineLayout = new LinearLayout(this);
        lineLayout.setGravity(1);
        linearLayout.addView(lineLayout);

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
        textView.setTextSize(25);
        textView.setTextColor(Color.rgb(255,255,77));
        viewGroup.addView(textView);

        //Checking for end of row
        if(viewGroup == lineLayout) column++;
        else column = 0;
        if(column > 4) makeLine();
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