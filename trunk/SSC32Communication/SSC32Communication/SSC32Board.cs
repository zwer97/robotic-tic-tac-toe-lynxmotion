using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SSC32Communication
{
	public class SSC32Board : IDisposable
	{
		private SerialPort m_SerialPort;
		private CommandRunner m_CommandRunner;

		public SSC32Board(string portName)
		{
			m_SerialPort = new System.IO.Ports.SerialPort();

			m_SerialPort.PortName = portName;
			m_SerialPort.BaudRate = 115200;
			m_SerialPort.Parity = System.IO.Ports.Parity.None;
			m_SerialPort.DataBits = 8;
			m_SerialPort.StopBits = System.IO.Ports.StopBits.One;

			m_SerialPort.Open();

			m_CommandRunner = new CommandRunner(m_SerialPort);
		}

		public void RunCommand(string command)
		{
			m_CommandRunner.RunCommand(command);
		}

		public void RunCommand(MoveRawCommand command)
		{
			m_CommandRunner.RunCommand(command);
		}

		public void RunCommand(MoveLogicalCommand command)
		{
			m_CommandRunner.RunCommand(command);
		}

		internal SerialPort SerialPort
		{
			get { return m_SerialPort; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_SerialPort != null)
			{
				m_SerialPort.Close();
			}
		}

		#endregion
	}
}
