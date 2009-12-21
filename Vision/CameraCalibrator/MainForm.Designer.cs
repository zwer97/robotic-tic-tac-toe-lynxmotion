namespace CameraCalibrator
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
			this.m_TopPanel = new System.Windows.Forms.Panel();
			this.m_ThresholdUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.m_BottomPanel = new System.Windows.Forms.Panel();
			this.m_CaptureButton = new System.Windows.Forms.Button();
			this.m_ExpectedRectanglesTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.m_DetectedRectanglesTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.m_ImageBoxWithHeading_Rectangles = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_BW = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_Original = new Vision.WinForm.ImageBoxWithHeading();
			this.m_MenuStrip.SuspendLayout();
			this.m_TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ThresholdUpDown)).BeginInit();
			this.m_BottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_MenuStrip
			// 
			this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.m_MenuStrip.Name = "m_MenuStrip";
			this.m_MenuStrip.Size = new System.Drawing.Size(759, 28);
			this.m_MenuStrip.TabIndex = 0;
			this.m_MenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.OnMenuSaveClicked);
			// 
			// m_TopPanel
			// 
			this.m_TopPanel.Controls.Add(this.m_ThresholdUpDown);
			this.m_TopPanel.Controls.Add(this.label1);
			this.m_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_TopPanel.Location = new System.Drawing.Point(0, 28);
			this.m_TopPanel.Name = "m_TopPanel";
			this.m_TopPanel.Size = new System.Drawing.Size(759, 40);
			this.m_TopPanel.TabIndex = 1;
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
			// m_BottomPanel
			// 
			this.m_BottomPanel.Controls.Add(this.m_CaptureButton);
			this.m_BottomPanel.Controls.Add(this.m_ExpectedRectanglesTextBox);
			this.m_BottomPanel.Controls.Add(this.label3);
			this.m_BottomPanel.Controls.Add(this.m_DetectedRectanglesTextBox);
			this.m_BottomPanel.Controls.Add(this.label2);
			this.m_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_BottomPanel.Location = new System.Drawing.Point(0, 350);
			this.m_BottomPanel.Name = "m_BottomPanel";
			this.m_BottomPanel.Size = new System.Drawing.Size(759, 100);
			this.m_BottomPanel.TabIndex = 5;
			// 
			// m_CaptureButton
			// 
			this.m_CaptureButton.Location = new System.Drawing.Point(162, 62);
			this.m_CaptureButton.Name = "m_CaptureButton";
			this.m_CaptureButton.Size = new System.Drawing.Size(168, 23);
			this.m_CaptureButton.TabIndex = 9;
			this.m_CaptureButton.Text = "Capture Calibration";
			this.m_CaptureButton.UseVisualStyleBackColor = true;
			this.m_CaptureButton.Click += new System.EventHandler(this.OnCaptureButtonClicked);
			// 
			// m_ExpectedRectanglesTextBox
			// 
			this.m_ExpectedRectanglesTextBox.Location = new System.Drawing.Point(162, 34);
			this.m_ExpectedRectanglesTextBox.Name = "m_ExpectedRectanglesTextBox";
			this.m_ExpectedRectanglesTextBox.ReadOnly = true;
			this.m_ExpectedRectanglesTextBox.Size = new System.Drawing.Size(100, 22);
			this.m_ExpectedRectanglesTextBox.TabIndex = 8;
			this.m_ExpectedRectanglesTextBox.UseWaitCursor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(145, 17);
			this.label3.TabIndex = 7;
			this.label3.Text = "Expected Rectangles:";
			// 
			// m_DetectedRectanglesTextBox
			// 
			this.m_DetectedRectanglesTextBox.Location = new System.Drawing.Point(162, 6);
			this.m_DetectedRectanglesTextBox.Name = "m_DetectedRectanglesTextBox";
			this.m_DetectedRectanglesTextBox.ReadOnly = true;
			this.m_DetectedRectanglesTextBox.Size = new System.Drawing.Size(100, 22);
			this.m_DetectedRectanglesTextBox.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(144, 17);
			this.label2.TabIndex = 5;
			this.label2.Text = "Detected Rectangles:";
			// 
			// m_ImageBoxWithHeading_Rectangles
			// 
			this.m_ImageBoxWithHeading_Rectangles.Heading = "Detected Rectangles";
			this.m_ImageBoxWithHeading_Rectangles.Location = new System.Drawing.Point(494, 74);
			this.m_ImageBoxWithHeading_Rectangles.Name = "m_ImageBoxWithHeading_Rectangles";
			this.m_ImageBoxWithHeading_Rectangles.Size = new System.Drawing.Size(235, 183);
			this.m_ImageBoxWithHeading_Rectangles.TabIndex = 4;
			// 
			// m_ImageBoxWithHeading_BW
			// 
			this.m_ImageBoxWithHeading_BW.Heading = "Black & White";
			this.m_ImageBoxWithHeading_BW.Location = new System.Drawing.Point(253, 74);
			this.m_ImageBoxWithHeading_BW.Name = "m_ImageBoxWithHeading_BW";
			this.m_ImageBoxWithHeading_BW.Size = new System.Drawing.Size(235, 183);
			this.m_ImageBoxWithHeading_BW.TabIndex = 3;
			// 
			// m_ImageBoxWithHeading_Original
			// 
			this.m_ImageBoxWithHeading_Original.Heading = "Original";
			this.m_ImageBoxWithHeading_Original.Location = new System.Drawing.Point(12, 74);
			this.m_ImageBoxWithHeading_Original.Name = "m_ImageBoxWithHeading_Original";
			this.m_ImageBoxWithHeading_Original.Size = new System.Drawing.Size(235, 183);
			this.m_ImageBoxWithHeading_Original.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(759, 450);
			this.Controls.Add(this.m_BottomPanel);
			this.Controls.Add(this.m_ImageBoxWithHeading_Rectangles);
			this.Controls.Add(this.m_ImageBoxWithHeading_BW);
			this.Controls.Add(this.m_ImageBoxWithHeading_Original);
			this.Controls.Add(this.m_TopPanel);
			this.Controls.Add(this.m_MenuStrip);
			this.MainMenuStrip = this.m_MenuStrip;
			this.Name = "MainForm";
			this.Text = "Camera Calibration";
			this.m_MenuStrip.ResumeLayout(false);
			this.m_MenuStrip.PerformLayout();
			this.m_TopPanel.ResumeLayout(false);
			this.m_TopPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ThresholdUpDown)).EndInit();
			this.m_BottomPanel.ResumeLayout(false);
			this.m_BottomPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip m_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.Panel m_TopPanel;
		private System.Windows.Forms.NumericUpDown m_ThresholdUpDown;
		private System.Windows.Forms.Label label1;
		private Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_Original;
		private Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_BW;
		private Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_Rectangles;
		private System.Windows.Forms.Panel m_BottomPanel;
		private System.Windows.Forms.TextBox m_DetectedRectanglesTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button m_CaptureButton;
		private System.Windows.Forms.TextBox m_ExpectedRectanglesTextBox;
		private System.Windows.Forms.Label label3;
	}
}

