using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace SSC32Communication
{
	public class SSC32ConfigFile
	{
		public static void ConfigureServosFromFile(string filePath, Servo[] servos)
		{
			using (StreamReader file = new StreamReader(filePath))
			{
				for (int servoIndex = 0; servoIndex < Servos.c_ServosCount; servoIndex++)
				{
					InitializeServo(file, servos[servoIndex]);
				}
			}
		}

		private static void InitializeServo(StreamReader file, Servo servo)
		{
			bool isEnabled = ParseBoolean(file.ReadLine());
			bool isReverse = ParseBoolean(file.ReadLine());
			file.ReadLine(); // Rate
			file.ReadLine(); // Don't know what this number means
			int limit1Ticks = Int32.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
			int limit2Ticks = Int32.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
			int limit1Degrees = Int32.Parse(file.ReadLine(), CultureInfo.InvariantCulture);
			int limit2Degrees = Int32.Parse(file.ReadLine(), CultureInfo.InvariantCulture);

			int minPosMSecs;
			int maxPosMSecs;

			int maxDegrees;
			int minDegrees;

			if (limit2Ticks > limit1Ticks)
			{
				minPosMSecs = MillisecsFromTicks(limit1Ticks);
				maxPosMSecs = MillisecsFromTicks(limit2Ticks);

				minDegrees = limit1Degrees;
				maxDegrees = limit2Degrees;
			}
			else
			{
				// inverse
				minPosMSecs = MillisecsFromTicks(limit2Ticks);
				maxPosMSecs = MillisecsFromTicks(limit1Ticks);

				minDegrees = limit2Degrees;
				maxDegrees = limit1Degrees;
			}

			double slope = (double)((maxPosMSecs - minPosMSecs)) / ((double)(maxDegrees - minDegrees));
			double yIntercept = maxPosMSecs - slope * maxDegrees;

			servo.Initialize(
				isEnabled,
				minPosMSecs,
				maxPosMSecs,
				slope,
				yIntercept);
		}

		/// <summary>
		/// Ticks represent the full servo range by a number between 0 and 1000. The servo itself is
		/// controlled by a millisecs width between 500 and 2500. 
		/// </summary>
		/// <param name="ticks"></param>
		/// <returns></returns>
		private static int MillisecsFromTicks(int ticks)
		{
			int ticksFromMiddle = ticks - 500;
			return 1500 + 2 * ticksFromMiddle;
		}

		private static bool ParseBoolean(string text)
		{
			if (text == "true")
			{
				return true;
			}
			else if (text == "false")
			{
				return false;
			}
			else
			{
				string errorMessage = String.Format(
					CultureInfo.InvariantCulture,
					"'{0}' cannot be parsed as a Boolean value.",
					text);
				throw new ArgumentException(errorMessage);
			}
		}
	}
}
