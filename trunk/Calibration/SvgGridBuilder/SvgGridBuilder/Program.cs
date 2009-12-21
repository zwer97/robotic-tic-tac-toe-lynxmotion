using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SvgNet.SvgTypes;

namespace SvgGridBuilder
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				CreateRobotCalibrationGrid();
				//CreateCameraCalibrationGrid();
				//CreateTicTacToeGrid();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			finally
			{
				Console.WriteLine("Done");
				Console.ReadLine();
			}
		}

		private static void CreateRobotCalibrationGrid()
		{
			Page page = new Page(700, 350, SvgLengthType.SVG_LENGTHTYPE_MM);
			GridBuilder gridBuilder = new GridBuilder(page, 15);

			gridBuilder.AddHorizontalLines(5);
			gridBuilder.AddVerticalLines(5);
			gridBuilder.AddAngleLines(1, 50);
			gridBuilder.AddCircles(5);
			gridBuilder.Save("Grid.svg");
		}

		private static void CreateCameraCalibrationGrid()
		{
			Page page = new Page(275, 215, SvgLengthType.SVG_LENGTHTYPE_MM);

			GridBuilder2 gridBuilder = new GridBuilder2(page, 15);

			gridBuilder.AddSquares(75, 0, 30, 7, 3);
			gridBuilder.AddAlignmentLines();
			gridBuilder.Save("Squares.svg");
		}

		private static void CreateTicTacToeGrid()
		{
			Page page = new Page(275, 215, SvgLengthType.SVG_LENGTHTYPE_MM);

			GridBuilder2 gridBuilder = new GridBuilder2(page, 15);

			gridBuilder.AddSquares(75, 30, 30, 3, 3);
			gridBuilder.AddAlignmentLines();
			gridBuilder.Save("TicTacToe.svg");
		}
	}
}
