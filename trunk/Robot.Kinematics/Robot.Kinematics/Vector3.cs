using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics
{
	public class Vector3
	{
		public double X, Y, Z;

		public Vector3()
		{
		}

		public Vector3(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public override string ToString()
		{
			return String.Format("{0}, {1}, {2}", X, Y, Z);
		}
	}
}
