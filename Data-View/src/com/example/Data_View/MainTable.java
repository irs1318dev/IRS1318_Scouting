package com.example.Data_View;

import android.app.Activity;
import android.graphics.Color;
import android.os.Bundle;
import android.view.View;
import android.widget.*;

import com.example.Data_View.Net.*;

import java.util.*;

public class MainTable extends Activity {
    int i;
    int j;
    int sortBy = 0;
    int filter = 0;
    int teamNum;
    int viewMode = 1;
    int defenseStart;
    int dataNum = 0;
    int[][] data;
    String[] dataTypes;
    String[] dataName;
    List<Integer> teams;
    List<List<Integer[]>> dataValue;
    Stack<Integer> stack;
    Stack<Integer> history = new Stack<>();
    TCPClient client;
    boolean[] checked;
    TableLayout tableLayout;


    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        if(getActionBar() != null) getActionBar().hide();
        noConnect();
    }

    public void noConnect() {
        stack = new Stack<>();
        dataTypes = new String[20];
        defenseStart = dataTypes.length - 11;
        dataTypes[0] = "Team";
        dataTypes[1] = "Offense";
        dataTypes[2] = "Defense";
        dataTypes[3] = "Breach";
        dataTypes[4] = "A Score";
        dataTypes[5] = "T Score";
        dataTypes[6] = "Challenge";
        dataTypes[7] = "Scale";
        dataTypes[8] = "Spybot";
        dataTypes[9] = "Portcullis";
        dataTypes[10] = "Cheval de frise";
        dataTypes[11] = "Moat";
        dataTypes[12] = "Ramparts";
        dataTypes[13] = "Drawbridge";
        dataTypes[14] = "Sally Port";
        dataTypes[15] = "Rock Wall";
        dataTypes[16] = "Rough terrain";
        dataTypes[17] = "Low Bar";
        dataTypes[18] = "Fouls";
        dataTypes[19] = "Tech Fouls";

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
        loadDefenceTable();
    }

    public void connect(View V) {
        EditText editText = (EditText) findViewById(R.id.editText);
        client = new TCPClient(11111, editText.getText().toString());
        client.OnConnected.add(new NetworkEvent() {
            @Override
            public void Call(TCPClient sender) {
                try {
                    client.SendPacket("GetData", "Hello?");
                } catch (Exception ie) {
                }
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
                i = 0;
                if(networkPackets[i].Name.equals("Game")) {
                    dataName = new String[networkPackets.length];
                    for(i = 0; i < networkPackets.length; i++) dataName[i] = networkPackets[i].Data.split(",")[0];
                }
                if(networkPackets[i].Name.equals("MatchData")) {
                    for(i = 0; i < networkPackets.length; i++) {
                        int team = Integer.valueOf(networkPackets[i].Data.split("&")[0].split(",")[1]);
                        if(!teams.contains(team)) teams.add(team);
                        j = teams.indexOf(team);
                        String[] currentData = networkPackets[i].Data.split("&")[1].split(",");
                        Integer[] teamData = new Integer[currentData.length];
                        Stack<Integer> id = new Stack<>();
                        for(int k = 0; k < teamData.length; k++) {
                            id.add(Integer.valueOf(currentData[k].split(":")[0]));
                        }
                        for(int k = 0; k < teamData.length; k++) {
                            int l = id.indexOf(k);
                            teamData[k] = Integer.valueOf(currentData[l].split(":")[1]);
                        }
                        dataValue.get(j).add(teamData);
                    }
                    noConnect();
                }
            }
        });
    }

    public void loadTable() {
        HorizontalScrollView scrollView = (HorizontalScrollView) findViewById(R.id.MainLayout);
        scrollView.removeAllViews();

        tableLayout = new TableLayout(this);
        scrollView.addView(tableLayout);

        TableRow topRow = new TableRow(this);
        tableLayout.addView(topRow);

        for(i = 0; i < dataTypes.length; i++) {
            Button button = new Button(this);
            button.setText(dataTypes[i]);
            button.setTextColor(Color.rgb(249,178,52));
            button.setOnClickListener(clickListener);
            button.setTextSize(15);
            topRow.addView(button);
        }
        while(!stack.empty()) {
            i = stack.pop();
            TableRow tableRow = new TableRow(this);
            tableRow.setId(i);
            tableRow.setOnClickListener(onClickListener);
            tableLayout.addView(tableRow);

            CheckBox checkBox = new CheckBox(this);
            checkBox.setId(i + teamNum);
            checkBox.setTextSize(25);
            checkBox.setText(String.valueOf(data[i][0]));
            checkBox.setTextColor(Color.LTGRAY);
            if(checked[i]) checkBox.setChecked(true);
            checkBox.setOnCheckedChangeListener(checkedChangeListener);
            tableRow.addView(checkBox);

            for(int j = 1; j < dataTypes.length; j++) {
                TextView textView = new TextView(this);
                textView.setText(String.valueOf(data[i][j]));
                textView.setTextColor(Color.LTGRAY);
                textView.setTextSize(25);
                textView.setGravity(1);
                if(j == sortBy || j == -sortBy) textView.setBackgroundColor(Color.argb(100,100,100,100));
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
            LinearLayout linearLayout = (LinearLayout) findViewById(i);
            switch(v.getId()) {
                case  R.id.All:
                    if(viewMode == 0) linearLayout.setVisibility(View.VISIBLE);
                    filter = 0;
                    break;
                case R.id.Grab:
                    if(viewMode == 0) {
                        if (checked[i]) linearLayout.setVisibility(View.VISIBLE);
                        else linearLayout.setVisibility(View.GONE);
                    }
                    filter = 1;
                    break;
                case R.id.Drop:
                    if(viewMode == 0) {
                        if (checked[i]) linearLayout.setVisibility(View.GONE);
                        else linearLayout.setVisibility(View.VISIBLE);
                    }
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

    public void toggleView(View v) {
        viewMode++;
        if(viewMode == 2) viewMode = 0;
        switch (viewMode) {
            case 0:
                i = teamNum;
                sortBy = 0;
                stack = new Stack<>();
                while(i > 0) {
                    i--;
                    stack.push(i);
                }
                LinearLayout linearLayout = (LinearLayout) findViewById(R.id.SecondLayout);
                linearLayout.removeView(tableLayout);
                loadTable();
                break;
            case 1:
                HorizontalScrollView scrollView = (HorizontalScrollView) findViewById(R.id.MainLayout);
                scrollView.removeAllViews();
                loadDefenceTable();
                break;
        }
    }

    public void loadDefenceTable() {
        LinearLayout linearLayout = (LinearLayout) findViewById(R.id.SecondLayout);
        tableLayout = new TableLayout(this);
        linearLayout.addView(tableLayout);

        LinearLayout tableRow = new LinearLayout(this);
        tableRow.setBackgroundColor(Color.RED);
        tableLayout.addView(tableRow);

        TextView divider = new TextView(this);
        divider.setBackgroundColor(Color.LTGRAY);
        divider.setWidth(100);
        divider.setHeight(5);
        tableLayout.addView(divider);

        for(i = 0; i < 6; i++) {
            if(i == 3) {
                tableRow = new LinearLayout(this);
                tableRow.setBackgroundColor(Color.BLUE);
                tableLayout.addView(tableRow);
            }

            TableLayout teamLayout = new TableLayout(this);
            teamLayout.setId(i);
            teamLayout.setOnClickListener(teamViewSet);
            tableRow.addView(teamLayout);

            divider = new TextView(this);
            divider.setBackgroundColor(Color.LTGRAY);
            divider.setWidth(5);
            divider.setHeight(450);
            tableRow.addView(divider);

            Space space = new Space(this);
            space.setMinimumWidth(20);
            tableRow.addView(space);
            TableRow teamRow = new TableRow(this);
            int k = 1;
            for(j = 0; j < 8; j++) {
                if(j < 7) {
                    teamRow = new TableRow(this);
                    teamLayout.addView(teamRow);
                }

                TextView textView = new TextView(this);
                textView.setText(dataTypes[j] + ": " + data[i][j]);
                textView.setTextSize(23);
                if(j < 4) textView.setShadowLayer(3,3,3,Color.DKGRAY);
                teamRow.addView(textView);

                space = new Space(this);
                space.setMinimumWidth(15);
                teamRow.addView(space);

                boolean first = true;
                String defense = "";
                int l = k + 1;
                k--;

                while (k < l && j + k < 12) {
                    boolean skipLine = false;
                    switch (j + k) {
                        case 0:
                            defense = "A--P: ";
                            break;
                        case 1:
                            defense = "C: ";
                            break;
                        case 2:
                            defense = "B--M: ";
                            break;
                        case 3:
                            defense = "R: ";
                            break;
                        case 4:
                            defense = "C--D: ";
                            break;
                        case 5:
                            defense = "S: ";
                            break;
                        case 6:
                            defense = "D--W: ";
                            break;
                        case 7:
                            defense = "T: ";
                            break;
                        case 8:
                            defense = "LB: ";
                            break;
                        case 9:
                            skipLine = true;
                            defenseStart--;
                            break;
                        case 10:
                            defense = "F: ";
                            break;
                        case 11:
                            defense = "TF: ";
                            break;
                    }
                    if(!skipLine) {
                        textView = new TextView(this);
                        if (j + k < 8) {
                            if ((first && data[i][j + k + defenseStart] > data[i][j + k + defenseStart + 1]) || (!first && data[i][j + k + defenseStart] > data[i][j + k + defenseStart - 1]))
                                textView.setTextColor(Color.LTGRAY);
                            else textView.setTextColor(Color.GRAY);
                        }
                        textView.setText(defense + data[i][j + k + defenseStart]);
                        textView.setTextSize(23);
                        if (j + k < 9) textView.setBackgroundColor(Color.argb(100, 50, 50, 50));
                        teamRow.addView(textView);

                        space = new Space(this);
                        space.setMinimumWidth(20);
                        teamRow.addView(space);
                    }
                    first = false;
                    k++;
                }
            }
        }
        defenseStart = dataTypes.length - 11;
    }

    public void loadTeam() {
        findViewById(R.id.Selection).setVisibility(View.GONE);
        findViewById(R.id.Back).setVisibility(View.VISIBLE);
        tableLayout.setVisibility(View.GONE);
        HorizontalScrollView scrollView = (HorizontalScrollView) findViewById(R.id.MainLayout);
        scrollView.removeAllViews();

        LinearLayout linearLayout = new LinearLayout(this);
        linearLayout.setOrientation(LinearLayout.VERTICAL);
        scrollView.addView(linearLayout);

        TextView textView = new TextView(this);
        textView.setTextSize(25);
        textView.setTextColor(Color.LTGRAY);
        textView.setText("Team: " + teams.get(i));
        linearLayout.addView(textView);

        GridLayout gridLayout = new GridLayout(this);
        gridLayout.setColumnCount(18);
        linearLayout.addView(gridLayout);

        for(j = 0; j < dataName.length; j++) {
            List<Integer[]> currentTeam = dataValue.get(i);
            int value = currentTeam.get(currentTeam.size())[j];
            value += currentTeam.get(currentTeam.size() - 1)[j];
            value += currentTeam.get(currentTeam.size() - 2)[j];

            textView = new TextView(this);
            textView.setTextSize(20);
            textView.setTextColor(Color.GRAY);
            textView.setText(dataName[j] + ": " + value);
            gridLayout.addView(textView);

            Space space = new Space(this);
            space.setMinimumWidth(10);
            gridLayout.addView(space);
        }
    }

    public void back(View v) {
        HorizontalScrollView scrollView = (HorizontalScrollView) findViewById(R.id.MainLayout);
        scrollView.removeAllViews();
        findViewById(R.id.Selection).setVisibility(View.VISIBLE);
        tableLayout.setVisibility(View.VISIBLE);
        v.setVisibility(View.GONE);
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
            if(i == sortBy) sortBy = -i;
            else sortBy = i;
            loadTable();
        }
    };
    LinearLayout.OnClickListener onClickListener = new LinearLayout.OnClickListener() {
        @Override
        public void onClick(View v) {
            CheckBox checkBox = (CheckBox) findViewById(v.getId() + teamNum);
            checkBox.toggle();
        }
    };
    LinearLayout.OnClickListener teamViewSet = new LinearLayout.OnClickListener() {
        @Override
        public void onClick(View v) {
            i = v.getId();
            loadTeam();
        }
    };
}
