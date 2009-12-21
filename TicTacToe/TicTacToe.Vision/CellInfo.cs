using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV;
using TicTacToe.Logic;

namespace TicTacToe.Robot.Vision
{
	public class CellInfo
	{
		public MCvBox2D Box;
		public CellState CellState;

		internal void RecordGamePiece(Image<Gray, Byte> image)
		{
			Point center = new Point((int)this.Box.center.X, (int)this.Box.center.Y);
			if (image[center].Intensity == 255.0)
			{
				this.CellState = CellState.Player1;
			}
			else
			{
				this.CellState = CellState.Empty;
			}
		}
	}
}
