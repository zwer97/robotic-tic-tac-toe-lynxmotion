using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace TicTacToe.Logic
{
	public class Location
	{
		public static int ToIndex(int x, int y)
		{
			return y * 3 + x;
		}

		public int X { get; private set; }
		public int Y { get; private set; }

		public Location(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public Location(int index)
		{
			this.Y = index / 3;
			this.X = index - this.Y * 3;
		}

		public int Index
		{
			get { return ToIndex(this.X, this.Y); }
		}

		public override string ToString()
		{
			return String.Format(CultureInfo.InvariantCulture, "({0},{1})", this.X, this.Y);
		}
	}
}
