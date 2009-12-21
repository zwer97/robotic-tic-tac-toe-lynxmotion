using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace TicTacToe.Vision
{
	public class CalibrationPoint
	{
		public static CalibrationPoint Parse(string line)
		{
			string[] parts = line.Split('\t');

			PointF cameraPoint = new PointF(
				ParseFloat(parts[0]),
				ParseFloat(parts[1]));

			PointF physicalPoint = new PointF(
				ParseFloat(parts[2]),
				ParseFloat(parts[3]));

			float unitsPerPixelX = ParseFloat(parts[4]);
			float unitsPerPixelY = ParseFloat(parts[5]);

			return new CalibrationPoint(cameraPoint, physicalPoint, unitsPerPixelX, unitsPerPixelY);
		}

		private static float ParseFloat(string text)
		{
			return float.Parse(text, CultureInfo.InvariantCulture);
		}

		private CalibrationPoint(
			PointF cameraPoint,
			PointF physicalPoint,
			float unitsPerPixelX,
			float unitsPerPixelY)
		{
			this.CameraPoint = cameraPoint;
			this.PhysicalPoint = physicalPoint;
			this.UnitsPerPixelX = unitsPerPixelX;
			this.UnitsPerPixelY = unitsPerPixelY;
		}

		public CalibrationPoint(
			CameraVsPhysicalPoint center,
			CameraVsPhysicalPoint left,
			CameraVsPhysicalPoint right,
			CameraVsPhysicalPoint above,
			CameraVsPhysicalPoint below)
		{
			this.CameraPoint = center.CameraPoint;
			this.PhysicalPoint = center.PhysicalPoint;
			this.UnitsPerPixelX = CalculateUnitsPerPixelX(left, right);
			this.UnitsPerPixelY = CalculateUnitsPerPixelY(above, below);
		}

		private float CalculateUnitsPerPixelX(CameraVsPhysicalPoint left, CameraVsPhysicalPoint right)
		{
			// left or right but not both can be null
			float pointsUsed = 0;
			float unitsPerPixelX = 0;

			if (left != null)
			{
				pointsUsed += 1;
				unitsPerPixelX += CalculateUnitsPerPixelX(left);
			}
			if (right != null)
			{
				pointsUsed += 1;
				unitsPerPixelX += CalculateUnitsPerPixelX(right);
			}

			return unitsPerPixelX / pointsUsed;
		}

		private float CalculateUnitsPerPixelX(CameraVsPhysicalPoint neighbor)
		{
			float physicalDistance = (float)Math.Sqrt(
				(neighbor.PhysicalPoint.X - this.PhysicalPoint.X) * (neighbor.PhysicalPoint.X - this.PhysicalPoint.X) +
				(neighbor.PhysicalPoint.Y - this.PhysicalPoint.Y) * (neighbor.PhysicalPoint.Y - this.PhysicalPoint.Y));

			float pixelDistance = (float)Math.Sqrt(
				(neighbor.CameraPoint.X - this.CameraPoint.X) * (neighbor.CameraPoint.X - this.CameraPoint.X) +
				(neighbor.CameraPoint.Y - this.CameraPoint.Y) * (neighbor.CameraPoint.Y - this.CameraPoint.Y));

			return physicalDistance / pixelDistance;
		}

		private float CalculateUnitsPerPixelY(CameraVsPhysicalPoint above, CameraVsPhysicalPoint below)
		{
			// above or below but not both can be null
			float pointsUsed = 0;
			float unitsPerPixelY = 0;

			if (above != null)
			{
				pointsUsed += 1;
				unitsPerPixelY += CalculateUnitsPerPixelY(above);
			}
			if (below != null)
			{
				pointsUsed += 1;
				unitsPerPixelY += CalculateUnitsPerPixelY(below);
			}

			return unitsPerPixelY / pointsUsed;
		}

		private float CalculateUnitsPerPixelY(CameraVsPhysicalPoint neighbor)
		{
			return (neighbor.PhysicalPoint.Y - this.PhysicalPoint.Y) / (neighbor.CameraPoint.Y - this.CameraPoint.Y);
		}

		public PointF CameraPoint { get; private set; }
		public PointF PhysicalPoint { get; private set; }
		public float UnitsPerPixelX { get; private set; }
		public float UnitsPerPixelY { get; private set; }

		public PointF GetPhysicalPoint(PointF cameraPoint)
		{
			return new PointF(
				this.PhysicalPoint.X + (this.CameraPoint.X - cameraPoint.X) * UnitsPerPixelX,
				this.PhysicalPoint.Y + (this.CameraPoint.Y - cameraPoint.Y) * UnitsPerPixelY);
		}

		public void SerializeAsLine(StringBuilder stringBuilder)
		{
			stringBuilder.Append(this.CameraPoint.X);
			stringBuilder.Append('\t');
			stringBuilder.Append(this.CameraPoint.Y);
			stringBuilder.Append('\t');
			stringBuilder.Append(this.PhysicalPoint.X);
			stringBuilder.Append('\t');
			stringBuilder.Append(this.PhysicalPoint.Y);
			stringBuilder.Append('\t');
			stringBuilder.Append(this.UnitsPerPixelX);
			stringBuilder.Append('\t');
			stringBuilder.Append(this.UnitsPerPixelY);
			stringBuilder.AppendLine();
		}
	}
}
