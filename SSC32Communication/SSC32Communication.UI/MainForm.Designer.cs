namespace SSC32Communication.UI
{
	partial class MainForm
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
			this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadCalibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_ServoControl5 = new SSC32Communication.UI.ServoControl();
			this.m_ServoControl4 = new SSC32Communication.UI.ServoControl();
			this.m_ServoControl3 = new SSC32Communication.UI.ServoControl();
			this.m_ServoControl2 = new SSC32Communication.UI.ServoControl();
			this.m_ServoControl1 = new SSC32Communication.UI.ServoControl();
			this.m_ServoControl0 = new SSC32Communication.UI.ServoControl();
			this.m_MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_MenuStrip
			// 
			this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.m_MenuStrip.Name = "m_MenuStrip";
			this.m_MenuStrip.Size = new System.Drawing.Size(614, 28);
			this.m_MenuStrip.TabIndex = 6;
			this.m_MenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCalibrationToolStripMenuItem,
            this.saveToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadCalibrationToolStripMenuItem
			// 
			this.loadCalibrationToolStripMenuItem.Name = "loadCalibrationToolStripMenuItem";
			this.loadCalibrationToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
			this.loadCalibrationToolStripMenuItem.Text = "Load Calibration";
			this.loadCalibrationToolStripMenuItem.Click += new System.EventHandler(this.OnLoadCalibration);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(188, 24);
			this.saveToolStripMenuItem.Text = "Save Calibration";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnSaveCalibration);
			// 
			// m_ServoControl5
			// 
			this.m_ServoControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_ServoControl5.Location = new System.Drawing.Point(508, 31);
			this.m_ServoControl5.Name = "m_ServoControl5";
			this.m_ServoControl5.Size = new System.Drawing.Size(93, 593);
			this.m_ServoControl5.TabIndex = 5;
			// 
			// m_ServoControl4
			// 
			this.m_ServoControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_ServoControl4.Location = new System.Drawing.Point(409, 31);
			this.m_ServoControl4.Name = "m_ServoControl4";
			this.m_ServoControl4.Size = new System.Drawing.Size(93, 593);
			this.m_ServoControl4.TabIndex = 4;
			// 
			// m_ServoControl3
			// 
			this.m_ServoControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_ServoControl3.Location = new System.Drawing.Point(310, 31);
			this.m_ServoControl3.Name = "m_ServoControl3";
			this.m_ServoControl3.Size = new System.Drawing.Size(93, 593);
			this.m_ServoControl3.TabIndex = 3;
			// 
			// m_ServoControl2
			// 
			this.m_ServoControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_ServoControl2.Location = new System.Drawing.Point(210, 31);
			this.m_ServoControl2.Name = "m_ServoControl2";
			this.m_ServoControl2.Size = new System.Drawing.Size(93, 593);
			this.m_ServoControl2.TabIndex = 2;
			// 
			// m_ServoControl1
			// 
			this.m_ServoControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_ServoControl1.Location = new System.Drawing.Point(111, 31);
			this.m_ServoControl1.Name = "m_ServoControl1";
			this.m_ServoControl1.Size = new System.Drawing.Size(93, 593);
			this.m_ServoControl1.TabIndex = 1;
			// 
			// m_ServoControl0
			// 
			this.m_ServoControl0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_ServoControl0.Location = new System.Drawing.Point(12, 31);
			this.m_ServoControl0.Name = "m_ServoControl0";
			this.m_ServoControl0.Size = new System.Drawing.Size(93, 593);
			this.m_ServoControl0.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(614, 636);
			this.Controls.Add(this.m_ServoControl5);
			this.Controls.Add(this.m_ServoControl4);
			this.Controls.Add(this.m_ServoControl3);
			this.Controls.Add(this.m_ServoControl2);
			this.Controls.Add(this.m_ServoControl1);
			this.Controls.Add(this.m_ServoControl0);
			this.Controls.Add(this.m_MenuStrip);
			this.MainMenuStrip = this.m_MenuStrip;
			this.Name = "MainForm";
			this.Text = "Servo Control";
			this.m_MenuStrip.ResumeLayout(false);
			this.m_MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ServoControl m_ServoControl0;
		private ServoControl m_ServoControl1;
		private ServoControl m_ServoControl2;
		private ServoControl m_ServoControl3;
		private ServoControl m_ServoControl4;
		private ServoControl m_ServoControl5;
		private System.Windows.Forms.MenuStrip m_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadCalibrationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
	}
}

