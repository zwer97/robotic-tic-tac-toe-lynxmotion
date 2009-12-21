using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics
{
	public class Vector4
	{
		public double V0, V1, V2, V3;

		public Vector4()
		{
		}

		public Vector4(double v0, double v1, double v2, double v3)
		{
			V0 = v0;
			V1 = v1;
			V2 = v2;
			V3 = v3;
		}

		public Vector4(double x, double y, double z)
		{
			V0 = x;
			V1 = y;
			V2 = z;
			V3 = 1.0;
		}

		public Vector4(Vector3 vector)
			: this(vector.X, vector.Y, vector.Z)
		{
		}

		public override string ToString()
		{
			return String.Format("{0}, {1}, {2}, {3}", V0, V1, V2, V3);
		}
	}
}
