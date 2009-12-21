namespace Collector
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
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadRobotCalibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_LeftPanel = new System.Windows.Forms.Panel();
			this.m_ImageBoxWithHeading_DetectedBlobs = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_BW = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_Original = new Vision.WinForm.ImageBoxWithHeading();
			this.m_RightPanel = new System.Windows.Forms.Panel();
			this.m_HomeButton = new System.Windows.Forms.Button();
			this.m_CollectButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.m_DetectedBlobsTextBox = new System.Windows.Forms.TextBox();
			this.m_ThresholdUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.m_MenuStrip.SuspendLayout();
			this.m_LeftPanel.SuspendLayout();
			this.m_RightPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ThresholdUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// m_MenuStrip
			// 
			this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.m_MenuStrip.Name = "m_MenuStrip";
			this.m_MenuStrip.Size = new System.Drawing.Size(554, 28);
			this.m_MenuStrip.TabIndex = 0;
			this.m_MenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadRobotCalibrationToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
			this.saveToolStripMenuItem.Text = "Load Camera Calibration";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnMenuLoadCameraCalibrationClicked);
			// 
			// loadRobotCalibrationToolStripMenuItem
			// 
			this.loadRobotCalibrationToolStripMenuItem.Name = "loadRobotCalibrationToolStripMenuItem";
			this.loadRobotCalibrationToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
			this.loadRobotCalibrationToolStripMenuItem.Text = "Load Robot Calibration";
			this.loadRobotCalibrationToolStripMenuItem.Click += new System.EventHandler(this.OnMenuLoadRobotCalibrationClicked);
			// 
			// m_LeftPanel
			// 
			this.m_LeftPanel.Controls.Add(this.m_ImageBoxWithHeading_DetectedBlobs);
			this.m_LeftPanel.Controls.Add(this.m_ImageBoxWithHeading_BW);
			this.m_LeftPanel.Controls.Add(this.m_ImageBoxWithHeading_Original);
			this.m_LeftPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.m_LeftPanel.Location = new System.Drawing.Point(0, 28);
			this.m_LeftPanel.Name = "m_LeftPanel";
			this.m_LeftPanel.Size = new System.Drawing.Size(262, 570);
			this.m_LeftPanel.TabIndex = 5;
			// 
			// m_ImageBoxWithHeading_DetectedBlobs
			// 
			this.m_ImageBoxWithHeading_DetectedBlobs.Heading = "Detected Pieces";
			this.m_ImageBoxWithHeading_DetectedBlobs.Location = new System.Drawing.Point(12, 381);
			this.m_ImageBoxWithHeading_DetectedBlobs.Name = "m_ImageBoxWithHeading_DetectedBlobs";
			this.m_ImageBoxWithHeading_DetectedBlobs.Size = new System.Drawing.Size(235, 183);
			this.m_ImageBoxWithHeading_DetectedBlobs.TabIndex = 5;
			// 
			// m_ImageBoxWithHeading_BW
			// 
			this.m_ImageBoxWithHeading_BW.Heading = "Black & White";
			this.m_ImageBoxWithHeading_BW.Location = new System.Drawing.Point(12, 192);
			this.m_ImageBoxWithHeading_BW.Name = "m_ImageBoxWithHeading_BW";
			this.m_ImageBoxWithHeading_BW.Size = new System.Drawing.Size(235, 183);
			this.m_ImageBoxWithHeading_BW.TabIndex = 4;
			// 
			// m_ImageBoxWithHeading_Original
			// 
			this.m_ImageBoxWithHeading_Original.Heading = "Original";
			this.m_ImageBoxWithHeading_Original.Location = new System.Drawing.Point(12, 3);
			this.m_ImageBoxWithHeading_Original.Name = "m_ImageBoxWithHeading_Original";
			this.m_ImageBoxWithHeading_Original.Size = new System.Drawing.Size(235, 183);
			this.m_ImageBoxWithHeading_Original.TabIndex = 3;
			// 
			// m_RightPanel
			// 
			this.m_RightPanel.Controls.Add(this.m_HomeButton);
			this.m_RightPanel.Controls.Add(this.m_CollectButton);
			this.m_RightPanel.Controls.Add(this.label2);
			this.m_RightPanel.Controls.Add(this.m_DetectedBlobsTextBox);
			this.m_RightPanel.Controls.Add(this.m_ThresholdUpDown);
			this.m_RightPanel.Controls.Add(this.label1);
			this.m_RightPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_RightPanel.Location = new System.Drawing.Point(262, 28);
			this.m_RightPanel.Name = "m_RightPanel";
			this.m_RightPanel.Size = new System.Drawing.Size(292, 570);
			this.m_RightPanel.TabIndex = 6;
			// 
			// m_HomeButton
			// 
			this.m_HomeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_HomeButton.Location = new System.Drawing.Point(124, 541);
			this.m_HomeButton.Name = "m_HomeButton";
			this.m_HomeButton.Size = new System.Drawing.Size(75, 23);
			this.m_HomeButton.TabIndex = 6;
			this.m_HomeButton.Text = "Home";
			this.m_HomeButton.UseVisualStyleBackColor = true;
			this.m_HomeButton.Click += new System.EventHandler(this.OnHomeButtonClick);
			// 
			// m_CollectButton
			// 
			this.m_CollectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.m_CollectButton.Location = new System.Drawing.Point(205, 541);
			this.m_CollectButton.Name = "m_CollectButton";
			this.m_CollectButton.Size = new System.Drawing.Size(75, 23);
			this.m_CollectButton.TabIndex = 5;
			this.m_CollectButton.Text = "Collect";
			this.m_CollectButton.UseVisualStyleBackColor = true;
			this.m_CollectButton.Click += new System.EventHandler(this.OnCollectButtonClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "Detected Blobs:";
			// 
			// m_DetectedBlobsTextBox
			// 
			this.m_DetectedBlobsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_DetectedBlobsTextBox.Location = new System.Drawing.Point(15, 76);
			this.m_DetectedBlobsTextBox.Multiline = true;
			this.m_DetectedBlobsTextBox.Name = "m_DetectedBlobsTextBox";
			this.m_DetectedBlobsTextBox.ReadOnly = true;
			this.m_DetectedBlobsTextBox.Size = new System.Drawing.Size(265, 459);
			this.m_DetectedBlobsTextBox.TabIndex = 3;
			// 
			// m_ThresholdUpDown
			// 
			this.m_ThresholdUpDown.Location = new System.Drawing.Point(94, 7);
			this.m_ThresholdUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
			this.m_ThresholdUpDown.Name = "m_ThresholdUpDown";
			this.m_ThresholdUpDown.Size = new System.Drawing.Size(55, 22);
			this.m_ThresholdUpDown.TabIndex = 2;
			this.m_ThresholdUpDown.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
			this.m_ThresholdUpDown.ValueChanged += new System.EventHandler(this.OnThresholdValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Threshold:";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(554, 598);
			this.Controls.Add(this.m_RightPanel);
			this.Controls.Add(this.m_LeftPanel);
			this.Controls.Add(this.m_MenuStrip);
			this.MainMenuStrip = this.m_MenuStrip;
			this.Name = "MainForm";
			this.Text = "Camera Calibration";
			this.m_MenuStrip.ResumeLayout(false);
			this.m_MenuStrip.PerformLayout();
			this.m_LeftPanel.ResumeLayout(false);
			this.m_RightPanel.ResumeLayout(false);
			this.m_RightPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ThresholdUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip m_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadRobotCalibrationToolStripMenuItem;
		private System.Windows.Forms.Panel m_LeftPanel;
		private Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_DetectedBlobs;
		private Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_BW;
		private Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_Original;
		private System.Windows.Forms.Panel m_RightPanel;
		private System.Windows.Forms.NumericUpDown m_ThresholdUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_DetectedBlobsTextBox;
		private System.Windows.Forms.Button m_HomeButton;
		private System.Windows.Forms.Button m_CollectButton;
	}
}

