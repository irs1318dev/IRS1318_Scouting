package com.example.Data_View;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.view.View;
import android.widget.*;

import java.lang.reflect.Array;
import java.util.Arrays;
import java.util.Comparator;
import java.util.Stack;

public class MainTable extends Activity {
    int i;
    int j;
    int sortBy = 0;
    int filter = 0;
    int teamNum;
    int[][] data;
    Stack<Integer> stack;
    String[] dataTypes;
    Stack<Integer> history = new Stack<>();
    boolean[] checked;

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        if(getActionBar() != null) getActionBar().hide();
        noConnect();
    }

    public void noConnect() {
        stack = new Stack<>();
        dataTypes = new String[4];
        dataTypes[0] = "Team";
        dataTypes[1] = "Offense";
        dataTypes[2] = "Defence";
        dataTypes[3] = "Breaching";
        teamNum = 30;
        checked = new boolean[teamNum];
        data = new int[teamNum][dataTypes.length];
        for(i = 0; i < teamNum; i++) {
            data[i][0] = teamNum - i;
            for(j = 1; j < dataTypes.length; j++) {
                if(i % 2 == 0) data[i][j] = j + i;
                else data[i][j] = j - i;
                if(j % 2 == 0) data[i][j] = -data[i][j];
            }
        }
        i = teamNum;
        while(i > 0) {
            i--;
            stack.push(i);
        }
        loadObjects();
    }

    public void loadObjects() {
        ScrollView scrollView = (ScrollView) findViewById(R.id.MainLayout);
        TableLayout tableLayout = new TableLayout(this);
        tableLayout.setId(teamNum * 2);
        scrollView.addView(tableLayout);

        TableRow topRow = new TableRow(this);
        tableLayout.addView(topRow);

        for(i = 0; i < dataTypes.length; i++) {
            Button button = new Button(this);
            button.setText(dataTypes[i]);
            button.setTextColor(Color.rgb(255,255,77));
            button.setOnClickListener(clickListener);
            button.setTextSize(20);
            topRow.addView(button);
        }
        while(!stack.empty()) {
            i = stack.pop();
            TableRow tableRow = new TableRow(this);
            tableRow.setId(i);
            tableLayout.addView(tableRow);
            CheckBox checkBox = new CheckBox(this);
            checkBox.setId(i + teamNum);
            checkBox.setTextSize(20);
            checkBox.setText(String.valueOf(data[i][0]));
            checkBox.setTextColor(Color.rgb(255,255,77));
            if(checked[i]) checkBox.setChecked(true);
            checkBox.setOnCheckedChangeListener(checkedChangeListener);
            tableRow.addView(checkBox);
            for(int j = 1; j < dataTypes.length; j++) {
                TextView textView = new TextView(this);
                textView.setText(String.valueOf(data[i][j]));
                textView.setTextColor(Color.rgb(255,255,77));
                textView.setTextSize(25);
                tableRow.addView(textView);
            }
        }
        switch(filter) {
            case 0:
                RadioButton radioButton = (RadioButton) findViewById(R.id.All);
                radioButton.callOnClick();
                break;
            case 1:
                radioButton = (RadioButton) findViewById(R.id.Grab);
                radioButton.callOnClick();
                break;
            case 2:
                radioButton = (RadioButton) findViewById(R.id.Drop);
                radioButton.callOnClick();
                break;
        }
    }

    public void filter(View v) {
        for(i = 0; i < teamNum; i++) {
            TableRow tableRow = (TableRow) findViewById(i);
            CheckBox checkBox = (CheckBox) findViewById(i + teamNum);
            switch(v.getId()) {
                case  R.id.All:
                    tableRow.setVisibility(View.VISIBLE);
                    filter = 0;
                    break;
                case R.id.Grab:
                    if(checkBox.isChecked()) tableRow.setVisibility(View.VISIBLE);
                    else tableRow.setVisibility(View.GONE);
                    filter = 1;
                    break;
                case R.id.Drop:
                    if(checkBox.isChecked()) tableRow.setVisibility(View.GONE);
                    else tableRow.setVisibility(View.VISIBLE);
                    filter = 2;
                    break;

            }
        }
    }

    public void undo(View v) {
        if(!history.empty()) {
            if(history.peek() == 0) {
                history.pop();
                while(history.peek() != 0) {
                    CheckBox checkBox = (CheckBox) findViewById(history.pop());
                    checkBox.setChecked(!checkBox.isChecked());
                    history.pop();
                }
                history.pop();
            } else {
                CheckBox checkBox = (CheckBox) findViewById(history.pop());
                checkBox.setChecked(!checkBox.isChecked());
                history.pop();
            }
        }
    }

    public void deselect(View v) {
        history.push(0);
        for(i = teamNum; i < teamNum * 2; i++) {
            CheckBox checkBox = (CheckBox) findViewById(i);
            if(checkBox.isChecked()) checkBox.setChecked(false);
        }
        history.push(0);
    }

    CheckBox.OnCheckedChangeListener  checkedChangeListener = new CheckBox.OnCheckedChangeListener() {
        @Override
        public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
            i = compoundButton.getId();
            history.push(i);
            checked[i - teamNum] = compoundButton.isChecked();
        }
    };

    Button.OnClickListener clickListener = new Button.OnClickListener() {
        @Override
        public void onClick(View v) {
            Button button = (Button) v;
            i = 0;
            while(button.getText() != dataTypes[i]) i++;
            String[] newData = new String[teamNum];
            for(j = 0; j < teamNum; j++) newData[j] = data[j][i] + "," + j;
            Arrays.sort(newData, new Comparator<String>() {
                        @Override
                        public int compare(String s, String t1) {
                            if(i == sortBy) return Integer.valueOf(t1.split(",")[0]) - Integer.valueOf(s.split(",")[0]);
                            else return Integer.valueOf(s.split(",")[0]) - Integer.valueOf(t1.split(",")[0]);
                        }
            });
            stack = new Stack<>();
            for(j = 0; j < teamNum; j++) {
                stack.push(Integer.valueOf(newData[j].split(",")[1]));
            }
            if(i == sortBy) sortBy = -1;
            else sortBy = i;
            ScrollView linearLayout = (ScrollView) findViewById(R.id.MainLayout);
            TableLayout tableLayout = (TableLayout) findViewById(teamNum * 2);
            linearLayout.removeView(tableLayout);
            loadObjects();
        }
    };
}
