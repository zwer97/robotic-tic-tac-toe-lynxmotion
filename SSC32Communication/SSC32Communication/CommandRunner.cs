using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace SSC32Communication
{
	internal class CommandRunner
	{
		private SerialPort m_SerialPort;

		internal CommandRunner(SerialPort serialPort)
		{
			m_SerialPort = serialPort;
		}

		internal void RunCommand(string command)
		{
			m_SerialPort.Write(command);
			WaitForCommandCompletion();
		}

		internal void RunCommand(MoveRawCommand command)
		{
			this.RunCommand(command.GetCommandText());
		}

		public void RunCommand(MoveLogicalCommand command)
		{
			this.RunCommand(command.GetRawCommand());
		}

		private void WaitForCommandCompletion()
		{
			while (!CommandCompleted())
			{
				//Console.WriteLine("Waiting ...");
				Thread.Sleep(TimeSpan.FromMilliseconds(10));
			}
			return;
		}

		private bool CommandCompleted()
		{
			m_SerialPort.Write("Q\r");
			char result = (char)m_SerialPort.ReadChar();
			Console.WriteLine(result);

			if (result == '.')
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
