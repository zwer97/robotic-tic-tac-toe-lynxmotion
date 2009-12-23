using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Diagnostics;

namespace SSC32Communication
{
	public class Servo
	{
		private const double c_MilliSecPerTic = 2.0;

		private int m_Id;
		private bool m_IsEnabled = false;
		private int m_MinPosMSecs = 500;
		private int m_MaxPosMSecs = 2500;

		private double m_Slope;
		private double m_YIntercept;

		internal Servo(int id)
			: this(id, false, 500, 2500, 2000.0/180.0, 1500) 
		{
		}

		internal Servo(
			int id,
			bool isEnabled,
			int minPosMSecs,
			int maxPosMSecs,
			double slope,
			double yIntercept)
		{
			m_Id = id;
			this.IsEnabled = isEnabled;
			this.MinPosMSecs = minPosMSecs;
			this.MaxPosMSecs = maxPosMSecs;
			this.Slope = slope;
			this.YIntercept = yIntercept;
		}

		internal void Initialize(
			bool isEnabled,
			int minPosMSecs,
			int maxPosMSecs,
			double slope,
			double yIntercept)
		{
			this.IsEnabled = isEnabled;
			this.MinPosMSecs = minPosMSecs;
			this.MaxPosMSecs = maxPosMSecs;
			this.Slope = slope;
			this.YIntercept = yIntercept;
		}

		public event EventHandler Changed;

		public string Name
		{
			get
			{
				string name = Enum.GetName(typeof(ChannelId), m_Id);
				if (String.IsNullOrEmpty(name))
				{
					name = m_Id.ToString(CultureInfo.InvariantCulture);
				}
				return name;
			}
		}

		public bool IsEnabled
		{
			get { return m_IsEnabled; }
			set
			{
				if (value == m_IsEnabled)
				{
					return;
				}
				m_IsEnabled = value;
				RaiseChangedEvent();
			}
		}

		public int MinPosMSecs
		{
			get { return m_MinPosMSecs; }
			set
			{
				if (value == m_MinPosMSecs)
				{
					return;
				}

				m_MinPosMSecs = value;
				RaiseChangedEvent();
			}
		}

		public int MaxPosMSecs
		{
			get { return m_MaxPosMSecs; }
			set
			{
				if (value == m_MaxPosMSecs)
				{
					return;
				}

				m_MaxPosMSecs = value;
				RaiseChangedEvent();
			}
		}

		public double Slope
		{
			get { return m_Slope; }
			set
			{
				if (value == m_Slope)
				{
					return;
				}

				m_Slope = value;
				RaiseChangedEvent();
			}
		}

		public double YIntercept
		{
			get { return m_YIntercept; }
			set
			{
				if (value == m_YIntercept)
				{
					return;
				}

				m_YIntercept = value;
				RaiseChangedEvent();
			}
		}

		/// <summary>
		/// Returns pulse width in millisecs
		/// </summary>
		/// <param name="angleDeg"></param>
		/// <returns></returns>
		public int MilliSecForAngleDeg(double angleDeg)
		{
			int milliSecs = (int)(m_Slope * angleDeg + m_YIntercept);
			CheckMilliSecs(milliSecs);

			return milliSecs;
		}

		private void CheckMilliSecs(int milliSecs)
		{
			if (milliSecs < m_MinPosMSecs ||
				milliSecs > m_MaxPosMSecs)
			{
				throw new ArgumentException("Specified angle falls outside the accessible range", "angleDeg");
			}
		}

		public void Move(double toAngle, int speed, SSC32Board board)
		{
			if (!this.IsEnabled)
			{
				return;
			}
			int milliSecs = MilliSecForAngleDeg(toAngle);

			string command = String.Format(
				CultureInfo.InvariantCulture,
				"#{0} P{1} S{2}\r",
				m_Id, milliSecs, speed);

			Debug.WriteLine(command);
			board.RunCommand(command);
		}

		public void Move(int milliSecs, int speed, SSC32Board board)
		{
			if (!this.IsEnabled)
			{
				return;
			}
			CheckMilliSecs(milliSecs);
			string command = String.Format(
				CultureInfo.InvariantCulture,
				"#{0} P{1} S{2}\r",
				m_Id, milliSecs, speed);

			Debug.WriteLine(command);
			board.RunCommand(command);
		}

		public override string ToString()
		{
			return String.Format(
				CultureInfo.InvariantCulture,
				"IsEnabled={0}, MinPos={1}, MaxPos={2}, Slope={3}, YIntercept={4}",
				this.IsEnabled, this.MinPosMSecs, this.MaxPosMSecs, this.Slope, this.YIntercept);
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
