using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Logic
{
	public class GameOverEventArgs : EventArgs
	{
		private GameState m_GameState;

		public GameOverEventArgs(GameState gameState)
		{
			m_GameState = gameState;
		}

		public GameState GameState
		{
			get { return m_GameState; }
		}
	}
}
