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
using System.Diagnostics;

namespace TicTacToe.Logic
{
	public class Board
	{
		static Board()
		{
		}

		/// <summary>
		/// An array holding arrays of three indexes for columns, rows, and the two diagonals;
		/// i.e., all three cell configurations that can be used to get three in a 'row'.
		/// </summary>
		private static readonly int[][] s_TripletIndexes = new int[][]
		{
			new int[] {0,1,2}, // first row
			new int[] {3,4,5}, // second row
			new int[] {6,7,8}, // third row

			new int[] {0,3,6}, // first column
			new int[] {1,4,7}, // second column
			new int[] {2,5,8}, // third column

			new int[] {0,4,8}, // first diagonal
			new int[] {2,4,6}, // second diagonal
		};

		private CellState[] m_CellStates;

		public Board()
			: this(new CellState[9])
		{
		}

		private Board(CellState[] cellStates)
		{
			m_CellStates = cellStates;
		}

		public CellState this[int index]
		{
			get
			{
				EnsureValidCellIndex(index);
				return m_CellStates[index];
			}
			internal set
			{
				EnsureValidCellIndex(index);
				m_CellStates[index] = value;
			}
		}

		private void EnsureValidCellIndex(int index)
		{
			if (index < 0 || index > 8)
			{
				throw new ArgumentException("Invalid cell index. Index must be between 0 and 8.");
			}
		}

		public CellState this[int x, int y]
		{
			get
			{
				EnsureValidCellCoordinate(x);
				EnsureValidCellCoordinate(y);
				return m_CellStates[y * 3 + x];
			}
			internal set
			{
				EnsureValidCellCoordinate(x);
				EnsureValidCellCoordinate(y);
				m_CellStates[y * 3 + x] = value;
			}
		}

		private void EnsureValidCellCoordinate(int coordinate)
		{
			if (coordinate < 0 || coordinate > 2)
			{
				throw new ArgumentException("Invalid cell coordinate. Only 0,1, or 2 are valid.");
			}
		}

		public int[] GetEmptyCells()
		{
			List<int> emptyCells = new List<int>(9);
			for (int i = 0; i < 9; i++)
			{
				if (m_CellStates[i] == CellState.Empty)
				{
					emptyCells.Add(i);
				}
			}
			return emptyCells.ToArray();
		}

		public GameState GetGameState()
		{
			bool isFull = true;
			// We search through all triplets that can be used to get three in a row
			for (int i = 0; i < s_TripletIndexes.Length; i++)
			{
				TripletState tripletState = CheckTripletState(s_TripletIndexes[i]);
				switch (tripletState)
				{
					case TripletState.Player1Won:
						return GameState.Player1Won;
					case TripletState.Player2Won:
						return GameState.Player2Won;
					case TripletState.NotFullYet:
						isFull = false;
						break;
					case TripletState.FullMixed:
						break;
					default:
						break;
				}
			}

			if (isFull)
			{
				return GameState.Draw;
			}
			else
			{
				return GameState.InProgress;
			}
		}

		public bool GameIsOver
		{
			get { return GetGameState() != GameState.InProgress; }
		}

		private TripletState CheckTripletState(int[] cellIndexes)
		{
			Debug.Assert(cellIndexes.Length == 3);

			int player1Count = 0;
			int player2Count = 0;
			int emptyCount = 0;

			for (int i = 0; i < 3; i++)
			{
				switch (m_CellStates[cellIndexes[i]])
				{
					case CellState.Empty:
						emptyCount++;
						break;
					case CellState.Player1:
						player1Count++;
						break;
					case CellState.Player2:
						player2Count++;
						break;
					default:
						break;
				}
			}

			if (player1Count == 3)
			{
				return TripletState.Player1Won;
			}
			if (player2Count == 3)
			{
				return TripletState.Player2Won;
			}
			if (emptyCount > 0)
			{
				return TripletState.NotFullYet;
			}
			return TripletState.FullMixed;
		}

		private enum TripletState
		{
			Player1Won,
			Player2Won,
			NotFullYet,
			FullMixed,
		}

		public Board Clone()
		{
			CellState[] cellStates = new CellState[m_CellStates.Length];
			m_CellStates.CopyTo(cellStates, 0);
			return new Board(cellStates);
		}

		public override string ToString()
        {
			StringBuilder stringBuilder = new StringBuilder();

			InsertRow(stringBuilder, 0);
			stringBuilder.AppendLine("-+-+-");
			InsertRow(stringBuilder, 1);
			stringBuilder.AppendLine("-+-+-");
			InsertRow(stringBuilder, 2);

			return stringBuilder.ToString();
		}

		private void InsertRow(StringBuilder stringBuilder, int y)
		{
			IncertCell(stringBuilder, 0, y);
			stringBuilder.Append("|");
			IncertCell(stringBuilder, 1, y);
			stringBuilder.Append("|");
			IncertCell(stringBuilder, 2, y);
			stringBuilder.AppendLine();
		}

		private void IncertCell(StringBuilder stringBuilder, int x, int y)
		{
			switch (this[x,y])
			{
				case CellState.Empty:
					stringBuilder.Append(" ");
					break;
				case CellState.Player1:
					stringBuilder.Append("X");
					break;
				case CellState.Player2:
					stringBuilder.Append("O");
					break;
				default:
					break;
			}
		}
	}
}
