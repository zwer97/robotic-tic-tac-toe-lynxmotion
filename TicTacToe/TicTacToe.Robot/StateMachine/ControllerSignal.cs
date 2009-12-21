using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using qf4net;

namespace TicTacToe.Robot.StateMachine
{
	internal enum ControllerSignal
	{
		ProcessFrame = QSignals.UserSig,
		BoardInitialized,
		Reset,
		HumanMoved,
		FindUnusedPiece,
		RobotMoveComplete,
		TimeOut,					// Idle time expired
		ShutDown,					// Triggers the shut down of the execution of the active object
		MaxSignal					// Keep this signal always last
	}
}
