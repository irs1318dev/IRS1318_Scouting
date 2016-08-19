package com.irs1318.Scouter_Client;

import android.content.Context;
import android.graphics.Color;
import android.view.View;
import android.view.ViewGroup;
import android.widget.*;

import java.util.ArrayList;
import java.util.List;

public class ScoutForm {
    int objectNum;
    int i;
    int column = 0;
    int lineLength = 0;
    int[] pageId;
    int[] objectType;
    int[] objectValue;
    String text;
    String[] objectName;
    boolean reverse;
    TableLayout tableLayout;
    LinearLayout sideLayout;
    LinearLayout lineLayout;
    LinearLayout mainLayout;
    List<ButtonPress> dataLog;
    Context context;
    MainInput mainInput;

    public void loadObjects(int page) {
        dataLog = new ArrayList<>();
        objectNum = objectValue.length;

        //Removing old layout
        LinearLayout linearLayout = (LinearLayout) mainInput.findViewById(R.id.mainLayout);
        linearLayout.removeView(mainLayout);

        //Adding essential variables
        mainLayout = new LinearLayout(context);
        mainInput.mainLayout = mainLayout;
        mainLayout.setGravity(1);
        linearLayout.addView(mainLayout);

        sideLayout = new LinearLayout(context);
        tableLayout = new TableLayout(context);

        makeLine();
        int newPage = 0;
        text = objectName[0];

        //Creating actual form
        for (i = 0; i < objectNum; i++) {
            text = objectName[i];
            switch (objectType[i]) {
                case 1:
                    //Page
                    linearLayout = new LinearLayout(context);
                    linearLayout.setId(i);
                    linearLayout.setGravity(1);
                    if (newPage != page) linearLayout.setVisibility(View.GONE);
                    mainLayout.addView(linearLayout);

                    //Adjusting variables
                    pageId[newPage] = i;
                    newPage++;
                    break;
                case 2:
                    //Category
                    //First Divider
                    TextView divider = new TextView(context);
                    divider.setWidth(5);
                    divider.setBackgroundColor(Color.LTGRAY);
                    divider.setHeight(650);
                    linearLayout.addView(divider);

                    Space space = new Space(context);
                    space.setMinimumWidth(5);
                    linearLayout.addView(space);

                    sideLayout = new LinearLayout(context);
                    sideLayout.setGravity(1);
                    sideLayout.setOrientation(LinearLayout.VERTICAL);
                    linearLayout.addView(sideLayout);

                    //Second divider
                    space = new Space(context);
                    space.setMinimumWidth(5);
                    linearLayout.addView(space);

                    divider = new TextView(context);
                    divider.setWidth(5);
                    divider.setBackgroundColor(Color.LTGRAY);
                    divider.setHeight(650);
                    linearLayout.addView(divider);

                    //Labelling
                    TextView textView = new TextView(context);
                    makeView(textView, sideLayout);
                    textView.setTextSize(25);
                    textView.setTextColor(Color.rgb(249, 178, 52));

                    tableLayout = new TableLayout(context);
                    sideLayout.addView(tableLayout);
                    makeLine();

                    break;

                case 3:
                    //Switch
                    LinearLayout switchLayout = new LinearLayout(context);
                    switchLayout.setOrientation(LinearLayout.VERTICAL);
                    switchLayout.setId(i + objectNum);
                    switchLayout.setOnClickListener(clickListener);
                    lineLayout.addView(switchLayout);

                    textView = new TextView(context);
                    makeView(textView, switchLayout);
                    column--;
                    text = "";

                    Switch aSwitch = new Switch(context);
                    aSwitch.setOnClickListener(clickListener);
                    aSwitch.setId(i);
                    if (objectValue[i] == 1) aSwitch.setChecked(true);
                    aSwitch.setGravity(1);
                    makeView(aSwitch, switchLayout);
                    break;
                case 4:
                    //Count
                    Button button = new Button(context);
                    button.setOnClickListener(clickListener);
                    button.setId(i);
                    text = objectName[i] + ": " + objectValue[i];
                    makeView(button, lineLayout);
                    break;
                case 5:
                    //Choice
                    //Choice Group
                    LinearLayout radioGroup = new LinearLayout(context);
                    radioGroup.setOnClickListener(clickListener);
                    radioGroup.setId(i + objectNum);
                    radioGroup.setOrientation(LinearLayout.HORIZONTAL);
                    lineLayout.addView(radioGroup);

                    //Button label
                    textView = new TextView(context);
                    makeView(textView, radioGroup);
                    column--;
                    text = "";

                    //Button
                    RadioButton radioButton = new RadioButton(context);
                    radioButton.setId(i);
                    if (objectValue[i] == 1) radioButton.setChecked(true);
                    radioButton.setOnClickListener(clickListener);
                    makeView(radioButton, radioGroup);
                    break;
                case 6:
                    //Line
                    //New table
                    tableLayout = new TableLayout(context);
                    sideLayout.addView(tableLayout);

                    //New line
                    lineLength = Integer.valueOf(objectName[i]);
                    makeLine();
                    break;
                case 7:
                    //Label
                    textView = new TextView(context);
                    makeView(textView, lineLayout);
                    textView.setTextSize(25);
                    textView.setTextColor(Color.rgb(249, 178, 52));
                    break;
            }
        }
        i = 0;
    }

    //Creating a grid for other objects
    public void makeLine() {
        //New line
        lineLayout = new TableRow(context);
        lineLayout.setGravity(1);
        tableLayout.addView(lineLayout);

        //Resetting other variables
        column = 0;
    }

    //Finalizing the object
    public void makeView(TextView textView, ViewGroup viewGroup) {
        //Formatting and adding object
        textView.setText(text);
        textView.setTextSize(20);
        textView.setGravity(1);
        textView.setHighlightColor(Color.rgb(54, 179, 222));
        textView.setTextColor(Color.LTGRAY);
        viewGroup.addView(textView);

        //Checking for end of row
        if (objectType[i] > 2) column++;
        else column = 0;
        if (column == lineLength) makeLine();
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
                    Switch aSwitch = (Switch) mainInput.findViewById(i);
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
                        RadioButton radioButton = (RadioButton) mainInput.findViewById(j);
                        objectValue[j] = 0;
                        if(i == j && !reverse) {
                            radioButton.setChecked(true);
                            changed = true;
                        } else if(radioButton.isChecked()) {
                            radioButton.setChecked(false);
                            dataLog.add(new ButtonPress("Undo", j));
                        }
                        j--;
                    }
                    objectValue[i] = 1;
                    break;
            }

            if(reverse) text = "Undo";
            else text = "Event";

            //Notifying server of change
            if(changed) dataLog.add(new ButtonPress(text, i));

            Switch aSwitch = (Switch) mainInput.findViewById(R.id.Reverse);
            reverse = aSwitch.isChecked();
        }
    };
}