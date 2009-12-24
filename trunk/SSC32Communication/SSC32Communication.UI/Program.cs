using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SSC32Communication.UI
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			string portName = SerialPortHelper.FindProlificPortName();

			using (SSC32Board board = new SSC32Board(portName))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);

				MainForm mainForm = new MainForm();
				mainForm.Initialize(board);
				Application.Run(mainForm);
			}

		}
	}
}
