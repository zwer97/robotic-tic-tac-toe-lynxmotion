using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace TicTacToe.Robot.Model
{
	internal class ImageProcessor
	{
		internal const int c_ChangeThreshold = 15;
		internal static readonly TimeSpan c_MinStablePeriod = TimeSpan.FromSeconds(0.5);

		private Capture m_Capture;
		private Rectangle m_RegionOfInterest = new Rectangle(33, 100, 270, 120);

		private int m_Threshold = 128;
		private int m_CountNonZero;
		private int m_DeltaNonZero;
		//private bool m_IsStable = true;
		private DateTime m_UtcLastSignificantChange = DateTime.UtcNow; 

		private Image<Bgr, Byte> m_OriginalImage = null;
		private Image<Bgr, Byte> m_ClippedImage = null;
		private Image<Bgr, Byte> m_ErodedImage = null;
		private Image<Gray, Byte> m_GrayImage = null;
		private Image<Gray, Byte> m_BlackAndWhiteImage = null;
		private Image<Bgr, Byte> m_DetectedRectanglesImage = null;
		private Image<Gray, Byte> m_DetectedBlobsImage = null;

		private List<MCvBox2D> m_FoundRectangles = new List<MCvBox2D>();
		private List<BlobInfo> m_BlobInfos = new List<BlobInfo>();

		public ImageProcessor()
		{
			m_Capture = new Capture();
			m_Capture.FlipHorizontal = true;
			m_Capture.FlipVertical = true;
		}

		public event EventHandler Changed;

		public Rectangle RegionOfInterest
		{
			get { return m_RegionOfInterest; }
			set { m_RegionOfInterest = value; }
		}

		public int Threshold
		{
			get { return m_Threshold; }
			set
			{
				if (value == m_Threshold)
				{
					return;
				}

				m_Threshold = value;
				RaiseChangedEvent();
			}
		}

		public void ProcessFrame()
		{
			m_OriginalImage = m_Capture.QueryFrame();
			m_ClippedImage = m_OriginalImage.Copy(this.RegionOfInterest);

			// Make the dark portions bigger
			//m_ErodedImage = m_ClippedImage.Erode(1);

			//Convert the image to grayscale
			m_GrayImage = m_ClippedImage.Convert<Gray, Byte>(); //.PyrDown().PyrUp();

			m_BlackAndWhiteImage = m_GrayImage.ThresholdBinaryInv(new Gray(m_Threshold), new Gray(255));

			int countNonZero = m_BlackAndWhiteImage.CountNonzero()[0];
			m_DeltaNonZero = Math.Abs(m_CountNonZero - countNonZero);

			if (m_DeltaNonZero > c_ChangeThreshold)
			{
				m_UtcLastSignificantChange = DateTime.UtcNow;
			}
			m_CountNonZero = countNonZero;

			//FindBlobsAndDraw(m_BlackAndWhiteImage);
			//RaiseChangedEvent();
		}

		internal void UpdateRectanglesImage(IEnumerable<MCvBox2D> rectangles)
		{
			m_DetectedRectanglesImage = m_ClippedImage.CopyBlank();
			foreach (MCvBox2D box in rectangles)
			{
				m_DetectedRectanglesImage.Draw(box, new Bgr(Color.White), 1);
			}
		}

		internal void DrawUnusedRobotPieces(List<BlobInfo> blobInfos)
		{
			m_DetectedBlobsImage = m_GrayImage.CopyBlank();

			int width = 0;
			foreach (BlobInfo blobInfo in blobInfos)
			{
				width++;
				PointF[] pointsF = blobInfo.MinAreaRect.GetVertices();
				Point[] points = new Point[pointsF.Length];

				for (int i = 0; i < points.Length; i++)
				{
					points[i] = new Point(
						(int)pointsF[i].X,
						(int)pointsF[i].Y);
				}

				m_DetectedBlobsImage.DrawPolyline(points, true, new Gray(255), width);
			}
		}

		public Image<Bgr, Byte> OriginalImage
		{
			get { return m_OriginalImage; }
		}

		public Image<Bgr, Byte> ClippedImage
		{
			get { return m_ClippedImage; }
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

		public Image<Bgr, Byte> DetectedRectanglesImage
		{
			get { return m_DetectedRectanglesImage; }
		}

		public Image<Gray, Byte> DetectedBlobsImage
		{
			get
			{
				if (m_DetectedBlobsImage == null)
				{
					m_DetectedBlobsImage = this.BlackAndWhiteImage.CopyBlank();
				}
				return m_DetectedBlobsImage;
			}
			set { m_DetectedBlobsImage = value; }
		}

		public int DeltaNonZero
		{
			get { return m_DeltaNonZero; }
		}

		public bool ImageIsStable
		{
			get
			{
				return (DateTime.UtcNow.Ticks - m_UtcLastSignificantChange.Ticks) > c_MinStablePeriod.Ticks;
			}
		}

		private void RaiseChangedEvent()
		{
			EventHandler handler = this.Changed;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}
	}
}
