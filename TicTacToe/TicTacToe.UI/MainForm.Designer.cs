namespace TicTacToe.UI
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
			this.ComputerStarts = new System.Windows.Forms.ToolStripMenuItem();
			this.computerStartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.humanStartsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// m_MenuStrip
			// 
			this.m_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ComputerStarts});
			this.m_MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.m_MenuStrip.Name = "m_MenuStrip";
			this.m_MenuStrip.Size = new System.Drawing.Size(282, 28);
			this.m_MenuStrip.TabIndex = 0;
			this.m_MenuStrip.Text = "m_MenuStrip";
			// 
			// ComputerStarts
			// 
			this.ComputerStarts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.computerStartsToolStripMenuItem,
            this.humanStartsToolStripMenuItem});
			this.ComputerStarts.Name = "ComputerStarts";
			this.ComputerStarts.Size = new System.Drawing.Size(52, 24);
			this.ComputerStarts.Text = "Start";
			// 
			// computerStartsToolStripMenuItem
			// 
			this.computerStartsToolStripMenuItem.Name = "computerStartsToolStripMenuItem";
			this.computerStartsToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
			this.computerStartsToolStripMenuItem.Text = "Computer Starts";
			this.computerStartsToolStripMenuItem.Click += new System.EventHandler(this.OnComputerStartsMenuClicked);
			// 
			// humanStartsToolStripMenuItem
			// 
			this.humanStartsToolStripMenuItem.Name = "humanStartsToolStripMenuItem";
			this.humanStartsToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
			this.humanStartsToolStripMenuItem.Text = "Human Starts";
			this.humanStartsToolStripMenuItem.Click += new System.EventHandler(this.OnHumanStartsMenuClicked);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(282, 253);
			this.Controls.Add(this.m_MenuStrip);
			this.MainMenuStrip = this.m_MenuStrip;
			this.Name = "MainForm";
			this.Text = "Tic Tac Toe";
			this.m_MenuStrip.ResumeLayout(false);
			this.m_MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip m_MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ComputerStarts;
		private System.Windows.Forms.ToolStripMenuItem computerStartsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem humanStartsToolStripMenuItem;
	}
}

