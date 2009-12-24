using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SSC32Communication;
using Robot.Kinematics;
using TicTacToe.Robot.Properties;
using System.Windows.Forms;
using System.Diagnostics;

namespace TicTacToe.Robot.Model
{
	internal class RobotDriver : IDisposable
	{
		private const double c_OpenGripperPosition = 20;
		private const double c_ClosedGripperPosition = 8;
		private const double c_UpperHeight = 30;
		internal const double ZeroHeight = -3;

		private SSC32Board m_SSC32Board;
		private Servos m_Servos;

		private Vector3 m_HomeLocation = new Vector3(0, -120, ZeroHeight);

		private bool m_IsBusy = false;
		internal object SyncObject = new object();

		public RobotDriver()
		{
			m_Servos = new Servos();
			RestoreRobotCalibration();
			RestoreCameraCalibration();

			MoveRawCommand.DefaultSpeed = 300;
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
					global::Vision.CameraCalibration.Instance.InitializeFromFile(Settings.Default.CameraCalibrationFile);
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

		internal void LoadRobotCalibration(string calibrationFileName)
		{
			SSC32Communication.CalibrationFile.Read(calibrationFileName, m_Servos);
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

		private Vector3 m_PickupLocation, m_TargetLocation;

		internal void MakeMove(Vector3 pickupLocation, Vector3 targetLocation)
		{
			this.IsBusy = true;

			m_PickupLocation = pickupLocation;
			m_TargetLocation = targetLocation;

			Action action = new Action(BeginMove);
			action.BeginInvoke(new AsyncCallback(OnMoveComplete), null);
		}

		private void BeginMove()
		{
			MoveAbove(m_HomeLocation, "Moving above home location");

			// pick up the piece
			MoveAbove(m_PickupLocation, "Moving above pickup location.");
			OpenGripper();
			MoveTo(m_PickupLocation, "Moving to pickup location.");
			CloseGripper();
			MoveAbove(m_PickupLocation, "Moving above pickup location.");

			// set the piece on the board
			MoveAbove(m_TargetLocation, "Moving above target location.");
			MoveTo(m_TargetLocation, "Moving to target location.");
			OpenGripper();
			MoveAbove(m_TargetLocation, "Moving above target location.");

			// move back home
			MoveAbove(m_HomeLocation, "Moving above home location");
			MoveTo(m_HomeLocation, "Moving to home location");
		}

		private void OnMoveComplete(IAsyncResult result)
		{
			this.IsBusy = false;
		}

		private void MoveAbove(Vector3 location, string message)
		{
			if (!String.IsNullOrEmpty(message))
			{
				Debug.WriteLine(message);
			}

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(location.X, location.Y, location.Z + c_UpperHeight);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = jointAngles.WristTiltAngle;

			this.SSC32Board.RunCommand(moveCommand);
		}

		private void MoveTo(Vector3 location, string message)
		{
			if (!String.IsNullOrEmpty(message))
			{
				Debug.WriteLine(message);
			}

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(location.X, location.Y, location.Z);
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

		#region IDisposable Members

		public void Dispose()
		{
			if (m_SSC32Board != null)
			{
				m_SSC32Board.Dispose();
			}
		}

		#endregion
	}
}
