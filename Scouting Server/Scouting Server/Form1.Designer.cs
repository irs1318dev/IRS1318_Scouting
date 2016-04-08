namespace Scouting_Server
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.LoadMatchButton = new System.Windows.Forms.Button();
            this.red3Team = new System.Windows.Forms.NumericUpDown();
            this.red1Team = new System.Windows.Forms.NumericUpDown();
            this.red2Team = new System.Windows.Forms.NumericUpDown();
            this.matchNumber = new System.Windows.Forms.NumericUpDown();
            this.blue1Team = new System.Windows.Forms.NumericUpDown();
            this.blue2Team = new System.Windows.Forms.NumericUpDown();
            this.blue3Team = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SetMatchButton = new System.Windows.Forms.Button();
            this.errorMessage = new System.Windows.Forms.Label();
            this.pulse = new System.Windows.Forms.Timer(this.components);
            this.RedDef1 = new System.Windows.Forms.ComboBox();
            this.RedDef2 = new System.Windows.Forms.ComboBox();
            this.RedDef3 = new System.Windows.Forms.ComboBox();
            this.RedDef4 = new System.Windows.Forms.ComboBox();
            this.RedDef5 = new System.Windows.Forms.ComboBox();
            this.BlueDef5 = new System.Windows.Forms.ComboBox();
            this.BlueDef4 = new System.Windows.Forms.ComboBox();
            this.BlueDef3 = new System.Windows.Forms.ComboBox();
            this.BlueDef2 = new System.Windows.Forms.ComboBox();
            this.BlueDef1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SaveMatchButton = new System.Windows.Forms.Button();
            this.scoutControl6 = new Scouting_Server.ScoutControl();
            this.scoutControl5 = new Scouting_Server.ScoutControl();
            this.scoutControl4 = new Scouting_Server.ScoutControl();
            this.scoutControl3 = new Scouting_Server.ScoutControl();
            this.scoutControl2 = new Scouting_Server.ScoutControl();
            this.scoutControl1 = new Scouting_Server.ScoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.red3Team)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.red1Team)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.red2Team)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue1Team)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue2Team)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue3Team)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadMatchButton
            // 
            this.LoadMatchButton.Location = new System.Drawing.Point(181, 12);
            this.LoadMatchButton.Name = "LoadMatchButton";
            this.LoadMatchButton.Size = new System.Drawing.Size(75, 23);
            this.LoadMatchButton.TabIndex = 1;
            this.LoadMatchButton.Text = "Load";
            this.LoadMatchButton.UseVisualStyleBackColor = true;
            this.LoadMatchButton.Click += new System.EventHandler(this.LoadMatchButton_Click);
            // 
            // red3Team
            // 
            this.red3Team.Location = new System.Drawing.Point(267, 63);
            this.red3Team.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.red3Team.Name = "red3Team";
            this.red3Team.Size = new System.Drawing.Size(122, 20);
            this.red3Team.TabIndex = 4;
            // 
            // red1Team
            // 
            this.red1Team.Location = new System.Drawing.Point(15, 63);
            this.red1Team.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.red1Team.Name = "red1Team";
            this.red1Team.Size = new System.Drawing.Size(122, 20);
            this.red1Team.TabIndex = 2;
            // 
            // red2Team
            // 
            this.red2Team.Location = new System.Drawing.Point(141, 63);
            this.red2Team.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.red2Team.Name = "red2Team";
            this.red2Team.Size = new System.Drawing.Size(122, 20);
            this.red2Team.TabIndex = 3;
            // 
            // matchNumber
            // 
            this.matchNumber.Location = new System.Drawing.Point(55, 12);
            this.matchNumber.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.matchNumber.Name = "matchNumber";
            this.matchNumber.Size = new System.Drawing.Size(120, 20);
            this.matchNumber.TabIndex = 0;
            this.matchNumber.ValueChanged += new System.EventHandler(this.matchNumber_ValueChanged);
            // 
            // blue1Team
            // 
            this.blue1Team.Location = new System.Drawing.Point(393, 63);
            this.blue1Team.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.blue1Team.Name = "blue1Team";
            this.blue1Team.Size = new System.Drawing.Size(122, 20);
            this.blue1Team.TabIndex = 5;
            // 
            // blue2Team
            // 
            this.blue2Team.Location = new System.Drawing.Point(519, 63);
            this.blue2Team.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.blue2Team.Name = "blue2Team";
            this.blue2Team.Size = new System.Drawing.Size(122, 20);
            this.blue2Team.TabIndex = 6;
            // 
            // blue3Team
            // 
            this.blue3Team.Location = new System.Drawing.Point(645, 63);
            this.blue3Team.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.blue3Team.Name = "blue3Team";
            this.blue3Team.Size = new System.Drawing.Size(122, 20);
            this.blue3Team.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Match";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Red1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Red2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Red3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(390, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Blue1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(516, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Blue2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(642, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Blue3";
            // 
            // SetMatchButton
            // 
            this.SetMatchButton.Location = new System.Drawing.Point(262, 12);
            this.SetMatchButton.Name = "SetMatchButton";
            this.SetMatchButton.Size = new System.Drawing.Size(75, 23);
            this.SetMatchButton.TabIndex = 8;
            this.SetMatchButton.Text = "Set";
            this.SetMatchButton.UseVisualStyleBackColor = true;
            this.SetMatchButton.Click += new System.EventHandler(this.SetMatchButton_Click);
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(424, 17);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(0, 13);
            this.errorMessage.TabIndex = 22;
            // 
            // pulse
            // 
            this.pulse.Enabled = true;
            this.pulse.Interval = 1000;
            this.pulse.Tick += new System.EventHandler(this.pulse_Tick);
            // 
            // RedDef1
            // 
            this.RedDef1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.RedDef1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.RedDef1.BackColor = System.Drawing.Color.Red;
            this.RedDef1.FormattingEnabled = true;
            this.RedDef1.Items.AddRange(new object[] {
            "Low Bar"});
            this.RedDef1.Location = new System.Drawing.Point(18, 296);
            this.RedDef1.Name = "RedDef1";
            this.RedDef1.Size = new System.Drawing.Size(121, 21);
            this.RedDef1.Sorted = true;
            this.RedDef1.TabIndex = 0;
            this.RedDef1.TabStop = false;
            this.RedDef1.Text = "Low Bar";
            // 
            // RedDef2
            // 
            this.RedDef2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.RedDef2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.RedDef2.BackColor = System.Drawing.Color.Red;
            this.RedDef2.FormattingEnabled = true;
            this.RedDef2.Items.AddRange(new object[] {
            "Cheval de Frise",
            "Portcullis"});
            this.RedDef2.Location = new System.Drawing.Point(145, 296);
            this.RedDef2.Name = "RedDef2";
            this.RedDef2.Size = new System.Drawing.Size(121, 21);
            this.RedDef2.Sorted = true;
            this.RedDef2.TabIndex = 0;
            this.RedDef2.TabStop = false;
            // 
            // RedDef3
            // 
            this.RedDef3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.RedDef3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.RedDef3.BackColor = System.Drawing.Color.Red;
            this.RedDef3.FormattingEnabled = true;
            this.RedDef3.Items.AddRange(new object[] {
            "Moat",
            "Ramparts"});
            this.RedDef3.Location = new System.Drawing.Point(272, 296);
            this.RedDef3.Name = "RedDef3";
            this.RedDef3.Size = new System.Drawing.Size(121, 21);
            this.RedDef3.Sorted = true;
            this.RedDef3.TabIndex = 0;
            this.RedDef3.TabStop = false;
            // 
            // RedDef4
            // 
            this.RedDef4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.RedDef4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.RedDef4.BackColor = System.Drawing.Color.Red;
            this.RedDef4.FormattingEnabled = true;
            this.RedDef4.Items.AddRange(new object[] {
            "Drawbridge",
            "Sally Port"});
            this.RedDef4.Location = new System.Drawing.Point(399, 296);
            this.RedDef4.Name = "RedDef4";
            this.RedDef4.Size = new System.Drawing.Size(121, 21);
            this.RedDef4.Sorted = true;
            this.RedDef4.TabIndex = 0;
            this.RedDef4.TabStop = false;
            // 
            // RedDef5
            // 
            this.RedDef5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.RedDef5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.RedDef5.BackColor = System.Drawing.Color.Red;
            this.RedDef5.FormattingEnabled = true;
            this.RedDef5.Items.AddRange(new object[] {
            "Rock Wall",
            "Rough Terrain"});
            this.RedDef5.Location = new System.Drawing.Point(526, 296);
            this.RedDef5.Name = "RedDef5";
            this.RedDef5.Size = new System.Drawing.Size(121, 21);
            this.RedDef5.Sorted = true;
            this.RedDef5.TabIndex = 0;
            this.RedDef5.TabStop = false;
            // 
            // BlueDef5
            // 
            this.BlueDef5.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.BlueDef5.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.BlueDef5.BackColor = System.Drawing.Color.DodgerBlue;
            this.BlueDef5.FormattingEnabled = true;
            this.BlueDef5.Items.AddRange(new object[] {
            "Rock Wall",
            "Rough Terrain"});
            this.BlueDef5.Location = new System.Drawing.Point(526, 323);
            this.BlueDef5.Name = "BlueDef5";
            this.BlueDef5.Size = new System.Drawing.Size(121, 21);
            this.BlueDef5.Sorted = true;
            this.BlueDef5.TabIndex = 0;
            this.BlueDef5.TabStop = false;
            // 
            // BlueDef4
            // 
            this.BlueDef4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.BlueDef4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.BlueDef4.BackColor = System.Drawing.Color.DodgerBlue;
            this.BlueDef4.FormattingEnabled = true;
            this.BlueDef4.Items.AddRange(new object[] {
            "Drawbridge",
            "Sally Port"});
            this.BlueDef4.Location = new System.Drawing.Point(399, 323);
            this.BlueDef4.Name = "BlueDef4";
            this.BlueDef4.Size = new System.Drawing.Size(121, 21);
            this.BlueDef4.Sorted = true;
            this.BlueDef4.TabIndex = 0;
            this.BlueDef4.TabStop = false;
            // 
            // BlueDef3
            // 
            this.BlueDef3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.BlueDef3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.BlueDef3.BackColor = System.Drawing.Color.DodgerBlue;
            this.BlueDef3.FormattingEnabled = true;
            this.BlueDef3.Items.AddRange(new object[] {
            "Moat",
            "Ramparts"});
            this.BlueDef3.Location = new System.Drawing.Point(272, 323);
            this.BlueDef3.Name = "BlueDef3";
            this.BlueDef3.Size = new System.Drawing.Size(121, 21);
            this.BlueDef3.Sorted = true;
            this.BlueDef3.TabIndex = 0;
            this.BlueDef3.TabStop = false;
            // 
            // BlueDef2
            // 
            this.BlueDef2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.BlueDef2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.BlueDef2.BackColor = System.Drawing.Color.DodgerBlue;
            this.BlueDef2.FormattingEnabled = true;
            this.BlueDef2.Items.AddRange(new object[] {
            "Cheval de Frise",
            "Portcullis"});
            this.BlueDef2.Location = new System.Drawing.Point(145, 323);
            this.BlueDef2.Name = "BlueDef2";
            this.BlueDef2.Size = new System.Drawing.Size(121, 21);
            this.BlueDef2.Sorted = true;
            this.BlueDef2.TabIndex = 0;
            this.BlueDef2.TabStop = false;
            // 
            // BlueDef1
            // 
            this.BlueDef1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.BlueDef1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.BlueDef1.BackColor = System.Drawing.Color.DodgerBlue;
            this.BlueDef1.FormattingEnabled = true;
            this.BlueDef1.Items.AddRange(new object[] {
            "Low Bar"});
            this.BlueDef1.Location = new System.Drawing.Point(18, 323);
            this.BlueDef1.Name = "BlueDef1";
            this.BlueDef1.Size = new System.Drawing.Size(121, 21);
            this.BlueDef1.Sorted = true;
            this.BlueDef1.TabIndex = 0;
            this.BlueDef1.TabStop = false;
            this.BlueDef1.Text = "Low Bar";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 280);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 33;
            this.label8.Text = "Defense B";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(401, 280);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Defense C";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(526, 280);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Defense D";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(145, 280);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 36;
            this.label11.Text = "Defense A";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 280);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Low Bar";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(653, 307);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 38;
            this.button1.Text = "Set Defense";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SaveMatchButton
            // 
            this.SaveMatchButton.Location = new System.Drawing.Point(343, 12);
            this.SaveMatchButton.Name = "SaveMatchButton";
            this.SaveMatchButton.Size = new System.Drawing.Size(75, 23);
            this.SaveMatchButton.TabIndex = 39;
            this.SaveMatchButton.Text = "Save";
            this.SaveMatchButton.UseVisualStyleBackColor = true;
            this.SaveMatchButton.Click += new System.EventHandler(this.SaveMatchButton_Click);
            // 
            // scoutControl6
            // 
            this.scoutControl6.BackColor = System.Drawing.Color.DodgerBlue;
            this.scoutControl6.Location = new System.Drawing.Point(404, 192);
            this.scoutControl6.Name = "scoutControl6";
            this.scoutControl6.Size = new System.Drawing.Size(187, 84);
            this.scoutControl6.TabIndex = 21;
            this.scoutControl6.TabStop = false;
            // 
            // scoutControl5
            // 
            this.scoutControl5.BackColor = System.Drawing.Color.DodgerBlue;
            this.scoutControl5.Location = new System.Drawing.Point(211, 192);
            this.scoutControl5.Name = "scoutControl5";
            this.scoutControl5.Size = new System.Drawing.Size(187, 84);
            this.scoutControl5.TabIndex = 20;
            this.scoutControl5.TabStop = false;
            // 
            // scoutControl4
            // 
            this.scoutControl4.BackColor = System.Drawing.Color.DodgerBlue;
            this.scoutControl4.Location = new System.Drawing.Point(18, 192);
            this.scoutControl4.Name = "scoutControl4";
            this.scoutControl4.Size = new System.Drawing.Size(187, 84);
            this.scoutControl4.TabIndex = 19;
            this.scoutControl4.TabStop = false;
            // 
            // scoutControl3
            // 
            this.scoutControl3.BackColor = System.Drawing.Color.Red;
            this.scoutControl3.Location = new System.Drawing.Point(404, 103);
            this.scoutControl3.Name = "scoutControl3";
            this.scoutControl3.Size = new System.Drawing.Size(187, 83);
            this.scoutControl3.TabIndex = 18;
            this.scoutControl3.TabStop = false;
            // 
            // scoutControl2
            // 
            this.scoutControl2.BackColor = System.Drawing.Color.Red;
            this.scoutControl2.Location = new System.Drawing.Point(211, 103);
            this.scoutControl2.Name = "scoutControl2";
            this.scoutControl2.Size = new System.Drawing.Size(187, 83);
            this.scoutControl2.TabIndex = 17;
            this.scoutControl2.TabStop = false;
            // 
            // scoutControl1
            // 
            this.scoutControl1.BackColor = System.Drawing.Color.Red;
            this.scoutControl1.Location = new System.Drawing.Point(17, 103);
            this.scoutControl1.Name = "scoutControl1";
            this.scoutControl1.Size = new System.Drawing.Size(187, 83);
            this.scoutControl1.TabIndex = 16;
            this.scoutControl1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 352);
            this.Controls.Add(this.SaveMatchButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.BlueDef5);
            this.Controls.Add(this.BlueDef4);
            this.Controls.Add(this.BlueDef3);
            this.Controls.Add(this.BlueDef2);
            this.Controls.Add(this.BlueDef1);
            this.Controls.Add(this.RedDef5);
            this.Controls.Add(this.RedDef4);
            this.Controls.Add(this.RedDef3);
            this.Controls.Add(this.RedDef2);
            this.Controls.Add(this.RedDef1);
            this.Controls.Add(this.errorMessage);
            this.Controls.Add(this.scoutControl6);
            this.Controls.Add(this.scoutControl5);
            this.Controls.Add(this.scoutControl4);
            this.Controls.Add(this.scoutControl3);
            this.Controls.Add(this.scoutControl2);
            this.Controls.Add(this.scoutControl1);
            this.Controls.Add(this.SetMatchButton);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.blue3Team);
            this.Controls.Add(this.blue2Team);
            this.Controls.Add(this.blue1Team);
            this.Controls.Add(this.matchNumber);
            this.Controls.Add(this.red2Team);
            this.Controls.Add(this.red1Team);
            this.Controls.Add(this.red3Team);
            this.Controls.Add(this.LoadMatchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Scouting Server";
            ((System.ComponentModel.ISupportInitialize)(this.red3Team)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.red1Team)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.red2Team)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue1Team)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue2Team)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue3Team)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.Button LoadMatchButton;
    private System.Windows.Forms.NumericUpDown red3Team;
    private System.Windows.Forms.NumericUpDown red1Team;
    private System.Windows.Forms.NumericUpDown red2Team;
    private System.Windows.Forms.NumericUpDown matchNumber;
    private System.Windows.Forms.NumericUpDown blue1Team;
    private System.Windows.Forms.NumericUpDown blue2Team;
    private System.Windows.Forms.NumericUpDown blue3Team;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button SetMatchButton;
    private ScoutControl scoutControl1;
    private ScoutControl scoutControl2;
    private ScoutControl scoutControl3;
    private ScoutControl scoutControl4;
    private ScoutControl scoutControl5;
    private ScoutControl scoutControl6;
    private System.Windows.Forms.Label errorMessage;
    private System.Windows.Forms.Timer pulse;
    private System.Windows.Forms.ComboBox RedDef1;
    private System.Windows.Forms.ComboBox RedDef2;
    private System.Windows.Forms.ComboBox RedDef3;
    private System.Windows.Forms.ComboBox RedDef4;
    private System.Windows.Forms.ComboBox RedDef5;
    private System.Windows.Forms.ComboBox BlueDef5;
    private System.Windows.Forms.ComboBox BlueDef4;
    private System.Windows.Forms.ComboBox BlueDef3;
    private System.Windows.Forms.ComboBox BlueDef2;
    private System.Windows.Forms.ComboBox BlueDef1;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button SaveMatchButton;
    }
}

