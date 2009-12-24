using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using qf4net;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using TicTacToe.Robot.Model;
using Emgu.CV.Structure;
using Emgu.CV;
using TicTacToe.Logic;
using Robot.Kinematics;
using System.Drawing;

namespace TicTacToe.Robot.StateMachine
{
	internal class ControllerHsm : QHsm
	{
		private MainModel m_MainModel;

		public ControllerHsm(MainModel mainModel)
		{
			m_MainModel = mainModel;
		}

		/// <summary>
		/// Is called inside of the function Init to give the deriving class a chance to
		/// initialize the state machine.
		/// </summary>
		protected override void InitializeStateMachine()
		{
			Thread.CurrentThread.Name = "Controller";
			Debug.WriteLine("Initializing Controller");
			InitializeState(this.CatchAll); // initial transition
		}

		private QState CatchAll(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)QSignals.Init:
					InitializeState(this.InitializeBoard);
					return null;
				case (int)ControllerSignal.Reset:
					TransitionTo(this.InitializeBoard);
					return null;
			}
			return this.TopState;
		}

		private QState InitializeBoard(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)ControllerSignal.ProcessFrame:
					m_MainModel.ImageProcessor.DetectedBlobsImage = null;
					DetectBoardRectangles();
					return null;
				case (int)ControllerSignal.BoardInitialized:
					TransitionTo(this.Initialized);
					return null;
			}
			return this.CatchAll;
		}

		private void DetectBoardRectangles()
		{
			ImageProcessor imageProcessor = m_MainModel.ImageProcessor;
			Image<Gray, Byte> erodedGrayImage = imageProcessor.GrayImage.Erode(1);
			Image<Gray, Byte> erodedBlackAndWhite = erodedGrayImage.ThresholdBinaryInv(new Gray(imageProcessor.Threshold), new Gray(255)); m_MainModel.ImageProcessor.BlackAndWhiteImage.Erode(1);

			List<MCvBox2D> foundRectangles = BoardImageModel.CollectRectangles(erodedBlackAndWhite);
			m_MainModel.ImageProcessor.UpdateRectanglesImage(foundRectangles);

			if (foundRectangles.Count == m_MainModel.BoardImageModel.ExpectedRectangleCount)
			{
				m_MainModel.BoardImageModel.InitializeRectangles(foundRectangles);
				if (m_MainModel.BoardImageModel.IsEmpty(m_MainModel.ImageProcessor.BlackAndWhiteImage))
				{
					this.Dispatch(new ControllerEvent(ControllerSignal.BoardInitialized));
				}
			}
		}

		private QState Initialized(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)QSignals.Init:
					InitializeState(this.WaitingForHuman);
					return null;
			}
			return this.CatchAll;
		}

		private QState WaitingForHuman(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)ControllerSignal.ProcessFrame:
					DetectHumanMove();
					return null;
				case (int)ControllerSignal.HumanMoved:
					if (m_MainModel.BoardManager.Board.GameIsOver)
					{
						HandleGameOver();
					}
					else
					{
						TransitionTo(this.RobotsTurn);
					}
					return null;
			}
			return this.Initialized;
		}

		private void DetectHumanMove()
		{
			BoardImageModel boardImageModel = m_MainModel.BoardImageModel;

			for (int x = 0; x < 3; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					if (m_MainModel.BoardManager.Board[x, y] == CellState.Empty)
					{
						if (boardImageModel.IsFilled(x, y, m_MainModel.ImageProcessor.BlackAndWhiteImage))
						{
							// We found the new piece
							m_MainModel.BoardManager.MakeMove(x, y);
							this.Dispatch(new ControllerEvent(ControllerSignal.HumanMoved));
						}
					}
				}
			}
		}

		private void HandleGameOver()
		{
			GameState gameState = m_MainModel.BoardManager.Board.GetGameState();
			if (gameState == GameState.Draw)
			{
				TransitionTo(this.Draw);
			}
			else if (gameState == GameState.Player1Won)
			{
				TransitionTo(this.HumanWon);
			}
			else if (gameState == GameState.Player2Won)
			{
				TransitionTo(this.RobotWon);
			}
			else
			{
				throw new Exception("We should never get here");
			}
		}

		private Location m_BestMove;
		private BlobInfo m_PickupPieceInfo;

		private QState RobotsTurn(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)QSignals.Entry:
					int bestMoveIndex = m_MainModel.BoardManager.GetBestMove();
					m_BestMove = new Location(bestMoveIndex);
					m_MainModel.NextMove = m_BestMove.ToString();
					return null;
				case (int)ControllerSignal.ProcessFrame:
					this.Dispatch(new ControllerEvent(ControllerSignal.FindUnusedPiece));
					return null;
				case (int)ControllerSignal.FindUnusedPiece:
					FindUnusedRobotPieces();

					if (m_PickupPieceInfo == null)
					{
						TransitionTo(this.NoPieceFound);
					}
					else
					{
						TransitionTo(this.MovingPiece);
					}
					return null;
				case (int)ControllerSignal.RobotMoveComplete:
					//Check that the move was successful
					if (m_MainModel.BoardImageModel.IsFilled(m_BestMove.X, m_BestMove.Y, m_MainModel.ImageProcessor.BlackAndWhiteImage))
					{
						// the move succeeded
						m_MainModel.BoardManager.MakeMove(m_BestMove.X, m_BestMove.Y);
						if (m_MainModel.BoardManager.Board.GameIsOver)
						{
							HandleGameOver();
						}
						else
						{
							TransitionTo(this.WaitingForHuman);
						}
					}
					else
					{
						// try again
						this.Dispatch(new ControllerEvent(ControllerSignal.FindUnusedPiece));
					}
					return null;
				case (int)QSignals.Exit:
					m_PickupPieceInfo = null;
					return null;
			}
			return this.Initialized;
		}

		private void FindUnusedRobotPieces()
		{
			ImageProcessor imageProcessor = m_MainModel.ImageProcessor;
			List<BlobInfo> blobInfos = m_MainModel.BoardImageModel.FindUnusedRobotPieces(imageProcessor.BlackAndWhiteImage);
			imageProcessor.DrawUnusedRobotPieces(blobInfos);

			if (blobInfos.Count > 0)
			{
				m_PickupPieceInfo = blobInfos[0];
			}
		}

		private QState NoPieceFound(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)ControllerSignal.ProcessFrame:
					FindUnusedRobotPieces();
					if (m_PickupPieceInfo != null)
					{
						TransitionTo(this.MovingPiece);
					}

					return null;
			}
			return this.RobotsTurn;
		}

		private QState MovingPiece(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			switch (qEvent.QSignal)
			{
				case (int)QSignals.Entry:
					StartRobotMove();
					return null;
				case (int)ControllerSignal.ProcessFrame:
					if (!m_MainModel.RobotDriver.IsBusy)
					{
						this.Dispatch(new ControllerEvent(ControllerSignal.RobotMoveComplete));
					}
					return null;
			}
			return this.RobotsTurn;
		}

		private void StartRobotMove()
		{
			Vector3 pickupLocation = new Vector3(m_PickupPieceInfo.PhysicalCenter.X, m_PickupPieceInfo.PhysicalCenter.Y, RobotDriver.ZeroHeight);
			PointF cameraTargetCenter = m_MainModel.BoardImageModel.InsideRectangles[m_BestMove.X, m_BestMove.Y].center;
			PointF physicalTargetCenter = global::Vision.CameraCalibration.Instance.GetPhysicalPoint(cameraTargetCenter);
			Vector3 targetLocation = new Vector3(physicalTargetCenter.X, physicalTargetCenter.Y, RobotDriver.ZeroHeight);

			m_MainModel.RobotDriver.MakeMove(pickupLocation, targetLocation);
		}

		private QState GameOver(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			return this.CatchAll;
		}

		private QState HumanWon(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			return this.GameOver;
		}

		private QState RobotWon(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			return this.GameOver;
		}

		private QState Draw(IQEvent qEvent)
		{
			LogEvent(MethodBase.GetCurrentMethod().Name, qEvent);
			return this.GameOver;
		}

		private void LogEvent(string stateName, IQEvent qEvent)
		{
			if (stateName == "Top")
			{
				Debug.WriteLine("Why Top?");
			}
			string message;
			switch (qEvent.QSignal)
			{
				case (int)QSignals.Entry:
					message = String.Format("{0} - Entering", stateName);
					break;
				case (int)QSignals.Exit:
					message = String.Format("{0} - Exiting", stateName);
					break;
				case (int)QSignals.Init:
					message = String.Format("{0} - Init", stateName);
					break;
				case (int)QSignals.Empty:
					return;
				default:
					message = String.Format("{0} - Handling event {1}", stateName, qEvent.ToString());
					break;
			}
			//Console.WriteLine(message);
			Debug.WriteLine(message);
		}
	}
}
