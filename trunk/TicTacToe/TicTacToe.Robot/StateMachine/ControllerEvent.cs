using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using qf4net;

namespace TicTacToe.Robot.StateMachine
{
	internal class ControllerEvent : QEvent
	{
		internal ControllerEvent(ControllerSignal signal)
			: base((int)signal)
		{
		}

		internal ControllerSignal Signal
		{
			get
			{
				return (ControllerSignal)this.QSignal;
			}
		}

		/// <summary>
		/// The QSignal in string form. It allows for simpler debugging and logging. 
		/// </summary>
		/// <returns>The signal as string.</returns>
		public override string ToString()
		{
			if (((int)this.QSignal >= (int)QSignals.UserSig)
				&& ((int)this.QSignal < (int)ControllerSignal.MaxSignal))
			{
				return ((ControllerSignal)this.QSignal).ToString();
			}
			else
			{
				return base.ToString();
			}
		}
	}
}
