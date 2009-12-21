using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace SSC32Communication
{
	public class CalibrationFile
	{
		private enum ParamField
		{
			Index,
			Name,
			MinPosMilliSecs,
			MaxPosMilliSecs,
			Slope,
			YIntercept
		}

		public static void Read(string filePath, Servos servos)
		{
			using (StreamReader reader = new StreamReader(filePath))
			{
				// First line is just header
				reader.ReadLine();

				for (int servoIndex = 0; servoIndex < servos.Count; servoIndex++)
				{
					InitializeServo(reader, servos[servoIndex]);
				}
			}
		}

		private static void InitializeServo(StreamReader reader, Servo servo)
		{
			string line = reader.ReadLine();
			string[] parts = line.Split('\t');

			int index = Int32.Parse(parts[(int)ParamField.Index], CultureInfo.InvariantCulture);
			string name = parts[(int)ParamField.Name];
			int minPosMilliSecs = Int32.Parse(parts[(int)ParamField.MinPosMilliSecs], CultureInfo.InvariantCulture);
			servo.MinPosMSecs = minPosMilliSecs;

			int maxPosMilliSecs = Int32.Parse(parts[(int)ParamField.MaxPosMilliSecs], CultureInfo.InvariantCulture);
			servo.MaxPosMSecs = maxPosMilliSecs;

			double slope = Double.Parse(parts[(int)ParamField.Slope], CultureInfo.InvariantCulture);
			servo.Slope = slope;

			double yIntercept = Double.Parse(parts[(int)ParamField.YIntercept], CultureInfo.InvariantCulture);
			servo.YIntercept = yIntercept;
		}

		public static void Write(string filePath, Servos servos)
		{
			using (StreamWriter writer = new StreamWriter(filePath))
			{
				// First line is just header
				writer.WriteLine("Index\tName\tMinPosMilliSecs\tMaxPosMilliSecs\tSlope\tYIntercept");

				for (int servoIndex = 0; servoIndex < servos.Count; servoIndex++)
				{
					WriteParameters(writer, servos[servoIndex], servoIndex);
				}
			}
		}

		private static void WriteParameters(StreamWriter writer, Servo servo, int index)
		{
			writer.Write(index);
			writer.Write('\t');
			writer.Write(servo.Name);
			writer.Write('\t');
			writer.Write(servo.MinPosMSecs);
			writer.Write('\t');
			writer.Write(servo.MaxPosMSecs);
			writer.Write('\t');
			writer.Write(servo.Slope);
			writer.Write('\t');
			writer.Write(servo.YIntercept);
			writer.WriteLine();
		}
	}
}