using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Vision
{
	public class CameraVsPhysicalPoint
	{
		public PointF CameraPoint { get; private set; }
		public PointF PhysicalPoint { get; private set; }

		public CameraVsPhysicalPoint(PointF cameraPoint, PointF physicalPoint)
		{
			this.CameraPoint = cameraPoint;
			this.PhysicalPoint = physicalPoint;
		}
	}
}
