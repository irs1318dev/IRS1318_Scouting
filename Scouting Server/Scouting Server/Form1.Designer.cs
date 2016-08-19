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
            this.SaveMatchButton = new System.Windows.Forms.Button();
            this.MatchCount = new System.Windows.Forms.Label();
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
            // MatchCount
            // 
            this.MatchCount.AutoSize = true;
            this.MatchCount.Location = new System.Drawing.Point(642, 103);
            this.MatchCount.Name = "MatchCount";
            this.MatchCount.Size = new System.Drawing.Size(41, 13);
            this.MatchCount.TabIndex = 40;
            this.MatchCount.Text = "label13";
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
            this.ClientSize = new System.Drawing.Size(778, 292);
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
        private System.Windows.Forms.Button SaveMatchButton;
        private System.Windows.Forms.Label MatchCount;
    }
}

