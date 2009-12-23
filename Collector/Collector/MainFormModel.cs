using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using SSC32Communication;
using Robot.Kinematics;
using Collector.Properties;

namespace Collector
{
	internal class MainFormModel
	{
		private const double c_OpenGripperPosition = 20;
		private const double c_ClosedGripperPosition = 8;
		private const double c_UpperHeight = 30;

		private SSC32Board m_SSC32Board;
		private Servos m_Servos;

		private Vector3 m_HomeLocation = new Vector3(0, -120, 0);
		private Vector3 m_DropOffLocation = new Vector3(50, 120, c_UpperHeight);

		private Capture m_Capture;
		private Rectangle m_RegionOfInterest = new Rectangle(33, 100, 270, 120);
		private int m_Threshold = 128;
		private bool m_IsBusy = false;
		internal object SyncObject = new object();

		private List<BlobInfo> m_BlobInfos = new List<BlobInfo>();

		private Image<Bgr, Byte> m_OriginalImage = null;
		private Image<Bgr, Byte> m_ClippedImage = null;
		private Image<Bgr, Byte> m_ErodedImage = null;
		private Image<Gray, Byte> m_GrayImage = null;
		private Image<Gray, Byte> m_BlackAndWhiteImage = null;
		private Image<Bgr, Byte> m_DetectedBlobsImage = null;

		public event EventHandler Changed;

		public MainFormModel()
		{
			m_Capture = new Capture();
			m_Capture.FlipHorizontal = true;
			m_Capture.FlipVertical = true;

			m_Servos = new Servos();
			RestoreRobotCalibration();
			RestoreCameraCalibration();
		}

		private void RestoreRobotCalibration()
		{
			if (!String.IsNullOrEmpty(Settings.Default.RobotCalibrationFile))
			{
				try
				{
					CalibrationFile.Read(Settings.Default.RobotCalibrationFile, m_Servos);
				}
				catch (Exception ex)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("Error: Could not read robot calibration data from file:");
					stringBuilder.AppendLine(Settings.Default.RobotCalibrationFile);
					stringBuilder.AppendLine("Original error:");
					stringBuilder.AppendLine(ex.Message);

					MessageBox.Show(stringBuilder.ToString());
				}
			}
		}

		private void RestoreCameraCalibration()
		{
			if (!String.IsNullOrEmpty(Settings.Default.CameraCalibrationFile))
			{
				try
				{
					Vision.CameraCalibration.Instance.InitializeFromFile(Settings.Default.CameraCalibrationFile);
				}
				catch (Exception ex)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("Error: Could not read camera calibration data from file:");
					stringBuilder.AppendLine(Settings.Default.CameraCalibrationFile);
					stringBuilder.AppendLine("Original error:");
					stringBuilder.AppendLine(ex.Message);

					MessageBox.Show(stringBuilder.ToString());
				}
			}
		}

		public Rectangle RegionOfInterest
		{
			get { return m_RegionOfInterest; }
			set { m_RegionOfInterest = value; }
		}

		internal void LoadRobotCalibration(string calibrationFileName)
		{
			SSC32Communication.CalibrationFile.Read(calibrationFileName, m_Servos);
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

		public bool IsBusy
		{
			get
			{
				lock (SyncObject)
				{
					return m_IsBusy;
				}
			}
			set
			{
				lock (SyncObject)
				{
					if (value == m_IsBusy)
					{
						return;
					}

					m_IsBusy = value;
				}

				RaiseChangedEvent();
			}
		}

		public void ProcessFrame()
		{
			lock (SyncObject)
			{
				m_OriginalImage = m_Capture.QueryFrame();

				m_ClippedImage = m_OriginalImage.Copy(this.RegionOfInterest);

				// Make the dark portions bigger
				m_ErodedImage = m_ClippedImage.Erode(1);

				//Convert the image to grayscale
				m_GrayImage = m_ErodedImage.Convert<Gray, Byte>(); //.PyrDown().PyrUp();

				m_BlackAndWhiteImage = m_GrayImage.ThresholdBinaryInv(new Gray(m_Threshold), new Gray(255));

				FindBlobsAndDraw(m_BlackAndWhiteImage);
			}
			RaiseChangedEvent();
		}

		private void FindBlobsAndDraw(Image<Gray, Byte> blackAndWhiteImage)
		{
			m_BlobInfos.Clear();
			m_DetectedBlobsImage = m_ClippedImage.CopyBlank();

			using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
			{
				int width = 0;
				for (Contour<Point> contours = blackAndWhiteImage.FindContours(
					Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
					Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST,
					storage);
					contours != null;
					contours = contours.HNext)
				{
					Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);
					//Debug.WriteLine(currentContour.Area);
					m_BlobInfos.Add(new BlobInfo(currentContour));

					width++;
					m_DetectedBlobsImage.DrawPolyline(currentContour.ToArray(), true, new Bgr(Color.White), width);
				}
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

		public Image<Bgr, Byte> DetectedBlobsImage
		{
			get { return m_DetectedBlobsImage; }
		}

		public List<BlobInfo> BlobInfos
		{
			get { return m_BlobInfos; }
		}

		public string GetBlobInfosString()
		{
			StringBuilder stringBuilder = new StringBuilder();

			foreach (BlobInfo blobInfo in m_BlobInfos)
			{
				stringBuilder.AppendFormat(
					"{0:##0.#}   {1:##0.#},{2:##0.#}   {3:##0.#},{4:##0.#}",
					blobInfo.Area, blobInfo.CameraCenter.X, blobInfo.CameraCenter.Y, blobInfo.PhysicalCenter.X, blobInfo.PhysicalCenter.Y);
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		private void RaiseChangedEvent()
		{
			EventHandler handler = this.Changed;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		private SSC32Board SSC32Board
		{
			get
			{
				if (m_SSC32Board == null)
				{
					string portName = SerialPortHelper.FindProlificPortName();
					m_SSC32Board = new SSC32Board(portName);
				}
				return m_SSC32Board;
			}
		}

		internal void StartMoveToHomePosition()
		{
			this.IsBusy = true;
			Action action = new Action(MoveToHomePosition);
			action.BeginInvoke(new AsyncCallback(OnMoveToHomePositionComplete), null);
		}

		private void MoveToHomePosition()
		{
			Debug.WriteLine("Moving to home position.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(m_HomeLocation.X, m_HomeLocation.Y, m_HomeLocation.Z);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = jointAngles.WristTiltAngle;
			moveCommand[ChannelId.Gripper] = c_OpenGripperPosition;

			this.SSC32Board.RunCommand(moveCommand);
		}

		private void OnMoveToHomePositionComplete(IAsyncResult result)
		{
			this.IsBusy = false;
		}

		public void StartCollectPieces()
		{
			this.IsBusy = true;
			Action action = new Action(CollectPieces);
			action.BeginInvoke(new AsyncCallback(OnCollectPiecesComplete), null);
		}

		private void CollectPieces()
		{
			lock (SyncObject)
			{
				if (this.BlobInfos.Count == 0)
				{
					return;
				}
			}

			MoveAboveHomePosition();
			while (true)
			{
				BlobInfo currentBlobInfo = null;
				lock (SyncObject)
				{
					if (this.BlobInfos.Count == 0)
					{
						break;
					}
					else
					{
						currentBlobInfo = this.BlobInfos[0];
					}
				}

				Collect(currentBlobInfo);
				Thread.Sleep(200);
			}

			MoveAboveHomePosition();
			MoveToHomePosition();
		}

		private void OnCollectPiecesComplete(IAsyncResult result)
		{
			this.IsBusy = false;
		}

		public void Collect(BlobInfo blobInfo)
		{
			Vector3 pickupLocation = new Vector3(blobInfo.PhysicalCenter.X, blobInfo.PhysicalCenter.Y, 0);

			MoveAbovePickupLocation(pickupLocation);
			OpenGripper();
			MoveDownToPickupLocation(pickupLocation);
			CloseGripper();
			MoveAbovePickupLocation(pickupLocation);
			MoveToDropTarget();
			OpenGripper();
		}

		private void MoveAboveHomePosition()
		{
			Debug.WriteLine("Moving above home position.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(m_HomeLocation.X, m_HomeLocation.Y, m_HomeLocation.Z + 30);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = jointAngles.WristTiltAngle;
			moveCommand[ChannelId.Gripper] = c_OpenGripperPosition;

			this.SSC32Board.RunCommand(moveCommand);
		}

		private void MoveAbovePickupLocation(Vector3 pickupLocation)
		{
			Debug.WriteLine("Moving above pickup location.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(pickupLocation.X, pickupLocation.Y, pickupLocation.Z + c_UpperHeight);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = jointAngles.WristTiltAngle;

			this.SSC32Board.RunCommand(moveCommand);
		}

		private void MoveDownToPickupLocation(Vector3 pickupLocation)
		{
			Debug.WriteLine("Moving to pickup location.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(pickupLocation.X, pickupLocation.Y, pickupLocation.Z);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = jointAngles.WristTiltAngle;

			this.SSC32Board.RunCommand(moveCommand);
		}

		private void CloseGripper()
		{
			Debug.WriteLine("Closing gripper.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);
			moveCommand[ChannelId.Gripper] = c_ClosedGripperPosition;
			this.SSC32Board.RunCommand(moveCommand);
		}

		private void MoveToDropTarget()
		{
			Debug.WriteLine("Moving to drop target.");
			JointAngles jointAngles = GetJointAngles(m_DropOffLocation.X, m_DropOffLocation.Y, m_DropOffLocation.Z);

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = jointAngles.WristTiltAngle;

			this.SSC32Board.RunCommand(moveCommand);
		}

		private void OpenGripper()
		{
			Debug.WriteLine("Opening gripper.");
			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);
			moveCommand[ChannelId.Gripper] = 20;
			m_SSC32Board.RunCommand(moveCommand);
		}

		private JointAngles GetJointAngles(double x, double y, double z)
		{
			z = z + RobotArm.EndEffectorLength;

			Vector3 wristLocation = new Vector3(x, y, z);
			JointAngles jointAngles = RobotArm.DoInverseKinematics(wristLocation);

			jointAngles.WristTiltAngle = GetWristTiltAngleForVertical(jointAngles);
			return jointAngles;
		}

		private double GetWristTiltAngleForVertical(JointAngles jointAngles)
		{
			return -90 + jointAngles.ShoulderAngle - jointAngles.ElbowAngle;
		}

		internal void OnExit()
		{
			if (m_SSC32Board != null)
			{
				m_SSC32Board.Dispose();
			}
		}
	}
}
