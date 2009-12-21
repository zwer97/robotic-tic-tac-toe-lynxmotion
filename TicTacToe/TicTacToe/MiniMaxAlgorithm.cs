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
	internal class MiniMaxAlgorithm
	{
		internal int GetBestMove(Board board, Player currentPlayer)
		{
			int bestMoveValue = Int32.MinValue;
			int bestMove = -1;

			int[] possibleMoves = board.GetEmptyCells();
			for (int i = 0; i < possibleMoves.Length; i++)
			{
				Board successorBoard = board.Clone();
				successorBoard[possibleMoves[i]] = (currentPlayer == Player.Player1 ? CellState.Player1 : CellState.Player2);

				// We need to find the maximum of the min values
				Player nextPlayer = BoardManager.GetNextPlayer(currentPlayer);
				int minValue = GetMinMove(successorBoard, nextPlayer);

				if (minValue > bestMoveValue)
				{
					bestMoveValue = minValue;
					bestMove = possibleMoves[i];
				}
			}

			return bestMove;
		}

		private int GetMaxMove(Board board, Player currentPlayer)
		{
			GameState gameState = board.GetGameState();
			if (gameState != GameState.InProgress)
			{
				return GetValueForCompletedGame(gameState, currentPlayer);
			}

			// the game has not yet ended
			int bestMoveValue = Int32.MinValue;

			int[] possibleMoves = board.GetEmptyCells();
			for (int i = 0; i < possibleMoves.Length; i++)
			{
				Board successorBoard = board.Clone();
				successorBoard[possibleMoves[i]] = (currentPlayer == Player.Player1 ? CellState.Player1 : CellState.Player2);

				// We need to find the maximum of the min values
				Player nextPlayer = BoardManager.GetNextPlayer(currentPlayer);
				int minValue = GetMinMove(successorBoard, nextPlayer);

				if (minValue > bestMoveValue)
				{
					bestMoveValue = minValue;
				}
			}

			return bestMoveValue;
		}

		private int GetMinMove(Board board, Player currentPlayer)
		{
			GameState gameState = board.GetGameState();
			if (gameState != GameState.InProgress)
			{
				return -GetValueForCompletedGame(gameState, currentPlayer);
			}

			// the game has not yet ended
			int bestMoveValue = Int32.MaxValue;

			int[] possibleMoves = board.GetEmptyCells();
			for (int i = 0; i < possibleMoves.Length; i++)
			{
				Board successorBoard = board.Clone();
				successorBoard[possibleMoves[i]] = (currentPlayer == Player.Player1 ? CellState.Player1 : CellState.Player2);

				// We need to find the minimum of the max values
				Player nextPlayer = BoardManager.GetNextPlayer(currentPlayer);
				int maxValue = GetMaxMove(successorBoard, nextPlayer);

				if (maxValue < bestMoveValue)
				{
					bestMoveValue = maxValue;
				}
			}

			return bestMoveValue;
		}

		private int GetValueForCompletedGame(GameState gameState, Player player)
		{
			switch (gameState)
			{
				case GameState.InProgress:
					throw new ArgumentException("The game has not ended yet.");
				case GameState.Player1Won:
				case GameState.Player2Won:
					if (gameState == GameState.Player1Won && player == Player.Player1 ||
						gameState == GameState.Player2Won && player == Player.Player2)
					{
						return 1;
					}
					else
					{
						return -1;
					}
				case GameState.Draw:
					return 0;
				default:
					throw new ArgumentException("Invalid game state.");
			}
		}
	}
}
