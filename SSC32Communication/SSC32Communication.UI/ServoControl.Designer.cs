namespace SSC32Communication.UI
{
	partial class ServoControl
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
			this.m_TicksTrackBar = new System.Windows.Forms.TrackBar();
			this.m_NameLabel = new System.Windows.Forms.Label();
			this.m_EnabledCheckBox = new System.Windows.Forms.CheckBox();
			this.m_PositionUpDown = new System.Windows.Forms.NumericUpDown();
			this.m_MinUpDown = new System.Windows.Forms.NumericUpDown();
			this.m_MaxUpDown = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.m_TicksTrackBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_PositionUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_MinUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.m_MaxUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// m_TicksTrackBar
			// 
			this.m_TicksTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.m_TicksTrackBar.Location = new System.Drawing.Point(3, 88);
			this.m_TicksTrackBar.Maximum = 2500;
			this.m_TicksTrackBar.Minimum = 500;
			this.m_TicksTrackBar.Name = "m_TicksTrackBar";
			this.m_TicksTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.m_TicksTrackBar.Size = new System.Drawing.Size(56, 445);
			this.m_TicksTrackBar.TabIndex = 0;
			this.m_TicksTrackBar.TickFrequency = 50;
			this.m_TicksTrackBar.Value = 1500;
			this.m_TicksTrackBar.ValueChanged += new System.EventHandler(this.OnTrackbarValueChanged);
			// 
			// m_NameLabel
			// 
			this.m_NameLabel.AutoSize = true;
			this.m_NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.m_NameLabel.Location = new System.Drawing.Point(3, 0);
			this.m_NameLabel.Name = "m_NameLabel";
			this.m_NameLabel.Size = new System.Drawing.Size(49, 17);
			this.m_NameLabel.TabIndex = 1;
			this.m_NameLabel.Text = "Name";
			// 
			// m_EnabledCheckBox
			// 
			this.m_EnabledCheckBox.AutoSize = true;
			this.m_EnabledCheckBox.Location = new System.Drawing.Point(6, 33);
			this.m_EnabledCheckBox.Name = "m_EnabledCheckBox";
			this.m_EnabledCheckBox.Size = new System.Drawing.Size(82, 21);
			this.m_EnabledCheckBox.TabIndex = 2;
			this.m_EnabledCheckBox.Text = "Enabled";
			this.m_EnabledCheckBox.UseVisualStyleBackColor = true;
			this.m_EnabledCheckBox.CheckedChanged += new System.EventHandler(this.OnEnabledCheckedChanged);
			// 
			// m_PositionUpDown
			// 
			this.m_PositionUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_PositionUpDown.Location = new System.Drawing.Point(6, 566);
			this.m_PositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
			this.m_PositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.m_PositionUpDown.Name = "m_PositionUpDown";
			this.m_PositionUpDown.Size = new System.Drawing.Size(82, 22);
			this.m_PositionUpDown.TabIndex = 4;
			this.m_PositionUpDown.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			this.m_PositionUpDown.ValueChanged += new System.EventHandler(this.OnNumericUpDownValueChanged);
			// 
			// m_MinUpDown
			// 
			this.m_MinUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_MinUpDown.Location = new System.Drawing.Point(6, 538);
			this.m_MinUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
			this.m_MinUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.m_MinUpDown.Name = "m_MinUpDown";
			this.m_MinUpDown.Size = new System.Drawing.Size(82, 22);
			this.m_MinUpDown.TabIndex = 5;
			this.m_MinUpDown.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			this.m_MinUpDown.ValueChanged += new System.EventHandler(this.OnMinUpDownChanged);
			// 
			// m_MaxUpDown
			// 
			this.m_MaxUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.m_MaxUpDown.Location = new System.Drawing.Point(6, 60);
			this.m_MaxUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
			this.m_MaxUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.m_MaxUpDown.Name = "m_MaxUpDown";
			this.m_MaxUpDown.Size = new System.Drawing.Size(82, 22);
			this.m_MaxUpDown.TabIndex = 6;
			this.m_MaxUpDown.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			this.m_MaxUpDown.ValueChanged += new System.EventHandler(this.OnMaxUpDownChanged);
			// 
			// ServoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.m_MaxUpDown);
			this.Controls.Add(this.m_MinUpDown);
			this.Controls.Add(this.m_PositionUpDown);
			this.Controls.Add(this.m_EnabledCheckBox);
			this.Controls.Add(this.m_NameLabel);
			this.Controls.Add(this.m_TicksTrackBar);
			this.Name = "ServoControl";
			this.Size = new System.Drawing.Size(93, 597);
			((System.ComponentModel.ISupportInitialize)(this.m_TicksTrackBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_PositionUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_MinUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.m_MaxUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TrackBar m_TicksTrackBar;
		private System.Windows.Forms.Label m_NameLabel;
		private System.Windows.Forms.CheckBox m_EnabledCheckBox;
		private System.Windows.Forms.NumericUpDown m_PositionUpDown;
		private System.Windows.Forms.NumericUpDown m_MinUpDown;
		private System.Windows.Forms.NumericUpDown m_MaxUpDown;
	}
}
