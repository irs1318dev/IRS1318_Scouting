package com.example.Data_View;

import android.app.Activity;
import android.os.Bundle;

public class DataView extends Activity {
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.main);
        if(getActionBar() != null) getActionBar().hide();
    }
    public void loadObjects() {

    }
}
