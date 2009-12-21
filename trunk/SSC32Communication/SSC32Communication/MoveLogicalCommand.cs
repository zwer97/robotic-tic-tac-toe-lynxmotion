using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC32Communication
{
	public class MoveLogicalCommand
	{
		private Servos m_Servos;
		private double?[] m_ServosPositions = new double?[Servos.c_ServosCount];
		private int m_Speed;

		public MoveLogicalCommand(Servos servos)
		{
			m_Servos = servos;
		}

		public double? this[ChannelId channelId]
		{
			get { return this[(int)channelId]; }
			set { this[(int)channelId] = value; }
		}

		public double? this[int index]
		{
			get { return m_ServosPositions[index]; }
			set { m_ServosPositions[index] = value; }
		}

		public int Speed
		{
			get
			{
				if (m_Speed <= 0)
				{
					return MoveRawCommand.DefaultSpeed;
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

		public MoveRawCommand GetRawCommand()
		{
			MoveRawCommand rawCommand = new MoveRawCommand();
			for (int i = 0; i < m_ServosPositions.Length; i++)
			{
				double? servoPosition = m_ServosPositions[i];
				if (servoPosition != null)
				{
					rawCommand[i] = m_Servos[i].MilliSecForAngleDeg(servoPosition.Value);
				}
			}
			rawCommand.Speed = this.Speed;
			return rawCommand;
		}

		public string GetCommandText()
		{
			return GetRawCommand().GetCommandText();
		}
	}
}
