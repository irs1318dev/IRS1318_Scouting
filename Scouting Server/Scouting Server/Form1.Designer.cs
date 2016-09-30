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
            this.red3Team = new System.Windows.Forms.ComboBox();
            this.red1Team = new System.Windows.Forms.ComboBox();
            this.red2Team = new System.Windows.Forms.ComboBox();
            this.matchNumber = new System.Windows.Forms.NumericUpDown();
            this.blue1Team = new System.Windows.Forms.ComboBox();
            this.blue2Team = new System.Windows.Forms.ComboBox();
            this.blue3Team = new System.Windows.Forms.ComboBox();
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
            this.SaveMatchButton = new System.Windows.Forms.Button();
            this.MatchCount = new System.Windows.Forms.Label();
            this.scoutControl6 = new Scouting_Server.ScoutControl();
            this.scoutControl5 = new Scouting_Server.ScoutControl();
            this.scoutControl4 = new Scouting_Server.ScoutControl();
            this.scoutControl3 = new Scouting_Server.ScoutControl();
            this.scoutControl2 = new Scouting_Server.ScoutControl();
            this.scoutControl1 = new Scouting_Server.ScoutControl();
            ((System.ComponentModel.ISupportInitialize)(this.matchNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadMatchButton
            // 
            this.LoadMatchButton.Location = new System.Drawing.Point(272, 18);
            this.LoadMatchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LoadMatchButton.Name = "LoadMatchButton";
            this.LoadMatchButton.Size = new System.Drawing.Size(112, 35);
            this.LoadMatchButton.TabIndex = 1;
            this.LoadMatchButton.Text = "Load";
            this.LoadMatchButton.UseVisualStyleBackColor = true;
            this.LoadMatchButton.Click += new System.EventHandler(this.LoadMatchButton_Click);
            // 
            // red3Team
            // 
            this.red3Team.Location = new System.Drawing.Point(400, 97);
            this.red3Team.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.red3Team.Name = "red3Team";
            this.red3Team.Size = new System.Drawing.Size(181, 28);
            this.red3Team.TabIndex = 4;
            this.red3Team.Text = "0";
            // 
            // red1Team
            // 
            this.red1Team.Location = new System.Drawing.Point(22, 97);
            this.red1Team.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.red1Team.Name = "red1Team";
            this.red1Team.Size = new System.Drawing.Size(181, 28);
            this.red1Team.TabIndex = 2;
            this.red1Team.Text = "0";
            // 
            // red2Team
            // 
            this.red2Team.Location = new System.Drawing.Point(212, 97);
            this.red2Team.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.red2Team.Name = "red2Team";
            this.red2Team.Size = new System.Drawing.Size(181, 28);
            this.red2Team.TabIndex = 3;
            this.red2Team.Text = "0";
            // 
            // matchNumber
            // 
            this.matchNumber.Location = new System.Drawing.Point(82, 18);
            this.matchNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.matchNumber.Maximum = new decimal(new int[] {
            -1530494976,
            232830,
            0,
            0});
            this.matchNumber.Name = "matchNumber";
            this.matchNumber.Size = new System.Drawing.Size(180, 26);
            this.matchNumber.TabIndex = 0;
            this.matchNumber.ValueChanged += new System.EventHandler(this.matchNumber_ValueChanged);
            // 
            // blue1Team
            // 
            this.blue1Team.Location = new System.Drawing.Point(590, 97);
            this.blue1Team.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blue1Team.Name = "blue1Team";
            this.blue1Team.Size = new System.Drawing.Size(181, 28);
            this.blue1Team.TabIndex = 5;
            this.blue1Team.Text = "0";
            // 
            // blue2Team
            // 
            this.blue2Team.Location = new System.Drawing.Point(778, 97);
            this.blue2Team.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blue2Team.Name = "blue2Team";
            this.blue2Team.Size = new System.Drawing.Size(181, 28);
            this.blue2Team.TabIndex = 6;
            this.blue2Team.Text = "0";
            // 
            // blue3Team
            // 
            this.blue3Team.Location = new System.Drawing.Point(968, 97);
            this.blue3Team.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.blue3Team.Name = "blue3Team";
            this.blue3Team.Size = new System.Drawing.Size(181, 28);
            this.blue3Team.TabIndex = 7;
            this.blue3Team.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Match";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Red1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Red2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Red3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(585, 68);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Blue1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(774, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Blue2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(963, 68);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Blue3";
            // 
            // SetMatchButton
            // 
            this.SetMatchButton.Location = new System.Drawing.Point(393, 18);
            this.SetMatchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SetMatchButton.Name = "SetMatchButton";
            this.SetMatchButton.Size = new System.Drawing.Size(112, 35);
            this.SetMatchButton.TabIndex = 8;
            this.SetMatchButton.Text = "Set";
            this.SetMatchButton.UseVisualStyleBackColor = true;
            this.SetMatchButton.Click += new System.EventHandler(this.SetMatchButton_Click);
            // 
            // errorMessage
            // 
            this.errorMessage.AutoSize = true;
            this.errorMessage.Location = new System.Drawing.Point(636, 26);
            this.errorMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.errorMessage.Name = "errorMessage";
            this.errorMessage.Size = new System.Drawing.Size(0, 20);
            this.errorMessage.TabIndex = 22;
            // 
            // pulse
            // 
            this.pulse.Enabled = true;
            this.pulse.Interval = 1000;
            this.pulse.Tick += new System.EventHandler(this.pulse_Tick);
            // 
            // SaveMatchButton
            // 
            this.SaveMatchButton.Location = new System.Drawing.Point(514, 18);
            this.SaveMatchButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveMatchButton.Name = "SaveMatchButton";
            this.SaveMatchButton.Size = new System.Drawing.Size(112, 35);
            this.SaveMatchButton.TabIndex = 39;
            this.SaveMatchButton.Text = "Save";
            this.SaveMatchButton.UseVisualStyleBackColor = true;
            this.SaveMatchButton.Click += new System.EventHandler(this.SaveMatchButton_Click);
            // 
            // MatchCount
            // 
            this.MatchCount.AutoSize = true;
            this.MatchCount.Location = new System.Drawing.Point(963, 158);
            this.MatchCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MatchCount.Name = "MatchCount";
            this.MatchCount.Size = new System.Drawing.Size(60, 20);
            this.MatchCount.TabIndex = 40;
            this.MatchCount.Text = "label13";
            // 
            // scoutControl6
            // 
            this.scoutControl6.BackColor = System.Drawing.Color.DodgerBlue;
            this.scoutControl6.Location = new System.Drawing.Point(606, 295);
            this.scoutControl6.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.scoutControl6.Name = "scoutControl6";
            this.scoutControl6.Size = new System.Drawing.Size(280, 129);
            this.scoutControl6.TabIndex = 21;
            this.scoutControl6.TabStop = false;
            // 
            // scoutControl5
            // 
            this.scoutControl5.BackColor = System.Drawing.Color.DodgerBlue;
            this.scoutControl5.Location = new System.Drawing.Point(316, 295);
            this.scoutControl5.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.scoutControl5.Name = "scoutControl5";
            this.scoutControl5.Size = new System.Drawing.Size(280, 129);
            this.scoutControl5.TabIndex = 20;
            this.scoutControl5.TabStop = false;
            // 
            // scoutControl4
            // 
            this.scoutControl4.BackColor = System.Drawing.Color.DodgerBlue;
            this.scoutControl4.Location = new System.Drawing.Point(27, 295);
            this.scoutControl4.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.scoutControl4.Name = "scoutControl4";
            this.scoutControl4.Size = new System.Drawing.Size(280, 129);
            this.scoutControl4.TabIndex = 19;
            this.scoutControl4.TabStop = false;
            // 
            // scoutControl3
            // 
            this.scoutControl3.BackColor = System.Drawing.Color.Red;
            this.scoutControl3.Location = new System.Drawing.Point(606, 158);
            this.scoutControl3.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.scoutControl3.Name = "scoutControl3";
            this.scoutControl3.Size = new System.Drawing.Size(280, 128);
            this.scoutControl3.TabIndex = 18;
            this.scoutControl3.TabStop = false;
            // 
            // scoutControl2
            // 
            this.scoutControl2.BackColor = System.Drawing.Color.Red;
            this.scoutControl2.Location = new System.Drawing.Point(316, 158);
            this.scoutControl2.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.scoutControl2.Name = "scoutControl2";
            this.scoutControl2.Size = new System.Drawing.Size(280, 128);
            this.scoutControl2.TabIndex = 17;
            this.scoutControl2.TabStop = false;
            // 
            // scoutControl1
            // 
            this.scoutControl1.BackColor = System.Drawing.Color.Red;
            this.scoutControl1.Location = new System.Drawing.Point(26, 158);
            this.scoutControl1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.scoutControl1.Name = "scoutControl1";
            this.scoutControl1.Size = new System.Drawing.Size(280, 128);
            this.scoutControl1.TabIndex = 16;
            this.scoutControl1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 449);
            this.Controls.Add(this.MatchCount);
            this.Controls.Add(this.SaveMatchButton);
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
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Scouting Server";
            ((System.ComponentModel.ISupportInitialize)(this.matchNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.Button LoadMatchButton;
    private System.Windows.Forms.ComboBox red3Team;
    private System.Windows.Forms.ComboBox red1Team;
    private System.Windows.Forms.ComboBox red2Team;
    private System.Windows.Forms.NumericUpDown matchNumber;
    private System.Windows.Forms.ComboBox blue1Team;
    private System.Windows.Forms.ComboBox blue2Team;
    private System.Windows.Forms.ComboBox blue3Team;
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
        private System.Windows.Forms.Button SaveMatchButton;
        private System.Windows.Forms.Label MatchCount;
    }
}

