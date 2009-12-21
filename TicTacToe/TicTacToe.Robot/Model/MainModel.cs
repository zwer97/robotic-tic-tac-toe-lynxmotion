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
using TicTacToe.Robot.Properties;
using TicTacToe.Robot.StateMachine;
using TicTacToe.Logic;

namespace TicTacToe.Robot.Model
{
	internal class MainModel
	{
		internal static MainModel Instance;

		private ImageProcessor m_ImageProcessor;
		private ControllerHsm m_ControllerHsm;
		private BoardImageModel m_BoardImageModel = new BoardImageModel();
		public BoardManager BoardManager { get; private set; }
		public RobotDriver RobotDriver { get; private set; }

		public string NextMove { get; set; }

		public event EventHandler Changed;

		public MainModel()
		{
			m_ImageProcessor = new ImageProcessor();
			m_ControllerHsm = new ControllerHsm(this);
			m_ControllerHsm.Init();

			this.BoardManager = new BoardManager(false);

			this.RobotDriver = new RobotDriver();
			Instance = this;
		}

		internal ImageProcessor ImageProcessor
		{
			get { return m_ImageProcessor; }
		}

		internal BoardImageModel BoardImageModel
		{
			get { return m_BoardImageModel; }
		}

		public Rectangle RegionOfInterest
		{
			get { return m_ImageProcessor.RegionOfInterest; }
		}

		internal void LoadRobotCalibration(string calibrationFileName)
		{
			this.RobotDriver.LoadRobotCalibration(calibrationFileName);
		}

		public int Threshold
		{
			get { return m_ImageProcessor.Threshold; }
			set
			{
				if (value == m_ImageProcessor.Threshold)
				{
					return;
				}

				m_ImageProcessor.Threshold = value;
				RaiseChangedEvent();
			}
		}

		public void Reset()
		{
			this.BoardManager = new BoardManager(false);
			m_ControllerHsm.Dispatch(new ControllerEvent(ControllerSignal.Reset));
		}

		public void ProcessFrame()
		{
			m_ImageProcessor.ProcessFrame();
			if (m_ImageProcessor.ImageIsStable)
			{
				m_ControllerHsm.Dispatch(new ControllerEvent(ControllerSignal.ProcessFrame));
			}

			RaiseChangedEvent();
		}

		public string CurrentStateName
		{
			get { return m_ControllerHsm.CurrentStateName; }
		}

		private void RaiseChangedEvent()
		{
			EventHandler handler = this.Changed;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		internal void OnExit()
		{
			this.RobotDriver.Dispose();
		}
	}
}

