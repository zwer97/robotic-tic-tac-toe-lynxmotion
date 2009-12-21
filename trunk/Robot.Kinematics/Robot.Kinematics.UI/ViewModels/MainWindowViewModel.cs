using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using Microsoft.Win32;
using SSC32Communication;
using Robot.Kinematics.UI.Properties;
using Robot.Kinematics.UI.Commands;

namespace Robot.Kinematics.UI.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private Servos m_Servos;
		private RelayCommand m_LoadCalibrationCommand;
		private RelayCommand m_MoveToCommand;
		private RelayCommand m_PickUpCommand;
		private RelayCommand m_HomeCommand;
		private double m_X;
		private double m_Y;
		private double m_Z;
		private JointAngles m_JointAngles;
		private SSC32Board m_SSC32Board;

		public MainWindowViewModel()
		{
			m_Servos = new Servos();
			RestoreCalibration();

			this.X = Settings.Default.X;
			this.Y = Settings.Default.Y;
			this.Z = Settings.Default.Z;
			this.ZIsBasedOnEndeffectorTip = Settings.Default.ZIsBasedOnTip;

			if (this.X == 0 && this.Y == 0 && this.Z == 0)
			{
				this.X = RobotArm.ForearmLength;
				this.Y = 0;
				this.Z = RobotArm.ShoulderHeight + RobotArm.UpperArmLength;
				this.ZIsBasedOnEndeffectorTip = false;
			}

			MoveRawCommand.DefaultSpeed = 500;
		}

		private void RestoreCalibration()
		{
			if (!String.IsNullOrEmpty(Settings.Default.CalibrationFile))
			{
				try
				{
					CalibrationFile.Read(Settings.Default.CalibrationFile, m_Servos);
				}
				catch (Exception ex)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine("Error: Could not read calibration data from file:");
					stringBuilder.AppendLine(Settings.Default.CalibrationFile);
					stringBuilder.AppendLine("Original error:");
					stringBuilder.AppendLine(ex.Message);

					MessageBox.Show(stringBuilder.ToString());
				}
			}
		}

		/// <summary>
		/// Gets the load command.
		/// </summary>
		/// <value>The load command.</value>
		public ICommand LoadCalibrationCommand
		{
			get
			{
				if (m_LoadCalibrationCommand == null)
				{
					m_LoadCalibrationCommand = new RelayCommand(param => LoadCalibration());
				}

				return m_LoadCalibrationCommand;
			}
		}

		private void LoadCalibration()
		{
			Debug.WriteLine("In Load Calibration");
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;

			if (openFileDialog.ShowDialog() == true)
			{
				try
				{
					CalibrationFile.Read(openFileDialog.FileName, m_Servos);
					Settings.Default.CalibrationFile = openFileDialog.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		public double X
		{
			get
			{
				return m_X;
			}
			set
			{
				if (value == m_X)
				{
					return;
				}
				m_X = value;
				Recalculate();
				Settings.Default.X = value;
				OnPropertyChanged("X");
			}
		}

		public double Y
		{
			get
			{
				return m_Y;
			}
			set
			{
				if (value == m_Y)
				{
					return;
				}
				m_Y = value;
				Recalculate();
				Settings.Default.Y = value;
				OnPropertyChanged("Y");
			}
		}

		public double Z
		{
			get
			{
				return m_Z;
			}
			set
			{
				if (value == m_Z)
				{
					return;
				}
				m_Z = value;
				Recalculate();
				Settings.Default.Z = value;
				OnPropertyChanged("Z");
			}
		}

		private bool m_ZIsBasedOnEndeffectorTip;

		public bool ZIsBasedOnEndeffectorTip
		{
			get
			{
				return m_ZIsBasedOnEndeffectorTip;
			}
			set
			{
				if (value == m_ZIsBasedOnEndeffectorTip)
				{
					return;
				}
				m_ZIsBasedOnEndeffectorTip = value;
				Recalculate();
				Settings.Default.ZIsBasedOnTip = value;
				OnPropertyChanged("ZIsBasedOnEndeffectorTip");
			}
		}

		private void Recalculate()
		{
			m_JointAngles = GetJointAngles(m_X, m_Y, m_Z, this.ZIsBasedOnEndeffectorTip);

			OnPropertyChanged("BaseAnglePhi0");
			OnPropertyChanged("BaseAngleTheta0");
			OnPropertyChanged("BaseMilliSecs");
			OnPropertyChanged("ShoulderAnglePhi1");
			OnPropertyChanged("ShoulderAngleTheta1");
			OnPropertyChanged("ShoulderMilliSecs");
			OnPropertyChanged("ElbowAnglePhi2");
			OnPropertyChanged("ElbowAngleTheta2");
			OnPropertyChanged("ElbowMilliSecs");
		}

		private JointAngles GetJointAngles(double x, double y, double z, bool zIsBasedOnEndeffectorTip)
		{
			if (zIsBasedOnEndeffectorTip)
			{
				z = z + RobotArm.EndEffectorLength;
			}

			Vector3 wristLocation = new Vector3(x, y, z);
			JointAngles jointAngles = RobotArm.DoInverseKinematics(wristLocation);
			return jointAngles;
		}

		public double BaseAnglePhi0
		{
			get { return m_JointAngles.BaseAngle; }
		}

		public double BaseAngleTheta0
		{
			get { return this.BaseAnglePhi0; }
		}

		public int BaseMilliSecs
		{
			get
			{
				try
				{
					return m_Servos.BaseServo.MilliSecForAngleDeg(this.BaseAnglePhi0);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}
			}
		}

		public double ShoulderAnglePhi1
		{
			get { return m_JointAngles.ShoulderAngle; }
		}

		public double ShoulderAngleTheta1
		{
			get { return 90 - m_JointAngles.ShoulderAngle; }
		}

		public int ShoulderMilliSecs
		{
			get
			{
				try
				{
					return m_Servos.ShoulderServo.MilliSecForAngleDeg(this.ShoulderAnglePhi1);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}
			}
		}

		public double ElbowAnglePhi2
		{
			get { return m_JointAngles.ElbowAngle; }
		}

		public double ElbowAngleTheta2
		{
			get { return m_JointAngles.ElbowAngle - 90; }
		}

		public int ElbowMilliSecs
		{
			get
			{
				try
				{
					return m_Servos.ElbowServo.MilliSecForAngleDeg(this.ElbowAnglePhi2);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}
			}
		}

		public double WristTiltAnglePhi3
		{
			get { return GetWristTiltAngleForVertical(m_JointAngles); }
		}

		private double GetWristTiltAngleForVertical(JointAngles jointAngles)
		{
			return -90 + jointAngles.ShoulderAngle - jointAngles.ElbowAngle;
		}

		public int WristTiltMilliSecs
		{
			get
			{
				try
				{
					return m_Servos.WristUpDownServo.MilliSecForAngleDeg(this.WristTiltAnglePhi3);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
					throw;
				}
			}
		}

		/// <summary>
		/// Gets the move command.
		/// </summary>
		public ICommand MoveToCommand
		{
			get
			{
				if (m_MoveToCommand == null)
				{
					m_MoveToCommand = new RelayCommand(param => CanMove(), param => Move());
				}

				return m_MoveToCommand;
			}
		}

		private bool CanMove()
		{
			try
			{
				EnsureSSC32BoardIsInitialized();

				double baseMilliSecs = this.BaseMilliSecs;
				double shoulderMilliSecs = this.ShoulderMilliSecs;
				double elbowMilliSecs = this.ElbowMilliSecs;
				double wristTiltMilliSecs = this.WristTiltMilliSecs;

				return true;
			}
			catch
			{
				return false;
			}
		}

		private void EnsureSSC32BoardIsInitialized()
		{
			if (m_SSC32Board == null)
			{
				string portName = SerialPortHelper.FindProlificPortName();
				m_SSC32Board = new SSC32Board(portName);
			}
		}

		private void Move()
		{
			MoveRawCommand moveCommand = new MoveRawCommand();

			moveCommand[ChannelId.Base] = this.BaseMilliSecs;
			moveCommand[ChannelId.Shoulder] = this.ShoulderMilliSecs;
			moveCommand[ChannelId.Elbow] = this.ElbowMilliSecs;
			moveCommand[ChannelId.WristUpDown] = this.WristTiltMilliSecs;

			m_SSC32Board.RunCommand(moveCommand);
		}

		public ICommand PickUpCommand
		{
			get
			{
				if (m_PickUpCommand == null)
				{
					m_PickUpCommand = new RelayCommand(param => CanMove(), param => PickUp());
				}

				return m_PickUpCommand;
			}
		}

		private void PickUp()
		{
			MoveAbovePickupLocation();
			MoveToPickupLocation();
			CloseGripper();
			MoveAbovePickupLocation();
			MoveToDropTarget();
			OpenGripper();
			MoveToHomePosition();
		}

		private void MoveAbovePickupLocation()
		{
			Debug.WriteLine("Moving above pikup location.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(this.X, this.Y, this.Z + 25, this.ZIsBasedOnEndeffectorTip);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = GetWristTiltAngleForVertical(jointAngles);
			//moveCommand[ChannelId.Gripper] = 20;

			m_SSC32Board.RunCommand(moveCommand);
		}

		private void MoveToPickupLocation()
		{
			Debug.WriteLine("Moving to pikup location.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			JointAngles jointAngles = GetJointAngles(this.X, this.Y, this.Z, this.ZIsBasedOnEndeffectorTip);
			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = GetWristTiltAngleForVertical(jointAngles);
			//moveCommand[ChannelId.Gripper] = 20;

			m_SSC32Board.RunCommand(moveCommand);
		}

		private void CloseGripper()
		{
			Debug.WriteLine("Closing gripper.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);
			moveCommand[ChannelId.Gripper] = 8;
			m_SSC32Board.RunCommand(moveCommand);
		}

		private void MoveToDropTarget()
		{
			Debug.WriteLine("Moving to drop target.");
			Vector3 wristLocation = new Vector3(50, 150, 150);
			JointAngles jointAngles = GetJointAngles(50, 150, 40, true);

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			moveCommand[ChannelId.Base] = jointAngles.BaseAngle;
			moveCommand[ChannelId.Shoulder] = jointAngles.ShoulderAngle;
			moveCommand[ChannelId.Elbow] = jointAngles.ElbowAngle;
			moveCommand[ChannelId.WristUpDown] = GetWristTiltAngleForVertical(jointAngles);
			//moveCommand[ChannelId.Gripper] = 28;

			m_SSC32Board.RunCommand(moveCommand);
		}

		private void OpenGripper()
		{
			Debug.WriteLine("Opening gripper.");
			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);
			moveCommand[ChannelId.Gripper] = 20;
			m_SSC32Board.RunCommand(moveCommand);
		}

		public ICommand HomeCommand
		{
			get
			{
				if (m_HomeCommand == null)
				{
					m_HomeCommand = new RelayCommand(param => CanMove(), param => MoveToHomePosition());
				}

				return m_HomeCommand;
			}
		}

		private void MoveToHomePosition()
		{
			Debug.WriteLine("Moving to home position.");

			MoveLogicalCommand moveCommand = new MoveLogicalCommand(m_Servos);

			moveCommand[ChannelId.Base] = -90;
			moveCommand[ChannelId.Shoulder] = 0;
			moveCommand[ChannelId.Elbow] =0;
			moveCommand[ChannelId.WristUpDown] = -90;
			moveCommand[ChannelId.Gripper] = 20;

			m_SSC32Board.RunCommand(moveCommand);
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
