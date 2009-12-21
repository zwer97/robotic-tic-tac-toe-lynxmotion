using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SSC32Communication.UI
{
	public partial class ServoControl : UserControl
	{
		internal ServoControlModel Model { get; private set; }

		public ServoControl()
		{
			InitializeComponent();
		}

		internal ServoControl(ServoControlModel model)
			: this()
		{
			InitializeModel(model);
		}

		internal void InitializeModel(ServoControlModel model)
		{
			this.Model = model;
			m_NameLabel.Text = this.Model.Servo.Name;
			this.Model.Servo.IsEnabled = false;
			UpdateFromModel();
			this.Model.Changed += new EventHandler(OnModelChanged);
		}

		private void OnModelChanged(object sender, EventArgs e)
		{
			UpdateFromModel();
		}

		private void UpdateFromModel()
		{
			m_EnabledCheckBox.Checked = this.Model.Servo.IsEnabled;
			m_MaxUpDown.Value = this.Model.Servo.MaxPosMSecs;
			m_MinUpDown.Value = this.Model.Servo.MinPosMSecs;
			m_PositionUpDown.Value = this.Model.Position;
			m_TicksTrackBar.Value = this.Model.Position;
		}

		private void OnTrackbarValueChanged(object sender, EventArgs e)
		{
			this.Model.Position = m_TicksTrackBar.Value;
		}

		private void OnNumericUpDownValueChanged(object sender, EventArgs e)
		{
			this.Model.Position = (int)m_PositionUpDown.Value;
		}

		private void OnEnabledCheckedChanged(object sender, EventArgs e)
		{
			this.Model.Servo.IsEnabled = m_EnabledCheckBox.Checked;
		}

		private void OnMaxUpDownChanged(object sender, EventArgs e)
		{
			this.Model.Servo.MaxPosMSecs = (int)m_MaxUpDown.Value;
		}

		private void OnMinUpDownChanged(object sender, EventArgs e)
		{
			this.Model.Servo.MinPosMSecs = (int)m_MinUpDown.Value;
		}
	}
}
