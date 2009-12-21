using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Robot.Kinematics
{
	public struct JointAngles
	{
		public double BaseAngle;
		public double ShoulderAngle;
		public double ElbowAngle;
		public double WristTiltAngle;

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("BaseAngle: "); stringBuilder.AppendLine(BaseAngle.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append("ShoulderAngle: "); stringBuilder.AppendLine(ShoulderAngle.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append("ElbowAngle: "); stringBuilder.AppendLine(ElbowAngle.ToString(CultureInfo.InvariantCulture));
			stringBuilder.Append("WristTiltAngle: "); stringBuilder.AppendLine(WristTiltAngle.ToString(CultureInfo.InvariantCulture));

			return stringBuilder.ToString();
		}
	}
}
