using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.Kinematics.DenavitHartenberg;

namespace Robot.Kinematics
{
	public class RobotArm
	{
		public const double ShoulderHeight = 68;
		public const double UpperArmLength = 95.25;  // humerus
		public const double ForearmLength = 107.95;  // ulna
		public const double EndEffectorLength = 100.0;

		static RobotArm()
		{
		}

		private static readonly double s_UpperArmLengthSquared = UpperArmLength * UpperArmLength;
		private static readonly double s_ForearmLengthSquared = ForearmLength * ForearmLength;

		private static DHFrameInfo[] GetFrameInfos(
			double baseAngle,
			double shoulderAngle,
			double elbowAngle,
			double wristTiltAngle,
			double wristRotationAngle)
		{
			return new DHFrameInfo[]
			{
				new DHFrameInfo(0, 90, ShoulderHeight, baseAngle),
				new DHFrameInfo(UpperArmLength, 0, 0, shoulderAngle),
				new DHFrameInfo(ForearmLength, 0, 0, elbowAngle),
				new DHFrameInfo(0, 90, 0, wristTiltAngle),
				new DHFrameInfo(0, 0, EndEffectorLength, wristRotationAngle),
			};
		}

		public static Vector3 DoForwardKinematics(
			double baseAngle,
			double shoulderAngle,
			double elbowAngle,
			double wristTiltAngle,
			double wristRotationAngle)
		{
			return DoForwardKinematics(baseAngle, shoulderAngle, elbowAngle, wristTiltAngle, wristRotationAngle, new Vector3());
		}

		public static Vector3 DoForwardKinematics(
			double baseAngle,
			double shoulderAngle,
			double elbowAngle,
			double wristTiltAngle,
			double wristRotationAngle,
			Vector3 endVector)
		{
			DHFrameInfo[] frameInfos = GetFrameInfos(baseAngle, shoulderAngle, elbowAngle, wristTiltAngle, wristRotationAngle);

			Matrix4x4[] transformations = new Matrix4x4[frameInfos.Length];
			Matrix4x4 completeTransformation = Matrix4x4.Identity;

			for (int i = frameInfos.Length - 1; i >= 0; i--)
			{
				transformations[i] = DHTransformation.ToPreviousFrame(frameInfos[i]);
				completeTransformation = transformations[i] * completeTransformation;
			}

			Vector4 endVector4 = new Vector4(endVector);
			Vector4 transformedVector = completeTransformation * endVector4;

			return new Vector3(
				transformedVector.V0,
				transformedVector.V1,
				transformedVector.V2
				);
		}

		public static JointAngles DoInverseKinematics(Vector3 targetWristPoint)
		{
			JointAngles jointAngles = new JointAngles();

			jointAngles.BaseAngle = Math.Atan2(targetWristPoint.Y, targetWristPoint.X) * 180.0 / Math.PI;

			double distanceFromZAxis = Math.Sqrt(targetWristPoint.X * targetWristPoint.X + targetWristPoint.Y * targetWristPoint.Y);
			double heightAboveShoulder = targetWristPoint.Z - ShoulderHeight;

			// Inverse kinematics for 2R planar manipulator
			double cosTheta2 =
				(distanceFromZAxis * distanceFromZAxis + heightAboveShoulder * heightAboveShoulder -
				s_UpperArmLengthSquared - s_ForearmLengthSquared) /
				(2.0 * UpperArmLength * ForearmLength);

			double sinTheta2 = -Math.Sqrt(1 - cosTheta2 * cosTheta2);

			double theta2 = Math.Atan2(sinTheta2, cosTheta2) * 180.0 / Math.PI;
			jointAngles.ElbowAngle = theta2 + 90;

			double k1 = UpperArmLength + ForearmLength * cosTheta2;
			double k2 = ForearmLength * sinTheta2;

			double theta1 = (Math.Atan2(heightAboveShoulder, distanceFromZAxis) - Math.Atan2(k2, k1)) * 180.0 / Math.PI;
			jointAngles.ShoulderAngle = 90 - theta1;

			return jointAngles;
		}
	}
}
