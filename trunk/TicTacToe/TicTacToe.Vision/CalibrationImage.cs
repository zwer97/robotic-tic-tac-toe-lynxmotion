using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;

namespace TicTacToe.Vision
{
	public class CalibrationImage : ImageModel
	{
		private MCvBox2D[,] m_InsideRectangles;

		private float m_SideLength;
		private float m_YOffset;
		private PointF m_PhysCenterFirstBox;

		public int XCount { get; private set; }
		public int YCount { get; private set; }

		public CalibrationImage(int xCount, int yCount, float sideLength, float yOffSet)
		{
			this.XCount = xCount;
			this.YCount = yCount;

			m_SideLength = sideLength;
			m_YOffset = yOffSet;

			m_PhysCenterFirstBox = new PointF(
				- (this.XCount - 1) / 2.0F * m_SideLength,
				yOffSet + m_SideLength / 2.0F);
		}

		public override int ExpectedRectangleCount
		{
			get { return this.XCount * this.YCount + 1; }
		}

		public override void AssignFoundRectangles(List<MCvBox2D> foundRectangles)
		{
			base.SortRectanglesBySize(foundRectangles);
			// The first one in the outer rectangle which we discard
			List<MCvBox2D> insideRectangles = new List<MCvBox2D>(foundRectangles);
			insideRectangles.RemoveAt(0);

			base.SortRectanglesAsGrid(insideRectangles);

			m_InsideRectangles = new MCvBox2D[this.XCount, this.YCount];
			for (int i = 0; i < this.XCount; i++)
			{
				for (int j = 0; j < this.YCount; j++)
				{
					//string message = String.Format("{0},{1}: {2}", i,j, j * this.XCount + i);
					//Debug.WriteLine(message);
					m_InsideRectangles[i, j] = insideRectangles[j * this.XCount + i];
				}
			}

			//CameraCalibration.Instance.Initialize(cameraVsPhysicalPoints);
		}

		public MCvBox2D[,] InsideRectangles
		{
			get { return m_InsideRectangles; }
		}

		public IEnumerable<MCvBox2D> GetInsideRectangles()
		{
			for (int j = 0; j < this.YCount; j++)
			{
				for (int i = 0; i < this.XCount; i++)
				{
					//string message = String.Format("{0},{1}", i, j);
					//Debug.WriteLine(message);

					yield return m_InsideRectangles[i, j];
				}
			}
		}

		public CameraVsPhysicalPoint[,] GetCameraVsPhysicalPoints()
		{
			CameraVsPhysicalPoint[,] cameraVsPhysicalPoints = new CameraVsPhysicalPoint[this.XCount, this.YCount];
			for (int i = 0; i < this.XCount; i++)
			{
				for (int j = 0; j < this.YCount; j++)
				{
					PointF physicalCenter = GetPhysicalCenter(i, j);
					MCvBox2D box = m_InsideRectangles[i, j];

					cameraVsPhysicalPoints[i, j] = new CameraVsPhysicalPoint(
						box.center,
						physicalCenter);
				}
			}

			return cameraVsPhysicalPoints;
		}

		private PointF GetPhysicalCenter(int xIndex, int yIndex)
		{
			return new PointF(
				m_PhysCenterFirstBox.X + xIndex * m_SideLength,
				m_PhysCenterFirstBox.Y + yIndex * m_SideLength);
		}
	}
}
