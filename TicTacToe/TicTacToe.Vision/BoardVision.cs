using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;

namespace TicTacToe.Robot.Vision
{
	public class BoardVision
	{
		private Capture m_Capture;
		//private Rectangle m_RegionOfInterest = new Rectangle(33, 60, 270, 120);
		//private Rectangle m_RegionOfInterest = new Rectangle(0, 0, 320, 240);
		private Rectangle m_RegionOfInterest = new Rectangle(33, 100, 270, 120);

		private Image<Bgr, Byte> m_OriginalImage = null;
		private Image<Bgr, Byte> m_ClippedImage = null;
		private Image<Bgr, Byte> m_WhiteBalancedImage = null;
		private Image<Bgr, Byte> m_ErodedImage = null;
		private Image<Gray, Byte> m_GrayImage = null;
		private Image<Gray, Byte> m_BlackAndWhiteImage = null;
		private Image<Bgr, Byte> m_FoundRectanglesImage = null;

		private IDisposable[] m_Disposables;

		private GameSection m_GameSection = new GameSection();
		private ParkSection m_LeftParkSection = new ParkSection();
		private ParkSection m_RightParkSection = new ParkSection();

		//private ImageModel m_ImageModel = new CalibrationImage(7,3, 30, 75);

		public BoardVision()
		{
			m_Capture = new Capture();
			m_Capture.FlipHorizontal = true;
			m_Capture.FlipVertical = true;

			m_Disposables = new IDisposable[]
			{
				m_OriginalImage,
				m_ClippedImage,
				m_WhiteBalancedImage,
				m_ErodedImage,
				m_GrayImage,
				m_BlackAndWhiteImage,
				m_FoundRectanglesImage
			};
		}

		public Rectangle RegionOfInterest
		{
			get { return m_RegionOfInterest; }
			set { m_RegionOfInterest = value; }
		}

		public void ProcessFrame(int threshold)
		{
			DisposeImages();
			m_OriginalImage = m_Capture.QueryFrame();

			m_ClippedImage = m_OriginalImage.Copy(this.RegionOfInterest);
			//m_ClippedImage.PyrDown().PyrUp();

			//Image<Gray, Byte>[] channels = new Image<Gray,byte>[]
			//{
			//    m_ClippedImage[0],
			//    m_ClippedImage[1],
			//    m_ClippedImage[2]
			//};

			//for (int i = 0; i < 3; i++)
			//{
			//    channels[i]._EqualizeHist();
			//}
			//m_ClippedImage[0]._EqualizeHist();
			//m_ClippedImage[1]._EqualizeHist();
			//m_ClippedImage[2]._EqualizeHist();

			//m_WhiteBalancedImage = channels[2]; // new Image<Bgr, byte>(channels);

			// Make the dark portions bigger
			m_ErodedImage = m_ClippedImage.Erode(1);

			//StructuringElementEx structuringElementEx= new StructuringElementEx(new int[1,1], 0,0);
			//m_WhiteBalancedImage = m_ErodedImage.MorphologyEx(structuringElementEx, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_BLACKHAT, 2);   //.Erode(1);

			//Convert the image to grayscale
			m_GrayImage = m_ErodedImage.Convert<Gray, Byte>(); //.PyrDown().PyrUp();

			//Bgr threshold = new Bgr(127, 127, 127);
			//Bgr maxValue = new Bgr(255, 255, 255);
			m_BlackAndWhiteImage = m_GrayImage.ThresholdBinaryInv(new Gray(threshold), new Gray(255));

			List<MCvBox2D> foundRectangles = FindRectangles(m_BlackAndWhiteImage);
			//Debug.WriteLine(foundRectangles.Count);
			//if (foundRectangles.Count != m_ImageModel.ExpectedRectangleCount)
			//{
			//    // not all required rectangles found
			//    return;
			//}

			//m_ImageModel.AssignFoundRectangles(foundRectangles);
			////AssignFoundRectangles(foundRectangles);
			//m_FoundRectanglesImage = CreateRectanglesImage();
			//RecordGamePieces();
		}

		private List<MCvBox2D> FindRectangles(Image<Gray, Byte> blackAndWhiteImage)
		{
			List<MCvBox2D> boxList = new List<MCvBox2D>(); //a box is a rotated rectangle

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
						//if (currentContour.Total == 3) //The contour has 3 vertices, it is a triangle
						//{
						//    Point[] pts = currentContour.ToArray();
						//    triangleList.Add(new Triangle2DF(
						//       pts[0],
						//       pts[1],
						//       pts[2]
						//       ));
						//}
						if (currentContour.Total == 4) //The contour has 4 vertices.
						{
							if (IsRectangle(currentContour))
							{
								boxList.Add(currentContour.GetMinAreaRect());
							}
						}
					}
				}
			}

			return boxList;
		}

		/// <summary>
		/// Determines whether the angles are close enough to 90 degrees
		/// </summary>
		/// <param name="contour"></param>
		/// <returns></returns>
		private bool IsRectangle(Contour<Point> contour)
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

		private void AssignFoundRectangles(List<MCvBox2D> foundRectangles)
		{
			// first we sort by size
			foundRectangles.Sort(delegate(MCvBox2D box1, MCvBox2D box2)
			{
				float area1 = box1.size.Height * box1.size.Width;
				float area2 = box2.size.Height * box2.size.Width;
				return area2.CompareTo(area1);
			});

			//Debug.WriteLine("Areas");
			//for (int i = 0; i < foundRectangles.Count; i++)
			//{
			//    float area1 = foundRectangles[i].size.Height * foundRectangles[i].size.Width;
			//    Debug.WriteLine(area1);
			//}

			// The biggest box is the outer box of the game section
			m_GameSection.InitializeBoxes(foundRectangles[0], foundRectangles, 3);

			// the next two (slightly smaller) boxes are the outer boxes of the two park section
			if (foundRectangles[1].center.X < foundRectangles[2].center.X)
			{
				m_LeftParkSection.InitializeBoxes(foundRectangles[1], foundRectangles, 3);
				m_RightParkSection.InitializeBoxes(foundRectangles[2], foundRectangles, 3);
			}
			else
			{
				m_LeftParkSection.InitializeBoxes(foundRectangles[2], foundRectangles, 3);
				m_RightParkSection.InitializeBoxes(foundRectangles[1], foundRectangles, 3);
			}
		}

		public GameSection GameSection
		{
			get { return m_GameSection; }
		}

		public ParkSection LeftParkSection
		{
			get { return m_LeftParkSection; }
		}

		public ParkSection RightParkSection
		{
			get { return m_RightParkSection; }
		}

		private Image<Bgr, Byte> CreateRectanglesImage()
		{
			Image<Bgr, Byte> rectangleImage = m_ClippedImage.CopyBlank();
			m_GameSection.DrawOn(rectangleImage, Color.White);
			m_LeftParkSection.DrawOn(rectangleImage, Color.Yellow);
			m_RightParkSection.DrawOn(rectangleImage, Color.Red);
			return rectangleImage;
		}

		private void RecordGamePieces()
		{
			Image<Gray, Byte> image = m_BlackAndWhiteImage;
			m_GameSection.RecordGamePieces(image);
			m_LeftParkSection.RecordGamePieces(image);
			m_RightParkSection.RecordGamePieces(image);
		}

		public Image<Bgr, Byte> OriginalImage
		{
			get { return m_OriginalImage; }
		}

		public Image<Bgr, Byte> ClippedImage
		{
			get { return m_ClippedImage; }
		}

		public Image<Bgr, Byte> WhiteBalancedImage
		{
			get { return m_WhiteBalancedImage; }
		}

		public Image<Bgr, Byte> ErodedImage
		{
			get { return m_ErodedImage; }
		}

		public Image<Gray, Byte> GrayImage
		{
			get { return m_GrayImage; }
		}

		public Image<Gray, Byte> BlackAndWhiteImage
		{
			get { return m_BlackAndWhiteImage; }
		}

		public Image<Bgr, Byte> FoundRectanglesImage
		{
			get { return m_FoundRectanglesImage; }
		}

		private void DisposeImages()
		{
			for (int i = 0; i < m_Disposables.Length; i++)
			{
				IDisposable disposable = m_Disposables[i];
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		//internal void OnExit()
		//{
		//    if (m_SSC32Board != null)
		//    {
		//        m_SSC32Board.Dispose();
		//    }
		//}
	}
}
