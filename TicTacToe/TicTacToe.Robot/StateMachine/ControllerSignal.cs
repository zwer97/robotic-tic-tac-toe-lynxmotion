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
		MaxSignal					// Keep this signal always last
	}
}
