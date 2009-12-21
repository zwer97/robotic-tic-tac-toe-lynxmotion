using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics.DenavitHartenberg
{
	public class DHTransformation
	{
		public static Matrix4x4 ToPreviousFrame(DHFrameInfo frameInfo)
		{
			double a = frameInfo.A;
			double d = frameInfo.D;

			double cosTheta = Math.Cos(frameInfo.ThetaRadian);
			double sinTheta = Math.Sin(frameInfo.ThetaRadian);

			double cosAlpha = Math.Cos(frameInfo.AlphaRadian);
			double sinAlpha = Math.Sin(frameInfo.AlphaRadian);

			return new Matrix4x4(
				cosTheta, -sinTheta * cosAlpha, sinTheta * sinAlpha, a * cosTheta,
				sinTheta, cosTheta * cosAlpha, -cosTheta * sinAlpha, a * sinTheta,
				0, sinAlpha, cosAlpha, d,
				0, 0, 0, 1);
		}
	}
}
