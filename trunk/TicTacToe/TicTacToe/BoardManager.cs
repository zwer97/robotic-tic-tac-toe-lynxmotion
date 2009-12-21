/*
Copyright (c) 2009 Dr. Rainer Hessmer
(Inspired in parts by Jonathan Haywood's code, available from
http://www.thehomeofjon.net/programming/2007/11/tic-tac-toe-using-minimax-and-asp-net/)

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
using System.Linq;
using System.Text;

namespace TicTacToe.Logic
{
	public class BoardManager
	{
		internal static Player GetNextPlayer(Player currentPlayer)
		{
			if (currentPlayer == Player.Player1)
			{
				return Player.Player2;
			}
			else
			{
				return Player.Player1;
			}
		}

		private Board m_Board = new Board();
		private Player m_ComputerPlayer;
		private MiniMaxAlgorithm m_MiniMaxAlgorithm = new MiniMaxAlgorithm();
		private bool m_GameOver = false;
		private Player m_CurrentPlayer;

		public BoardManager(bool computerStarts)
		{
			m_CurrentPlayer = Player.Player1;
			if (computerStarts)
			{
				m_ComputerPlayer = Player.Player1;
				DoComputerMove();
			}
			else
			{
				m_ComputerPlayer = Player.Player2;
			}
		}

		public event EventHandler<GameOverEventArgs> GameOver;
		public event EventHandler<BoardChangedEventArgs> BoardChanged;

		public Board Board
		{
			get { return m_Board; }
		}

		private void DoComputerMove()
		{
			int bestMove = GetBestMove();
			MakeMove(bestMove);
		}

		public void MakeMove(int x, int y)
		{
			int index = y * 3 + x;
			MakeMove(index);
		}

		public void MakeMove(int index)
		{
			if (m_GameOver)
			{
				throw new InvalidOperationException("Game is already over.");
			}

			m_Board[index] = (m_CurrentPlayer == Player.Player1 ? CellState.Player1 : CellState.Player2);
			OnBoardChanged();

			GameState gameState = m_Board.GetGameState();
			switch (gameState)
			{
				case GameState.InProgress:
					break;
				case GameState.Player1Won:
				case GameState.Player2Won:
				case GameState.Draw:
					OnGameOver(gameState);
					return;
				default:
					break;
			}

			// Now the other player becomes the next player
			m_CurrentPlayer = GetNextPlayer(m_CurrentPlayer);
			//if (m_CurrentPlayer == m_ComputerPlayer)
			//{
			//    // it is the computer's turn
			//    DoComputerMove();
			//    return;
			//}
		}

		public int GetBestMove()
		{
			return m_MiniMaxAlgorithm.GetBestMove(m_Board, m_CurrentPlayer);
		}

		private void OnBoardChanged()
		{
			EventHandler<BoardChangedEventArgs> handler = this.BoardChanged;
			if (handler != null)
			{
				handler(this, new BoardChangedEventArgs(m_Board));
			}
		}

		private void OnGameOver(GameState gameState)
		{
			m_GameOver = true;

			EventHandler<GameOverEventArgs> handler = this.GameOver;
			if (handler != null)
			{
				handler(this, new GameOverEventArgs(gameState));
			}
		}
	}
}
