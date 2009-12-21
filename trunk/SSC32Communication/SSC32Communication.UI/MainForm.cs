using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SSC32Communication.UI.Properties;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;

namespace SSC32Communication.UI
{
	public partial class MainForm : Form
	{
		private SSC32Board m_Board;
		private Servos m_Servos;
		private List<ServoControl> m_ServoControls = new List<ServoControl>();

		public MainForm()
		{
			InitializeComponent();
			m_Servos = new Servos();
			foreach (Control control in this.Controls)
			{
				ServoControl servoControl = control as ServoControl;
				if (servoControl != null)
				{
					m_ServoControls.Add(servoControl);
				}
			}
		}

		internal void Initialize(SSC32Board board)
		{
			m_Board = board;
			m_ServoControl0.InitializeModel(new ServoControlModel(m_Servos[0], board));
			m_ServoControl1.InitializeModel(new ServoControlModel(m_Servos[1], board));
			m_ServoControl2.InitializeModel(new ServoControlModel(m_Servos[2], board));
			m_ServoControl3.InitializeModel(new ServoControlModel(m_Servos[3], board));
			m_ServoControl4.InitializeModel(new ServoControlModel(m_Servos[5], board));
			m_ServoControl5.InitializeModel(new ServoControlModel(m_Servos[4], board));
			//m_ServoControl6.InitializeModel(new ServoControlModel(m_Servos[6], board));
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

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

			StringCollection servoPositionStrings = Settings.Default.ServoPositions;
			if (servoPositionStrings != null && servoPositionStrings.Count != 0)
			{
				for (int i = 0; i < m_ServoControls.Count; i++)
				{
					ServoControl servoControl = m_ServoControls[i];
					int servoPosition = (int)Convert.ToInt32(servoPositionStrings[i], CultureInfo.InvariantCulture);

					servoControl.Model.Position = servoPosition;
				}
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);

			//// If the MainForm is not minimized, save the current MainFormSize and 
			//// location to the settings file.  Otherwise, the existing values 
			//// in the settings file will not change...
			//if (this.WindowState != FormWindowState.Minimized)
			//{
			//    Settings.Instance.MainFormSize = this.Size;
			//    Settings.Instance.MainFormLocation = this.Location;
			//}

			//Settings.Instance.MainSplitterDistance = m_MainSplitContainer.SplitterDistance;

			Settings.Default.ServoPositions = new StringCollection();
			for (int i = 0; i < m_ServoControls.Count; i++)
			{
				ServoControl servoControl = m_ServoControls[i];
				Settings.Default.ServoPositions.Add(servoControl.Model.Position.ToString(CultureInfo.InvariantCulture));
			}

			Settings.Default.Save();
		}

		private void OnLoadCalibration(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			if (!String.IsNullOrEmpty(Settings.Default.CalibrationFile))
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(Settings.Default.CalibrationFile);
			}
			//openFileDialog.InitialDirectory = "c:\\";
			openFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = true;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.CalibrationFile = openFileDialog.FileName;
				try
				{
					CalibrationFile.Read(openFileDialog.FileName, m_Servos);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
				}
			}
		}

		private void OnSaveCalibration(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			if (!String.IsNullOrEmpty(Settings.Default.CalibrationFile))
			{
				saveFileDialog.InitialDirectory = Path.GetDirectoryName(Settings.Default.CalibrationFile);
			}
			//openFileDialog.InitialDirectory = "c:\\";
			saveFileDialog.Filter = "cfg files (*.cfg)|*.cfg|All files (*.*)|*.*";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = true;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.CalibrationFile = saveFileDialog.FileName;
				try
				{
					CalibrationFile.Write(saveFileDialog.FileName, m_Servos);
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error: Could not write file to disk. Original error: " + ex.Message);
				}
			}
		}
	}
}
