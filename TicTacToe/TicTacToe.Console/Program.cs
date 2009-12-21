using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Logic;

namespace TicTacToe.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				BoardManager boardManager = new BoardManager(true);
				Board board = boardManager.Board;
				System.Console.WriteLine(board);

				boardManager.MakeMove(1, 1);
				System.Console.WriteLine(board);

				//board[0, 1] = CellState.Player2;
				//System.Console.WriteLine(board);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.ToString());
			}
			finally
			{
				System.Console.ReadLine();
			}
		}
	}
}
