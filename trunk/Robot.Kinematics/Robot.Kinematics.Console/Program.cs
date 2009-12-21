using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.Kinematics.DenavitHartenberg;
using Robot.Kinematics;
using System.Diagnostics;

namespace Robot.Kinematics.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Vector3 wristLocation = new Vector3(RobotArm.ForearmLength, 0, RobotArm.ShoulderHeight + RobotArm.UpperArmLength);
				JointAngles jointAngles = RobotArm.DoInverseKinematics(wristLocation);
				Debug.WriteLine(jointAngles);


				//DHFrameInfo[] frameInfos = new DHFrameInfo[]
				//    {
				//        new	DHFrameInfo(0, 90, 60, 90),
				//        new	DHFrameInfo(100, 0, 0, 90),
				//        //new	DHFrameInfo(3, 0, 0, 0),
				//    };

				//Matrix4x4[] transformations = new Matrix4x4[frameInfos.Length];
				//Matrix4x4 completeTransformation = Matrix4x4.Identity;

				//for (int i = frameInfos.Length - 1; i >= 0; i--)
				//{
				//    transformations[i] = DHTransformation.ToPreviousFrame(frameInfos[i]);
				//    completeTransformation = transformations[i] * completeTransformation;
				//}

				//Vector4 vector1 = new Vector4(0, 0, 0);
				//Vector4 transformedVector = completeTransformation * vector1;

				//System.Console.WriteLine(transformedVector);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.ToString());
			}
			finally
			{
				System.Console.ReadLine();
			}
		}
	}
}
