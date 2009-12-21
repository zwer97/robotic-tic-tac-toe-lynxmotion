using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV;

namespace TicTacToe.Robot
{
	public class BlobInfo
	{
		public MCvBox2D MinAreaRect { get; private set; }
		public PointF CameraCenter { get; private set; }
		public PointF PhysicalCenter { get; private set; }
		public double Area { get; private set; }

		public BlobInfo(Contour<Point> contour)
		{
			this.Area = contour.Area;
			this.MinAreaRect = GetMinAreaRect(contour);

			this.CameraCenter = this.MinAreaRect.center;

			if (global::Vision.CameraCalibration.Instance.IsInitialized)
			{
				this.PhysicalCenter = global::Vision.CameraCalibration.Instance.GetPhysicalPoint(this.CameraCenter);
			}
		}

		private MCvBox2D GetMinAreaRect(Contour<Point> contour)
		{
			Point[] points = contour.ToArray();
			PointF[] pointsF = new PointF[points.Length];

			for (int i = 0; i < points.Length; i++)
			{
				pointsF[i] = new PointF(
					points[i].X,
					points[i].Y);
			}

			return PointCollection.MinAreaRect(pointsF);
		}
	}
}
