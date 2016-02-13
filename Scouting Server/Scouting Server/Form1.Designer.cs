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
            this.objectsText = new System.Windows.Forms.Label();
            this.devicesText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // objectsText
            // 
            this.objectsText.AutoSize = true;
            this.objectsText.Location = new System.Drawing.Point(344, 195);
            this.objectsText.Name = "objectsText";
            this.objectsText.Size = new System.Drawing.Size(43, 13);
            this.objectsText.TabIndex = 0;
            this.objectsText.Text = "Objects";
            // 
            // devicesText
            // 
            this.devicesText.AutoSize = true;
            this.devicesText.Location = new System.Drawing.Point(347, 224);
            this.devicesText.Name = "devicesText";
            this.devicesText.Size = new System.Drawing.Size(46, 13);
            this.devicesText.TabIndex = 1;
            this.devicesText.Text = "Devices";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 472);
            this.Controls.Add(this.devicesText);
            this.Controls.Add(this.objectsText);
            this.Name = "Form1";
            this.Text = "Scouter";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        #endregion

        private System.Windows.Forms.Label objectsText;
        private System.Windows.Forms.Label devicesText;
    }
}

