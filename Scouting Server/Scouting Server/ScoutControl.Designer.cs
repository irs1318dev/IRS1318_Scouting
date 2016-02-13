namespace Scouting_Server
{
  partial class ScoutControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.matchNum = new System.Windows.Forms.Label();
      this.teamNum = new System.Windows.Forms.Label();
      this.status = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // matchNum
      // 
      this.matchNum.AutoSize = true;
      this.matchNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.matchNum.Location = new System.Drawing.Point(3, 0);
      this.matchNum.Name = "matchNum";
      this.matchNum.Size = new System.Drawing.Size(66, 20);
      this.matchNum.TabIndex = 0;
      this.matchNum.Text = "Match#:";
      // 
      // teamNum
      // 
      this.teamNum.AutoSize = true;
      this.teamNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.teamNum.Location = new System.Drawing.Point(3, 20);
      this.teamNum.Name = "teamNum";
      this.teamNum.Size = new System.Drawing.Size(62, 20);
      this.teamNum.TabIndex = 1;
      this.teamNum.Text = "Team#:";
      // 
      // status
      // 
      this.status.AutoSize = true;
      this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.status.Location = new System.Drawing.Point(3, 41);
      this.status.Name = "status";
      this.status.Size = new System.Drawing.Size(60, 20);
      this.status.TabIndex = 2;
      this.status.Text = "Status:";
      // 
      // ScoutControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.status);
      this.Controls.Add(this.teamNum);
      this.Controls.Add(this.matchNum);
      this.Name = "ScoutControl";
      this.Size = new System.Drawing.Size(187, 65);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label matchNum;
    private System.Windows.Forms.Label teamNum;
    private System.Windows.Forms.Label status;
  }
}
