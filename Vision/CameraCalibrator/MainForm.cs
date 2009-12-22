using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using CameraCalibrator.Properties;
using Vision;

namespace CameraCalibrator
{
	public partial class MainForm : Form
	{
		private MainFormModel m_MainFormModel = new MainFormModel();

		public MainForm()
		{
			InitializeComponent();
			LayoutImageControls();

			m_MainFormModel.Changed += new EventHandler(OnModelChanged);
			Application.Idle += ProcessFrame;
		}

		private void OnModelChanged(object sender, EventArgs e)
		{
			m_ExpectedRectanglesTextBox.Text = m_MainFormModel.ExpectedRectangleCount.ToString();
			m_DetectedRectanglesTextBox.Text = m_MainFormModel.FoundRectangleCount.ToString();

			m_CaptureButton.Enabled = m_MainFormModel.CaptureIsPossible;
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
				m_MainFormModel.RegionOfInterest.Width + 5,
				m_MainFormModel.RegionOfInterest.Height + 25);

			m_ImageBoxWithHeading_Original.Size = size;

			m_ImageBoxWithHeading_BW.Size = size;
			m_ImageBoxWithHeading_BW.Left = m_ImageBoxWithHeading_Original.Left + size.Width;

			m_ImageBoxWithHeading_Rectangles.Size = size;
			m_ImageBoxWithHeading_Rectangles.Left = m_ImageBoxWithHeading_BW.Left + size.Width;
		}

		private void ProcessFrame(object sender, EventArgs e)
		{
			m_MainFormModel.ProcessFrame((int)m_ThresholdUpDown.Value);

			m_ImageBoxWithHeading_Original.ImageBox.Image = m_MainFormModel.ClippedImage;
			m_ImageBoxWithHeading_BW.ImageBox.Image = m_MainFormModel.BlackAndWhiteImage;
			m_ImageBoxWithHeading_Rectangles.ImageBox.Image = m_MainFormModel.FoundRectanglesImage;
		}

		private void OnCaptureButtonClicked(object sender, EventArgs e)
		{
			m_MainFormModel.CaptureCalibration();
		}

		private void OnMenuSaveClicked(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			if (!String.IsNullOrEmpty(Settings.Default.CalibrationFile))
			{
				saveFileDialog.InitialDirectory = Path.GetDirectoryName(Settings.Default.CalibrationFile);
			}
			saveFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.CalibrationFile = saveFileDialog.FileName;
				try
				{
					string calibrationData = CameraCalibration.Instance.Serialize();
					using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
					{
						writer.Write(calibrationData);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
				}
			}
		}
	}
}
