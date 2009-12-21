using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV;

namespace TicTacToe.Robot.Vision
{
	public abstract class SectionBase
	{
		private int m_XCount, m_YCount;
		private MCvBox2D m_OuterBox;
		private CellInfo[] m_CellInfos;

		protected SectionBase(int xCount, int yCount)
		{
			m_XCount = xCount;
			m_YCount = yCount;

			m_CellInfos = new CellInfo[xCount * yCount];
			for (int i = 0; i < xCount * yCount; i++)
			{
				m_CellInfos[i] = new CellInfo();
			}
		}

		internal void InitializeBoxes(MCvBox2D outerBox, List<MCvBox2D> allFoundRectangles, int startIndex)
		{
			m_OuterBox = outerBox;
			List<MCvBox2D> insideBoxes = GetOrderedInsideBoxes(allFoundRectangles, startIndex);
			for (int i = 0; i < m_CellInfos.Length; i++)
			{
				m_CellInfos[i].Box = insideBoxes[i];
			}
		}

		private List<MCvBox2D> GetOrderedInsideBoxes(List<MCvBox2D> allFoundRectangles, int startIndex)
		{
			PointF[] vertices = m_OuterBox.GetVertices();
			List<MCvBox2D> insideBoxes = new List<MCvBox2D>(m_CellInfos.Length);
			// The first three boxes are the outer boxes. Hence we skip them.
			for (int i = startIndex; i < allFoundRectangles.Count; i++)
			{
				if (PolygonContains(vertices, allFoundRectangles[i].center))
				{
					insideBoxes.Add(allFoundRectangles[i]);
				}
			}
			if (insideBoxes.Count != m_CellInfos.Length)
			{
				return null;
			}

			insideBoxes.Sort(delegate(MCvBox2D box1, MCvBox2D box2)
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
			return insideBoxes;
		}

		/// <summary>
		/// The algorithm is described here: http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html
		/// </summary>
		/// <param name="points">Polygon vertices</param>
		/// <param name="point">Point to check</param>
		/// <returns></returns>
		private bool PolygonContains(PointF[] points, PointF point)
		{
			bool isIn = false;
			for (int i = 0; i < points.Length; i++)
			{
				int j = (i + points.Length - 1) % points.Length;
				if (
					((points[i].Y > point.Y) != (points[j].Y > point.Y)) &&
					(point.X < (points[j].X - points[i].X) * (point.Y - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X)
				   )
				{
					isIn = !isIn;
				}
			}
			return isIn;
		}

		public MCvBox2D OuterBox
		{
			get { return m_OuterBox; }
		}

		public CellInfo this[int x, int y]
		{
			get { return m_CellInfos[y * m_XCount + x]; }
		}

		public CellInfo this[int i]
		{
			get { return m_CellInfos[i]; }
		}

		internal void RecordGamePieces(Image<Gray, Byte> image)
		{
			for (int i = 0; i < m_CellInfos.Length; i++)
			{
				m_CellInfos[i].RecordGamePiece(image);
			}
		}

		internal void DrawOn(Image<Bgr, Byte> image, Color color)
		{
			if (m_OuterBox.size == Size.Empty)
			{
				return;
			}
			image.Draw(m_OuterBox, new Bgr(color), 1);
			for (int i = 0; i < m_CellInfos.Length; i++)
			{
				if (m_CellInfos[i].Box.size != Size.Empty)
				{
					image.Draw(m_CellInfos[i].Box, new Bgr(color), 1);
				}
			}
		}
	}
}
