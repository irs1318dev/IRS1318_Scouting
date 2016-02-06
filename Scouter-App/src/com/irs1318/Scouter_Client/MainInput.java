package com.irs1318.Scouter_Client;

import android.app.Activity;
import android.os.Bundle;
import android.view.View;
import android.widget.*;
import com.irs1318.Scouter_Client.Net.*;
import java.io.IOException;

public class MainInput extends Activity {
    int objectNum = 24;
    int pageId = 0;
    int i;
    String text;
    String team = "1318 IRS";
    boolean connected = false;
    TCPClient client;
    GridLayout gridLayout;
    LinearLayout linearLayout;

    int[] objectType = new int[objectNum];
    int[] objectValue = new int[objectNum];
    String[] objectName = new String[objectNum];

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
    }

    public void connect(View v) {
        EditText editText = (EditText) findViewById(R.id.editText);
        client = new TCPClient(11111, editText.getText().toString());
        client.OnDataAvailable.add(new NetworkEvent()
        {
            @Override
            public void Call(TCPClient sender) throws Exception
            {
                NetworkPacket[] networkPackets = client.GetPackets();
                if(!connected) {
                    for (i = 0; i < networkPackets.length; i++) {
                        objectName[i] = networkPackets[i].Name;
                        objectType[i] = networkPackets[i].DataAsInt();
                    }
                    connected = true;
                    loadObjects();
                }
            }
        });
    }

    public void noConnect(View v) {
        for (i = 1; i < objectNum; i++) {
            switch(i) {
                case 0:
                    objectName[i] = "First Page";
                    break;
                case 1:
                    objectName[1] = "First Set";
                    objectType[1] = 2;
                    break;
                case 2:case 3:case 4:
                    objectName[i] = "Multiple Choice";
                    objectType[i] = 6;
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
                    objectName[i] = "Second Set";
                    objectType[i] = 2;
                    break;
                case 20:
                    objectName[i] = "Seek-Bar";
                    objectType[i] = 5;
                    break;
                case 21:
                    objectName[i] = "First Page";
                    objectType[i] = 7;
                    break;
                case 22:
                    objectName[i] = "Third Page";
                    objectType[i] = 1;
                    break;
                case 23:
                    objectName[i] = "Second Page";
                    objectType[i] = 7;
                    break;
            }
        }
        loadObjects();
    }

    public void loadObjects() {
        findViewById(R.id.startLayout).setVisibility(View.GONE);

        LinearLayout mainLayout = (LinearLayout) findViewById(R.id.mainLayout);

        linearLayout = new LinearLayout(this);
        linearLayout.setId(0);
        linearLayout.setOrientation(LinearLayout.VERTICAL);
        mainLayout.addView(linearLayout);

        makeGrid();
        boolean inRadio = false;
        boolean newPage = false;
        int oldPageId = 0;

        for(i = 1; i < objectNum; i++) {
            text = objectName[i];
            switch(objectType[i]) {
                case 1:
                    if(!newPage) makeGrid();
                    Button button = new Button(this);
                    button.setOnClickListener(pageListener);
                    makeView(button);

                    linearLayout = new LinearLayout(this);
                    linearLayout.setId(i + objectNum);
                    linearLayout.setOrientation(LinearLayout.VERTICAL);
                    linearLayout.setVisibility(View.GONE);
                    mainLayout.addView(linearLayout);

                    makeGrid();
                    inRadio = false;
                    oldPageId = pageId;
                    pageId = i;
                    newPage = false;
                    break;
                case 2:
                    TextView textView = new TextView(this);
                    textView.setTextSize(20);
                    textView.setGravity(1);
                    makeView(textView);

                    makeGrid();
                    inRadio = false;
                    break;
                case 3:
                    CheckBox checkBox = new CheckBox(this);
                    makeView(checkBox);
                    break;
                case 4:
                    button = new Button(this);
                    text = objectName[i] + ": 0";
                    button.setOnClickListener(clickListener);
                    makeView(button);
                    break;
                case 5:
                    textView = new TextView(this);
                    text = objectName[i] + ": 0";
                    makeView(textView, i + objectNum);

                    SeekBar seekBar = new SeekBar(this);
                    seekBar.setId(i);
                    seekBar.setMax(Integer.parseInt(objectName[i].split(":")[1]));
                    seekBar.setOnSeekBarChangeListener(seekBarChangeListener);
                    linearLayout.addView(seekBar);

                    makeGrid();
                    break;
                case 6:
                    if(!inRadio) {
                        RadioGroup radioGroup = new RadioGroup(this);
                        radioGroup.setOnCheckedChangeListener(checkedChangeListener);
                        gridLayout.addView(radioGroup);
                        inRadio = true;
                    }

                    RadioButton radioButton = new RadioButton(this);
                    makeView(radioButton);
                    break;
                case 7:
                    makeGrid();

                    button = new Button(this);
                    button.setOnClickListener(pageListener);
                    makeView(button, oldPageId);

                    newPage = true;
                    break;
            }
        }
        pageId = objectNum;
    }
    public void makeGrid() {
        gridLayout = new GridLayout(this);
        gridLayout.setColumnCount(3);
        linearLayout.addView(gridLayout);
    }
    public void makeView(TextView textView) {
        textView.setId(i);
        textView.setText(text);
        gridLayout.addView(textView);
    }
    public void makeView(TextView textView, int id) {
        textView.setId(id);
        textView.setText(text);
        gridLayout.addView(textView);
    }
    Button.OnClickListener pageListener = new Button.OnClickListener() {
        @Override
        public void onClick(View v) {
            findViewById(pageId).setVisibility(View.GONE);
            i = v.getId();
            pageId = i + objectNum;
            findViewById(pageId).setVisibility(View.VISIBLE);
            try {
                if(connected) client.SendPacket(objectName[i] + "#" + team,"Page");
            } catch (IOException ie) {}
        }
    };
    Button.OnClickListener clickListener = new Button.OnClickListener() {
        @Override
        public void onClick(View v) {
            Button button = (Button) v;
            i = button.getId();
            objectValue[i]++;
            text = objectName[i] + ": " + objectValue[i];
            button.setText(text);
            try {
                if(connected) client.SendPacket(objectName[i].split(":")[0] + "#" + team,String.valueOf(objectValue[i]));
            } catch (IOException ie) {}
        }
    };
    SeekBar.OnSeekBarChangeListener seekBarChangeListener = new SeekBar.OnSeekBarChangeListener() {
        @Override
        public void onProgressChanged(SeekBar seekBar, int progress, boolean fromUser) {
            i = seekBar.getId();
            TextView textView = (TextView) findViewById(i + objectNum);
            text = objectName[i].split(":")[0] + ":" + String.valueOf(progress);
            objectValue[i] = progress;
            if(fromUser) textView.setText(text);
            try {
                if(connected) client.SendPacket(objectName[i] + "#" + team,String.valueOf(progress));
            } catch (IOException ie) {}
        }
        @Override
        public void onStartTrackingTouch(SeekBar seekBar) {
        }
        @Override
        public void onStopTrackingTouch(SeekBar seekBar) {
        }
    };
    RadioGroup.OnCheckedChangeListener checkedChangeListener = (radioGroup, i1) -> {
        try {
            if(connected) client.SendPacket(objectName[radioGroup.getChildAt(i1).getId()] + "#" + team,"checked");
        } catch (IOException ie) {}
    };
}