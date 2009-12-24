using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using TicTacToe.Robot.Properties;
using TicTacToe.Robot.Model;

namespace TicTacToe.Robot
{
	public partial class MainForm : Form
	{
		private MainModel m_Model = new MainModel();

		public MainForm()
		{
			InitializeComponent();
			LayoutImageControls();

			m_Model.Changed += new EventHandler(OnModelChanged);
			Application.Idle += ProcessFrame;
		}

		private void OnModelChanged(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new Action(OnModelChanged));
			}
			OnModelChanged();
		}

		private void OnModelChanged()
		{
			m_ThresholdUpDown.Value = m_Model.Threshold;

			m_ImageBoxWithHeading_Original.ImageBox.Image = m_Model.ImageProcessor.ClippedImage;
			m_ImageBoxWithHeading_BW.ImageBox.Image = m_Model.ImageProcessor.BlackAndWhiteImage;
			m_ImageBoxWithHeading_Rectangles.ImageBox.Image = m_Model.ImageProcessor.DetectedRectanglesImage;

			if (m_Model.ImageProcessor.DetectedBlobsImage != null)
			{
				m_ImageBoxWithHeading_Blobs.ImageBox.Image = m_Model.ImageProcessor.DetectedBlobsImage;
			}

			m_CurrentStateTextBox.Text = m_Model.CurrentStateName;
			m_DeltaNonZeroTextBox.Text = m_Model.ImageProcessor.DeltaNonZero.ToString();

			m_BoardControl.UpdateGamePieces(m_Model.BoardManager.Board);
			m_NextMoveTextBox.Text = m_Model.NextMove;
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			Size formSize = Settings.Default.Size;
			if (formSize.Height != 0)
			{
				this.Size = formSize;
				this.Location = Settings.Default.Location;
				m_ThresholdUpDown.Value = Settings.Default.Threshold;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			m_Model.OnExit();

			// If the MainForm is not minimized, save the current Size and 
			// location to the settings file.  Otherwise, the existing values 
			// in the settings file will not change...
			if (this.WindowState != FormWindowState.Minimized)
			{
				Settings.Default.Size = this.Size;
				Settings.Default.Location = this.Location;
			}

			Settings.Default.Threshold = (int)m_ThresholdUpDown.Value;
			Settings.Default.Save();
		}

		private void LayoutImageControls()
		{
			Size size = new Size(
				m_Model.RegionOfInterest.Width + 5,
				m_Model.RegionOfInterest.Height + 25);

			m_ImageBoxWithHeading_Original.Size = size;

			m_ImageBoxWithHeading_BW.Size = size;
			m_ImageBoxWithHeading_BW.Left = m_ImageBoxWithHeading_Original.Left + size.Width;
			
			m_ImageBoxWithHeading_Rectangles.Size = size;
			m_ImageBoxWithHeading_Rectangles.Top = m_ImageBoxWithHeading_Original.Top + size.Height;

			m_ImageBoxWithHeading_Blobs.Size = size;
			m_ImageBoxWithHeading_Blobs.Left = m_ImageBoxWithHeading_Rectangles.Left + size.Width;
			m_ImageBoxWithHeading_Blobs.Top = m_ImageBoxWithHeading_Original.Top + size.Height;
		}

		private void OnMenuLoadCameraCalibrationClicked(object sender, EventArgs e)
		{
			LoadCameraCalibration();
		}

		private void LoadCameraCalibration()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					global::Vision.CameraCalibration.Instance.InitializeFromFile(openFileDialog.FileName);
					Settings.Default.CameraCalibrationFile = openFileDialog.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void OnMenuLoadRobotCalibrationClicked(object sender, EventArgs e)
		{
			LoadRobotCalibration();
		}

		private void LoadRobotCalibration()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					m_Model.LoadRobotCalibration(openFileDialog.FileName);
					Settings.Default.RobotCalibrationFile = openFileDialog.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void OnThresholdValueChanged(object sender, EventArgs e)
		{
			m_Model.Threshold = (int)m_ThresholdUpDown.Value;
		}

		private void ProcessFrame(object sender, EventArgs e)
		{
			m_Model.ProcessFrame();
		}

		private void OnResetButtonClicked(object sender, EventArgs e)
		{
			m_Model.Reset();
		}
	}
}
