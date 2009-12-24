using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC32Communication.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				string portName = SerialPortHelper.FindProlificPortName();
				using (SSC32Board board = new SSC32Board(portName))
				{
					Servos servos = new Servos();
					servos.ConfigureFromFile("ConfigSSC32.cfg");

					Servo baseServo = servos.BaseServo;
					System.Console.WriteLine(baseServo);

					baseServo.Move(-30, 100, board);
					baseServo.Move(30, 100, board);
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.ToString());
			}
			finally
			{
				System.Console.WriteLine("Hit <Enter> to end.");
				System.Console.ReadLine();
			}
		}
	}
}
