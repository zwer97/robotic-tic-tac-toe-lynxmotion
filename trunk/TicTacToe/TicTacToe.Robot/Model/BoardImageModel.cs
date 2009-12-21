using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using Vision;
using Emgu.CV;
using System.Drawing;
using System.Diagnostics;

namespace TicTacToe.Robot.Model
{
	public class BoardImageModel
	{
		#region static stuff
		public static List<MCvBox2D> CollectRectangles(Image<Gray, Byte> blackAndWhiteImage)
		{
			List<MCvBox2D> foundRectangles = new List<MCvBox2D>();

			using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
			{
				for (Contour<Point> contours = blackAndWhiteImage.FindContours(
					Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
					Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST,
					storage);
					contours != null;
					contours = contours.HNext)
				{
					Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);
					//Debug.WriteLine(currentContour.Area);

					if (currentContour.Area > 250) //only consider contours with area greater than 250
					{
						if (currentContour.Total == 4) //The contour has 4 vertices.
						{
							if (IsRectangle(currentContour))
							{
								foundRectangles.Add(currentContour.GetMinAreaRect());
							}
						}
					}
				}
			}

			return foundRectangles;
		}

		/// <summary>
		/// Determines whether the angles are close enough to 90 degrees
		/// </summary>
		/// <param name="contour"></param>
		/// <returns></returns>
		private static bool IsRectangle(Contour<Point> contour)
		{
			Point[] pts = contour.ToArray();
			LineSegment2D[] edges = PointCollection.PolyLine(pts, true);

			for (int i = 0; i < edges.Length; i++)
			{
				LineSegment2D currentEdge = edges[i];
				LineSegment2D nextEdge = edges[(i + 1) % edges.Length];

				double angle = Math.Abs(nextEdge.GetExteriorAngleDegree(currentEdge));
				if (angle < 80 || angle > 100)
				{
					return false;
				}
			}

			return true;
		}
		#endregion

		private MCvBox2D m_OutsideRectangle;
		private MCvBox2D[,] m_InsideRectangles;

		public int XCount { get; private set; }
		public int YCount { get; private set; }

		public BoardImageModel()
		{
			this.XCount = 3;
			this.YCount = 3;
		}

		public int ExpectedRectangleCount
		{
			get { return this.XCount * this.YCount + 1; }
		}

		public void InitializeRectangles(List<MCvBox2D> foundRectangles)
		{
			BoxesHelper.SortBoxesBySize(foundRectangles);
			// The first one in the outer rectangle
			m_OutsideRectangle = foundRectangles[0];
			List<MCvBox2D> insideRectangles = new List<MCvBox2D>(foundRectangles);
			insideRectangles.RemoveAt(0);

			BoxesHelper.SortBoxesAsGrid(insideRectangles);

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

		public bool IsFilled(int x, int y, Image<Gray, Byte> image)
		{
			MCvBox2D box = this.InsideRectangles[x,y];
			using (Image<Gray, Byte> cellImage = image.Copy(box))
			{
				int nonZeroCount = cellImage.CountNonzero()[0];
				//Debug.WriteLine(nonZeroCount);
				return nonZeroCount > 60;
			}
		}

		public bool IsEmpty(Image<Gray, Byte> image)
		{
			for (int x = 0; x < this.XCount; x++)
			{
				for (int y = 0; y < this.YCount; y++)
				{
					if (IsFilled(x, y, image))
					{
						return false;
					}
				}
			}
			return true;
		}

		internal List<BlobInfo> FindUnusedRobotPieces(Image<Gray, Byte> image)
		{
			List<BlobInfo> blobInfos = new List<BlobInfo>();

			using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
			{
				for (Contour<Point> contours = image.FindContours(
					Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
					Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST,
					storage);
					contours != null;
					contours = contours.HNext)
				{
					Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);
					BlobInfo blobInfo = new BlobInfo(currentContour);

					Debug.WriteLine("BlobInfo: " + blobInfo.Area + ", center: " + blobInfo.CameraCenter);

					// We are only interested in the blobs that are left of the grid
					if (blobInfo.CameraCenter.X + Math.Sqrt(blobInfo.Area)/2 < m_OutsideRectangle.center.X - m_OutsideRectangle.size.Width/2 - 10)
					{
						blobInfos.Add(new BlobInfo(currentContour));
					}
				}
			}

			return blobInfos;
		}
	}
}
