using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Globalization;

namespace Vision
{
	public class CameraCalibration
	{
		static CameraCalibration()
		{
		}

		public static readonly CameraCalibration Instance = new CameraCalibration();

		private CalibrationPoint[,] m_CalibrationPoints;
		private int m_XCount;
		private int m_YCount;

		private CameraCalibration()
		{
		}

		public void Initialize(CameraVsPhysicalPoint[,] cameraVsPhysicalPoints)
		{
			m_XCount = cameraVsPhysicalPoints.GetLength(0);
			m_YCount = cameraVsPhysicalPoints.GetLength(1);
			m_CalibrationPoints = new CalibrationPoint[m_XCount, m_YCount];

			CameraVsPhysicalPoint center, left, right, above, below;

			for (int i = 0; i < m_XCount; i++)
			{
				for (int j = 0; j < m_YCount; j++)
				{
					center = cameraVsPhysicalPoints[i,j];
					left = (i == 0 ? null : cameraVsPhysicalPoints[i - 1, j]);
					right = (i == m_XCount - 1 ? null : cameraVsPhysicalPoints[i + 1, j]);
					above = (j == 0 ? null : cameraVsPhysicalPoints[i, j - 1]);
					below = (j == m_YCount - 1 ? null : cameraVsPhysicalPoints[i, j + 1]);

					m_CalibrationPoints[i, j] = new CalibrationPoint(center, left, right, above, below);
				}
			}
		}

		public void InitializeFromText(string serializedData)
		{
			using (StringReader reader = new StringReader(serializedData))
			{
				Initialize(reader);
			}
		}

		public void InitializeFromFile(string calibrationFile)
		{
			using (StreamReader reader = new StreamReader(calibrationFile))
			{
				Initialize(reader);
			}
		}

		private void Initialize(TextReader reader)
		{
			string line;
			line = reader.ReadLine();
			ParseXYCount(line);

			m_CalibrationPoints = new CalibrationPoint[m_XCount, m_YCount];
			for (int i = 0; i < m_XCount; i++)
			{
				for (int j = 0; j < m_YCount; j++)
				{
					line = reader.ReadLine();
					m_CalibrationPoints[i, j] = CalibrationPoint.Parse(line);
				}
			}
		}

		private void ParseXYCount(string line)
		{
			string[] parts = line.Split('\t');
			m_XCount = int.Parse(parts[0], CultureInfo.InvariantCulture);
			m_YCount = int.Parse(parts[1], CultureInfo.InvariantCulture);
		}

		public bool IsInitialized
		{
			get { return m_CalibrationPoints != null && m_CalibrationPoints.Length > 0; }
		}

		public PointF GetPhysicalPoint(PointF cameraPoint)
		{
			CalibrationPoint closestCalibrationPoint = FindClosestCalibrationPoint(cameraPoint);
			PointF physicalPoint = closestCalibrationPoint.GetPhysicalPoint(cameraPoint);
			return physicalPoint;
		}

		private CalibrationPoint FindClosestCalibrationPoint(PointF cameraPoint)
		{
			CalibrationPoint closestCalibrationPoint = null;
			float closestDistance = float.MaxValue;

			for (int i = 0; i < m_XCount; i++)
			{
				for (int j = 0; j < m_YCount; j++)
				{
					PointF calibrationCameraPoint = m_CalibrationPoints[i,j].CameraPoint;
					float distance =
						(cameraPoint.X - calibrationCameraPoint.X) * (cameraPoint.X - calibrationCameraPoint.X) +
						(cameraPoint.Y - calibrationCameraPoint.Y) * (cameraPoint.Y - calibrationCameraPoint.Y);

					if (distance < closestDistance)
					{
						closestDistance = distance;
						closestCalibrationPoint = m_CalibrationPoints[i, j];
					}
				}
			}

			return closestCalibrationPoint;
		}


		public string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(m_XCount);
			stringBuilder.Append('\t');
			stringBuilder.Append(m_YCount);
			stringBuilder.AppendLine();

			for (int i = 0; i < m_XCount; i++)
			{
				for (int j = 0; j < m_YCount; j++)
				{
					m_CalibrationPoints[i, j].SerializeAsLine(stringBuilder);
				}
			}

			return stringBuilder.ToString();
		}
	}
}
