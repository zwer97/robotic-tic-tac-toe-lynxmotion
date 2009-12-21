using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC32Communication
{
	public class Servos
	{
		internal const int c_ServosCount = 8;

		private Servo[] m_Servos;

		public Servos()
		{
			m_Servos = new Servo[8];
			for (int servoIndex = 0; servoIndex < Servos.c_ServosCount; servoIndex++)
			{
				m_Servos[servoIndex] = new Servo(servoIndex);
			}
		}

		public void ConfigureFromFile(string configFilePath)
		{
			SSC32ConfigFile.ConfigureServosFromFile(configFilePath, m_Servos);
		}

		public Servo BaseServo
		{
			get { return m_Servos[(int)ChannelId.Base]; }
		}

		public Servo ShoulderServo
		{
			get { return m_Servos[(int)ChannelId.Shoulder]; }
		}

		public Servo ElbowServo
		{
			get { return m_Servos[(int)ChannelId.Elbow]; }
		}

		public Servo WristUpDownServo
		{
			get { return m_Servos[(int)ChannelId.WristUpDown]; }
		}

		public Servo WristTurnServo
		{
			get { return m_Servos[(int)ChannelId.WristTurn]; }
		}

		public Servo GripperServo
		{
			get { return m_Servos[(int)ChannelId.Gripper]; }
		}

		public Servo this[int servoIndex]
		{
			get { return m_Servos[servoIndex]; }
		}

		public Servo this[ChannelId channelId]
		{
			get { return this[(int)channelId]; }
		}

		public int Count
		{
			get { return m_Servos.Length; }
		}
	}
}
