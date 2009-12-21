using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;

namespace SSC32Communication.Playground
{
	class Program
	{

		static void Main(string[] args)
		{
			string serialPortName = SerialPortHelper.FindProlificPortName();
			//Console.WriteLine("Available Ports:");
			//foreach (string s in SerialPort.GetPortNames())
			//{
			//    Console.WriteLine("   {0}", s);
			//}


			
			SerialPort serialPort = new SerialPort();

			// Close the serial port if it is already open
			if (serialPort.IsOpen)
			{
				serialPort.Close();
			}
			try
			{
				// Configure our serial port *** You'll likely need to change these for your config! ***
				serialPort.PortName = serialPortName;
				serialPort.BaudRate = 115200;
				serialPort.Parity = System.IO.Ports.Parity.None;
				serialPort.DataBits = 8;
				serialPort.StopBits = System.IO.Ports.StopBits.One;

				//Now open the serial port
				serialPort.Open();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Couldn't open the Serial Port!");
				Console.WriteLine(ex.ToString());//Report the actual error
			}
			try
			{
				//Now we'll send some data. Specifically we want to send "#Channel Number P1500<cr>" to center the servo
				serialPort.Write("#0 P1400 S100\r");
				WaitForCommandCompletion(serialPort);
				serialPort.Write("#0 P1500 S100\r");
				WaitForCommandCompletion(serialPort);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Couldn't send the command for some reason... I give up. Sorry!");
				Console.WriteLine(ex.ToString()); //Report the actual error
			}

			//Report our apparent success
			Console.WriteLine("Center little servo, Center!");
			Console.WriteLine("Sent: #0 P1500 T1000<cr>");

			//Now let's close our serial port.
			try
			{
				serialPort.Close();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Couldn't close the darned serial port. Here's what it said: ");
				Console.WriteLine(ex.ToString());
			}
			//Wait for [Enter] before we close
			Console.ReadLine();

		}

		private static void WaitForCommandCompletion(SerialPort serialPort)
		{
			while (!CommandCompleted(serialPort))
			{
				Console.WriteLine("Waiting ...");
				Thread.Sleep(TimeSpan.FromMilliseconds(10));
			}
			return;
		}

		private static bool CommandCompleted(SerialPort serialPort)
		{
			serialPort.Write("Q\r");
			char result = (char)serialPort.ReadChar();
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