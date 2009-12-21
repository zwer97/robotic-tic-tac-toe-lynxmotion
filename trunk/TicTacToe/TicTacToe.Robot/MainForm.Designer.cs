namespace TicTacToe.Robot
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
			this.m_TopPanel = new System.Windows.Forms.Panel();
			this.m_ResetButton = new System.Windows.Forms.Button();
			this.m_DeltaNonZeroTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.m_ThresholdUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.m_BottomPanel = new System.Windows.Forms.Panel();
			this.m_CurrentStateTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.m_NextMoveTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.m_CenterPanel = new System.Windows.Forms.Panel();
			this.m_BoardControl = new TicTacToe.Robot.BoardControl();
			this.m_ImageBoxWithHeading_Blobs = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_Rectangles = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_BW = new Vision.WinForm.ImageBoxWithHeading();
			this.m_ImageBoxWithHeading_Original = new Vision.WinForm.ImageBoxWithHeading();
			this.m_MenuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadCameraCalibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadRobotCalibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_TopPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ThresholdUpDown)).BeginInit();
			this.m_BottomPanel.SuspendLayout();
			this.m_CenterPanel.SuspendLayout();
			this.m_MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_TopPanel
			// 
			this.m_TopPanel.Controls.Add(this.m_ResetButton);
			this.m_TopPanel.Controls.Add(this.m_DeltaNonZeroTextBox);
			this.m_TopPanel.Controls.Add(this.label4);
			this.m_TopPanel.Controls.Add(this.m_ThresholdUpDown);
			this.m_TopPanel.Controls.Add(this.label1);
			this.m_TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_TopPanel.Location = new System.Drawing.Point(0, 28);
			this.m_TopPanel.Name = "m_TopPanel";
			this.m_TopPanel.Size = new System.Drawing.Size(821, 40);
			this.m_TopPanel.TabIndex = 0;
			// 
			// m_ResetButton
			// 
			this.m_ResetButton.Location = new System.Drawing.Point(374, 6);
			this.m_ResetButton.Name = "m_ResetButton";
			this.m_ResetButton.Size = new System.Drawing.Size(75, 23);
			this.m_ResetButton.TabIndex = 7;
			this.m_ResetButton.Text = "Reset";
			this.m_ResetButton.UseVisualStyleBackColor = true;
			this.m_ResetButton.Click += new System.EventHandler(this.OnResetButtonClicked);
			// 
			// m_DeltaNonZeroTextBox
			// 
			this.m_DeltaNonZeroTextBox.Location = new System.Drawing.Point(282, 6);
			this.m_DeltaNonZeroTextBox.Name = "m_DeltaNonZeroTextBox";
			this.m_DeltaNonZeroTextBox.ReadOnly = true;
			this.m_DeltaNonZeroTextBox.Size = new System.Drawing.Size(70, 22);
			this.m_DeltaNonZeroTextBox.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(167, 9);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(109, 17);
			this.label4.TabIndex = 5;
			this.label4.Text = "Delta Non Zero:";
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
			// m_BottomPanel
			// 
			this.m_BottomPanel.Controls.Add(this.m_CurrentStateTextBox);
			this.m_BottomPanel.Controls.Add(this.label2);
			this.m_BottomPanel.Controls.Add(this.m_NextMoveTextBox);
			this.m_BottomPanel.Controls.Add(this.label3);
			this.m_BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.m_BottomPanel.Location = new System.Drawing.Point(0, 462);
			this.m_BottomPanel.Name = "m_BottomPanel";
			this.m_BottomPanel.Size = new System.Drawing.Size(821, 90);
			this.m_BottomPanel.TabIndex = 1;
			// 
			// m_CurrentStateTextBox
			// 
			this.m_CurrentStateTextBox.Location = new System.Drawing.Point(115, 11);
			this.m_CurrentStateTextBox.Name = "m_CurrentStateTextBox";
			this.m_CurrentStateTextBox.ReadOnly = true;
			this.m_CurrentStateTextBox.Size = new System.Drawing.Size(148, 22);
			this.m_CurrentStateTextBox.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "Current State:";
			// 
			// m_NextMoveTextBox
			// 
			this.m_NextMoveTextBox.Enabled = false;
			this.m_NextMoveTextBox.Location = new System.Drawing.Point(113, 39);
			this.m_NextMoveTextBox.Name = "m_NextMoveTextBox";
			this.m_NextMoveTextBox.ReadOnly = true;
			this.m_NextMoveTextBox.Size = new System.Drawing.Size(150, 22);
			this.m_NextMoveTextBox.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 42);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "Next Move:";
			// 
			// m_CenterPanel
			// 
			this.m_CenterPanel.Controls.Add(this.m_BoardControl);
			this.m_CenterPanel.Controls.Add(this.m_ImageBoxWithHeading_Blobs);
			this.m_CenterPanel.Controls.Add(this.m_ImageBoxWithHeading_Rectangles);
			this.m_CenterPanel.Controls.Add(this.m_ImageBoxWithHeading_BW);
			this.m_CenterPanel.Controls.Add(this.m_ImageBoxWithHeading_Original);
			this.m_CenterPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_CenterPanel.Location = new System.Drawing.Point(0, 68);
			this.m_CenterPanel.Name = "m_CenterPanel";
			this.m_CenterPanel.Size = new System.Drawing.Size(821, 394);
			this.m_CenterPanel.TabIndex = 2;
			// 
			// m_BoardControl
			// 
			this.m_BoardControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_BoardControl.Location = new System.Drawing.Point(623, 19);
			this.m_BoardControl.Name = "m_BoardControl";
			this.m_BoardControl.Size = new System.Drawing.Size(174, 158);
			this.m_BoardControl.TabIndex = 5;
			// 
			// m_ImageBoxWithHeading_Blobs
			// 
			this.m_ImageBoxWithHeading_Blobs.Heading = "Unused Pieces";
			this.m_ImageBoxWithHeading_Blobs.Location = new System.Drawing.Point(214, 155);
			this.m_ImageBoxWithHeading_Blobs.Name = "m_ImageBoxWithHeading_Blobs";
			this.m_ImageBoxWithHeading_Blobs.Size = new System.Drawing.Size(205, 143);
			this.m_ImageBoxWithHeading_Blobs.TabIndex = 4;
			// 
			// m_ImageBoxWithHeading_Rectangles
			// 
			this.m_ImageBoxWithHeading_Rectangles.Heading = "Detected Rectangles";
			this.m_ImageBoxWithHeading_Rectangles.Location = new System.Drawing.Point(3, 155);
			this.m_ImageBoxWithHeading_Rectangles.Name = "m_ImageBoxWithHeading_Rectangles";
			this.m_ImageBoxWithHeading_Rectangles.Size = new System.Drawing.Size(205, 143);
			this.m_ImageBoxWithHeading_Rectangles.TabIndex = 3;
			// 
			// m_ImageBoxWithHeading_BW
			// 
			this.m_ImageBoxWithHeading_BW.Heading = "Black & White";
			this.m_ImageBoxWithHeading_BW.Location = new System.Drawing.Point(214, 6);
			this.m_ImageBoxWithHeading_BW.Name = "m_ImageBoxWithHeading_BW";
			this.m_ImageBoxWithHeading_BW.Size = new System.Drawing.Size(205, 143);
			this.m_ImageBoxWithHeading_BW.TabIndex = 2;
			// 
			// m_ImageBoxWithHeading_Original
			// 
			this.m_ImageBoxWithHeading_Original.Heading = "Orignal";
			this.m_ImageBoxWithHeading_Original.Location = new System.Drawing.Point(3, 6);
			this.m_ImageBoxWithHeading_Original.Name = "m_ImageBoxWithHeading_Original";
			this.m_ImageBoxWithHeading_Original.Size = new System.Drawing.Size(205, 143);
			this.m_ImageBoxWithHeading_Original.TabIndex = 0;
			// 
			// m_MenuStrip
			// 
			this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.m_MenuStrip.Name = "m_MenuStrip";
			this.m_MenuStrip.Size = new System.Drawing.Size(821, 28);
			this.m_MenuStrip.TabIndex = 3;
			this.m_MenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadCameraCalibrationToolStripMenuItem,
            this.loadRobotCalibrationToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// loadCameraCalibrationToolStripMenuItem
			// 
			this.loadCameraCalibrationToolStripMenuItem.Name = "loadCameraCalibrationToolStripMenuItem";
			this.loadCameraCalibrationToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
			this.loadCameraCalibrationToolStripMenuItem.Text = "Load Camera Calibration";
			this.loadCameraCalibrationToolStripMenuItem.Click += new System.EventHandler(this.OnMenuLoadCameraCalibrationClicked);
			// 
			// loadRobotCalibrationToolStripMenuItem
			// 
			this.loadRobotCalibrationToolStripMenuItem.Name = "loadRobotCalibrationToolStripMenuItem";
			this.loadRobotCalibrationToolStripMenuItem.Size = new System.Drawing.Size(243, 24);
			this.loadRobotCalibrationToolStripMenuItem.Text = "Load Robot Calibration";
			this.loadRobotCalibrationToolStripMenuItem.Click += new System.EventHandler(this.OnMenuLoadRobotCalibrationClicked);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(821, 552);
			this.Controls.Add(this.m_CenterPanel);
			this.Controls.Add(this.m_BottomPanel);
			this.Controls.Add(this.m_TopPanel);
			this.Controls.Add(this.m_MenuStrip);
			this.Name = "MainForm";
			this.Text = "Tic Tac Toe Vision System";
			this.m_TopPanel.ResumeLayout(false);
			this.m_TopPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.m_ThresholdUpDown)).EndInit();
			this.m_BottomPanel.ResumeLayout(false);
			this.m_BottomPanel.PerformLayout();
			this.m_CenterPanel.ResumeLayout(false);
			this.m_MenuStrip.ResumeLayout(false);
			this.m_MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel m_TopPanel;
		private System.Windows.Forms.Panel m_BottomPanel;
		private System.Windows.Forms.Panel m_CenterPanel;
		private global::Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_Original;
		private global::Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_Rectangles;
		private global::Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_BW;
		private global::Vision.WinForm.ImageBoxWithHeading m_ImageBoxWithHeading_Blobs;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown m_ThresholdUpDown;
		private System.Windows.Forms.TextBox m_NextMoveTextBox;
		private System.Windows.Forms.Label label3;
		private BoardControl m_BoardControl;
		private System.Windows.Forms.MenuStrip m_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadCameraCalibrationToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadRobotCalibrationToolStripMenuItem;
		private System.Windows.Forms.TextBox m_CurrentStateTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox m_DeltaNonZeroTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button m_ResetButton;
	}
}

