using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Logic
{
	public class BoardChangedEventArgs : EventArgs
	{
		private Board m_Board;

		public BoardChangedEventArgs(Board board)
		{
			m_Board = board;
		}

		public Board Board
		{
			get { return m_Board; }
		}
	}
}

