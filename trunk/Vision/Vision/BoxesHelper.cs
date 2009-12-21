using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;

namespace Vision
{
	public static class BoxesHelper
	{
		public static void SortBoxesBySize(List<MCvBox2D> rectangles)
		{
			rectangles.Sort(delegate(MCvBox2D box1, MCvBox2D box2)
			{
				float area1 = box1.size.Height * box1.size.Width;
				float area2 = box2.size.Height * box2.size.Width;
				return area2.CompareTo(area1);
			});
		}

		public static void SortBoxesAsGrid(List<MCvBox2D> rectangles)
		{
			rectangles.Sort(delegate(MCvBox2D box1, MCvBox2D box2)
			{
				if (Math.Abs(box1.center.Y - box2.center.Y) < box1.size.Height / 2)
				{
					// the x location determines the order
					return box1.center.X.CompareTo(box2.center.X);
				}
				else
				{
					// the x location determines the order
					return box1.center.Y.CompareTo(box2.center.Y);
				}
			});
		}
	}
}
