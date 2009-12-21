using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TicTacToe.Robot.Model;
using TicTacToe.Logic;

namespace TicTacToe.Robot
{
	public partial class BoardControl : UserControl
	{
		//private SectionControl m_LeftParkControl;
		private SectionControl m_GameControl;
		//private SectionControl m_RightParkControl;

		public BoardControl()
		{
			InsertSectionControls();
			InitializeComponent();
			SizeAndPositionControls();
		}

		private void InsertSectionControls()
		{
			//m_LeftParkControl = new SectionControl(2, 3);
			//m_LeftParkControl.Parent = this;

			m_GameControl = new SectionControl(3, 3);
			m_GameControl.Parent = this;

			//m_RightParkControl = new SectionControl(2, 3);
			//m_RightParkControl.Parent = this;
		}

		protected override void OnResize(EventArgs ea)
		{
			//resize and reposition child controls
			SizeAndPositionControls();
			this.Invalidate();
		}

		private void SizeAndPositionControls()
		{
			//Ensure that the client area is always square
			int cellWidth = Math.Min(this.ClientSize.Height / 3, this.ClientSize.Width / 9);

			//m_LeftParkControl.Location = new Point(0, 0);
			m_GameControl.Location = new Point(cellWidth, 0);
			//m_RightParkControl.Location = new Point(7 * cellWidth, 0);
		}

		internal void UpdateGamePieces(Board board)
		{
			//m_LeftParkControl.UpdateGamePieces(boardVision.LeftParkSection);
			m_GameControl.UpdateGamePieces(board);
			//m_RightParkControl.UpdateGamePieces(boardVision.RightParkSection);
		}
	}
}
