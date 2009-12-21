/*
Copyright (c) 2009 Dr. Rainer Hessmer
(Inspired in parts by GrandInquisitor's code, available from
http://www.codeproject.com/KB/game/TicTacToe.aspx)

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using TicTacToe.Logic;

namespace TicTacToe.UI
{
	public partial class MainForm : Form
	{
		private CellControl[,] m_CellControls = new CellControl[3, 3];
		private BoardManager m_BoardManager;

		public MainForm()
		{
			InsertCellControls();
			InitializeComponent();

			//Sizing and positioning business
			this.ClientSize = new Size(500, 500);
			SizeAndPositionControls();

			//avoid exceptions due to controls with zero size
			this.MinimumSize = new Size(200, 200);

			InitializeBoardManager(false);
		}

		private void InsertCellControls()
		{
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					CellControl cellControl = new CellControl(x, y);
					m_CellControls[x, y] = cellControl;
					cellControl.Click += new EventHandler(OnCellControlClicked);
					cellControl.Parent = this;
				}
			}
		}

		private void InitializeBoardManager(bool computerStarts)
		{
			if (m_BoardManager != null)
			{
				m_BoardManager.BoardChanged -= new EventHandler<BoardChangedEventArgs>(OnBoardChanged);
				m_BoardManager.GameOver -= new EventHandler<GameOverEventArgs>(OnGameOver);
			}
			m_BoardManager = new BoardManager(computerStarts);
			m_BoardManager.BoardChanged += new EventHandler<BoardChangedEventArgs>(OnBoardChanged);
			m_BoardManager.GameOver += new EventHandler<GameOverEventArgs>(OnGameOver);

			UpdateCellControls();
		}

		private void OnBoardChanged(object sender, BoardChangedEventArgs e)
		{
			UpdateCellControls();
		}

		private void UpdateCellControls()
		{
			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					CellControl cellControl = m_CellControls[x, y];
					cellControl.CellState = m_BoardManager.Board[x, y];
				}
			}
			//Debug.WriteLine("MainForm.Invalidate");
			this.Invalidate(true);
		}

		private void OnGameOver(object sender, GameOverEventArgs e)
		{
			string messageText = null;

			switch (e.GameState)
			{
				case GameState.InProgress:
					break;
				case GameState.Player1Won:
					messageText = "Player 1 won.";
					break;
				case GameState.Player2Won:
					messageText = "Player 2 won.";
					break;
				case GameState.Draw:
					messageText = "Draw.";
					break;
				default:
					break;
			}
			MessageBox.Show(messageText, "Game over");
			InitializeBoardManager(false);
			this.Invalidate(true);
		}

		private void OnCellControlClicked(object sender, EventArgs e)
		{
			CellControl cellControl = (CellControl)sender;
			m_BoardManager.MakeMove(cellControl.X, cellControl.Y);

			m_BoardManager.MakeMove(m_BoardManager.GetBestMove());
		}

		protected override void OnResize(EventArgs ea)
		{
			//Ensure that the client area is always square
			int size = Math.Min(this.ClientSize.Height, this.ClientSize.Width);
			this.ClientSize = new Size(size, size);

			//resize and reposition child controls
			SizeAndPositionControls();
			this.Invalidate();
		}

		private void SizeAndPositionControls()
		{
			int cellWidth = this.ClientSize.Width / 3;
			Size cellSize = new Size(cellWidth, cellWidth);

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					CellControl cellControl = m_CellControls[x, y];
					cellControl.Size = cellSize;
					cellControl.Location = new Point(cellWidth * x, cellWidth * y);
				}
			}
		}

		private void OnComputerStartsMenuClicked(object sender, EventArgs e)
		{
			InitializeBoardManager(true);
		}

		private void OnHumanStartsMenuClicked(object sender, EventArgs e)
		{
			InitializeBoardManager(false);
		}
	}
}