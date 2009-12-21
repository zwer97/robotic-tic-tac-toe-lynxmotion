using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace SSC32Communication
{
	public class MoveRawCommand
	{
		public static int DefaultSpeed = 200;
 
		private int?[] m_ServosMilliSecs = new int?[Servos.c_ServosCount];
		private int m_Speed;

		public MoveRawCommand()
		{
		}

		public int? this[ChannelId channelId]
		{
			get { return this[(int)channelId]; }
			set { this[(int)channelId] = value; }
		}

		public int? this[int index]
		{
			get { return m_ServosMilliSecs[index]; }
			set { m_ServosMilliSecs[index] = value; }
		}

		public int Speed
		{
			get
			{
				if (m_Speed <= 0)
				{
					return DefaultSpeed;
				}
				else
				{
					return m_Speed;
				}
			}
			set
			{
				m_Speed = value;
			}
		}

		public string GetCommandText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < m_ServosMilliSecs.Length; i++)
			{
				int? servoMilliSecs = m_ServosMilliSecs[i];
				if (servoMilliSecs != null)
				{
					stringBuilder.AppendFormat(
						CultureInfo.InvariantCulture,
						"#{0} P{1} S{2} ",
						i, servoMilliSecs, this.Speed);
				}
			}
			stringBuilder.Append('\r');
			return stringBuilder.ToString();
		}
	}
}
