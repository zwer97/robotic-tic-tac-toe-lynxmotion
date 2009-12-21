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
		//internal static void Initialize(SerialPort serialPort)
		//{
		//    Instance = new CommandRunner(serialPort);
		//}

		//internal static CommandRunner Instance;

		private SerialPort m_SerialPort;
		//private Thread m_ExecutionThread;
		//private LinkedList<string> m_Commands = new LinkedList<string>();

		internal CommandRunner(SerialPort serialPort)
		{
			m_SerialPort = serialPort;

			//m_ExecutionThread = new Thread(new ThreadStart(this.DoEventLoop));
			//m_ExecutionThread.Start();
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

		///// <summary>
		///// This method is executed on the dedicated thread of this instance.
		///// </summary>
		//private void DoEventLoop()
		//{
		//    // Once initialized we kick off our event loop
		//    try
		//    {
		//        while (true)
		//        {
		//            string command = this.DeQueue(); // this blocks if there are no events in the queue
		//            ExecuteCommand(command);
		//        }
		//    }
		//    catch (ThreadAbortException)
		//    {
		//        // We use the method Thread.Abort() in this.Abort() to exit from the event loop
		//        Thread.ResetAbort();
		//    }

		//    m_ExecutionThread = null;
		//}

		//private void ExecuteCommand(string command)
		//{
		//    m_SerialPort.Write(command);
		//    WaitForCommandCompletion();
		//}

		//private void WaitForCommandCompletion()
		//{
		//    while (!CommandCompleted())
		//    {
		//        Thread.Sleep(TimeSpan.FromMilliseconds(10));
		//    }
		//    return;
		//}

		//private bool CommandCompleted()
		//{
		//    m_SerialPort.Write("Q\r");
		//    string result = m_SerialPort.ReadLine();

		//    if (result.Equals("."))
		//    {
		//        return true;
		//    }
		//    else
		//    {
		//        return false;
		//    }
		//}

		///// <summary>
		///// Returns <see langword="true"/> if the <see cref="CommandRunner"/> is empty
		///// </summary>
		//public bool IsEmpty
		//{
		//    get
		//    {
		//        lock (m_Commands)
		//        {
		//            return m_Commands.Count == 0;
		//        }
		//    }
		//}

		///// <summary>
		///// Number of events in the queue
		///// </summary>
		//public int Count
		//{
		//    get
		//    {
		//        lock (m_Commands)
		//        {
		//            return m_Commands.Count;
		//        }
		//    }
		//}

		///// <summary>
		///// Inserts the at the end of the queue (First In First Out). 
		///// </summary>
		//public void EnqueueFIFO(string command)
		//{
		//    lock (m_Commands)
		//    {
		//        m_Commands.AddLast(command);
		//        Monitor.Pulse(m_Commands);
		//    }
		//}

		///// <summary>
		///// Dequeues the first <see cref="QEvent"/> in the <see cref="QEventQueue"/>. If the <see cref="QEventQueue"/>
		///// is currently empty then it blocks until a new <see cref="QEvent"/> is put into the <see cref="QEventQueue"/>.
		///// </summary>
		///// <returns>The first command in the <see cref="CommandRunner"/>.</returns>
		//public string DeQueue()
		//{
		//    lock (m_Commands)
		//    {
		//        if (m_Commands.Count == 0)
		//        {
		//            // We wait for the next event to be put into the queue
		//            Monitor.Wait(m_Commands);
		//        }

		//        string command = m_Commands.First.Value;
		//        m_Commands.RemoveFirst();
		//        return command;
		//    }
		//}

		///// <summary>
		///// Allows the caller to peek at the head of the <see cref="CommandRunner"/>.
		///// </summary>
		///// <returns>The command at the head of the <see cref="CommandRunner"/> if it exists; 
		///// otherwise <see langword="null"/></returns>
		//public string Peek()
		//{
		//    lock (m_Commands)
		//    {
		//        if (m_Commands.Count == 0)
		//        {
		//            return null;
		//        }
		//        else
		//        {
		//            return m_Commands.First.Value;
		//        }
		//    }
		//}
	}
}
