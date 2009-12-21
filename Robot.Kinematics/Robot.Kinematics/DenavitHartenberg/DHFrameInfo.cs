using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics.DenavitHartenberg
{
	/// <summary>
	/// Denavit-Hartenberg Parameters describing one frame
	/// </summary>
	public class DHFrameInfo
	{
		private double m_A, m_Alpha, m_AlphaRadian, m_D, m_Theta, m_ThetaRadian;

		public DHFrameInfo(double a, double alpha, double d, double theta)
		{
			m_A = a;
			m_Alpha = alpha;
			m_AlphaRadian = alpha * Math.PI / 180;
			m_D = d;
			m_Theta = theta;
			m_ThetaRadian = theta * Math.PI / 180;;
		}

		/// <summary>
		/// Length
		/// </summary>
		public double A
		{
			get { return m_A; }
		}

		/// <summary>
		/// Twist angle
		/// </summary>
		public double Alpha
		{
			get { return m_Alpha; }
		}

		/// <summary>
		/// Twist angle in radians
		/// </summary>
		public double AlphaRadian
		{
			get { return m_AlphaRadian; }
		}

		/// <summary>
		/// Offset
		/// </summary>
		public double D
		{
			get { return m_D; }
		}

		/// <summary>
		/// theta
		/// </summary>
		public double Theta
		{
			get { return m_Theta; }
		}

		/// <summary>
		/// theta in radians
		/// </summary>
		public double ThetaRadian
		{
			get { return m_ThetaRadian; }
		}
	}
}
