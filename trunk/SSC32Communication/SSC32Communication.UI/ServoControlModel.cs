using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC32Communication.UI
{
	internal class ServoControlModel
	{
		private SSC32Board m_Board;
		//public const int MinPosition = 500;
		//public const int MaxPosition = 2500;
		private const int c_Speed = 100;

		private int m_Position;
		//private bool m_IsEnabled;

		public ServoControlModel(Servo servo, SSC32Board board)
		{
			this.Servo = servo;
			m_Board = board;
			m_Position = 1500;

			servo.Changed += new EventHandler(OnServoChanged);
		}

		private void OnServoChanged(object sender, EventArgs e)
		{
			RaiseChangedEvent();
		}

		public event EventHandler Changed;

		public Servo Servo { get; private set; }

		public int Position
		{
			get { return m_Position; }
			set
			{
				int oldValue = m_Position;

				if (value < this.Servo.MinPosMSecs)
				{
					m_Position = this.Servo.MinPosMSecs;
				}
				else if (value > this.Servo.MaxPosMSecs)
				{
					m_Position = this.Servo.MaxPosMSecs;
				}
				else
				{
					m_Position = value;
				}

				if (m_Position == oldValue)
				{
					return;
				}
				this.Servo.Move(m_Position, c_Speed, m_Board);
				RaiseChangedEvent();
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
