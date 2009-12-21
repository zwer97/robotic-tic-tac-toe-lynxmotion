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
	public partial class SectionControl : UserControl
	{
		private int m_XCount, m_YCount;
		private CellControl[] m_CellControls;

		public SectionControl(int xCount, int yCount)
		{
			m_XCount = xCount;
			m_YCount = yCount;
			m_CellControls = new CellControl[xCount * yCount];

			InitializeComponent();
			InsertCellControls();
			SizeAndPositionControls();
		}

		private void InsertCellControls()
		{
			for (int x = 0; x < m_XCount; x++)
			{
				for (int y = 0; y < m_YCount; y++)
				{
					CellControl cellControl = new CellControl(x, y);
					m_CellControls[y * m_XCount + x] = cellControl;
					cellControl.Parent = this;
				}
			}
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
			int cellWidth = Math.Min(this.ClientSize.Height / m_YCount, this.ClientSize.Width / m_XCount);
			Size cellSize = new Size(cellWidth, cellWidth);

			for (int x = 0; x < m_XCount; x++)
			{
				for (int y = 0; y < m_YCount; y++)
				{
					CellControl cellControl = m_CellControls[y * m_XCount + x];
					cellControl.Size = cellSize;
					cellControl.Location = new Point(cellWidth * x, cellWidth * y);
				}
			}
		}

		internal void UpdateGamePieces(Board board)
		{
			for (int i = 0; i < m_CellControls.Length; i++)
			{
				m_CellControls[i].CellState = board[i];
			}
		}
	}
}
