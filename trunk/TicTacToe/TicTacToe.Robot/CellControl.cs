using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using TicTacToe.Logic;

namespace TicTacToe.Robot
{
	public partial class CellControl : UserControl
	{
		private const int c_BorderWidth = 3;

		private Bitmap m_BackBuffer; //Used for double-buffering
		private int m_X, m_Y;
		private CellState m_CellState = CellState.Empty;

		public CellControl(int x, int y)
		{
			m_X = x;
			m_Y = y;
			InitializeComponent();

			m_BackBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			this.BackColor = SystemColors.Window;
		}

		public int X
		{
			get { return m_X; }
		}

		public int Y
		{
			get { return m_Y; }
		}

		public CellState CellState
		{
			get { return m_CellState; }
			set
			{
				if (value != m_CellState)
				{
					m_CellState = value;
					this.Invalidate();
				}
			}
		}

		protected override void OnResize(EventArgs ea)
		{
			if (this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
			{
				m_BackBuffer = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
			}
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			//Debug.WriteLine("CellControl.Paint");
			base.OnPaint(pea);

			//Use a back-buffer to reduce flicker
			Graphics buffer = Graphics.FromImage(m_BackBuffer);
			buffer.SmoothingMode = SmoothingMode.AntiAlias;

			buffer.Clear(this.BackColor);


			Pen pen;
			// draw border
			pen = new Pen(new SolidBrush(Color.Black), c_BorderWidth);
			buffer.DrawRectangle(pen, 0, 0, ClientSize.Width, ClientSize.Height);

			switch (m_CellState)
			{
				case CellState.Empty:
					break;
				case CellState.Player1:
					//draw an X
					pen = new Pen(new SolidBrush(Color.Red), 3);
					buffer.DrawLine(pen, c_BorderWidth, c_BorderWidth, ClientSize.Width - c_BorderWidth, ClientSize.Height - c_BorderWidth);
					buffer.DrawLine(pen, c_BorderWidth, ClientSize.Height - c_BorderWidth, ClientSize.Width - c_BorderWidth, c_BorderWidth);
					break;
				case CellState.Player2:
					//draw an O
					pen = new Pen(new SolidBrush(Color.Blue), 3);
					Rectangle rect = new Rectangle(c_BorderWidth + 2, c_BorderWidth + 2, ClientRectangle.Width - c_BorderWidth - 2, ClientRectangle.Height - c_BorderWidth - 2);
					buffer.DrawEllipse(pen, rect);
					break;
				default:
					break;
			}

			//Copy the back-buffer to the screen
			Graphics viewable = pea.Graphics;
			viewable.DrawImageUnscaled(m_BackBuffer, 0, 0);

			buffer.Dispose();
			viewable.Dispose();
		}
	}
}
