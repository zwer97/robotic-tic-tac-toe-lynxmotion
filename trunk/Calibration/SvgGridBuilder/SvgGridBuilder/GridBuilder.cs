using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SvgNet.SvgElements;
using System.IO;
using SvgNet.SvgTypes;
using System.Drawing;
using SvgNet;
using System.Globalization;

namespace SvgGridBuilder
{
	public class GridBuilder
	{
		private const SvgLengthType s_LengthType = SvgLengthType.SVG_LENGTHTYPE_MM;

		private static readonly SvgStyle s_MinorLineStyle = "fill:none;stroke:#000000;stroke-opacity:1;stroke-width:0.70866142;stroke-miterlimit:4;stroke-dasharray:1.418,1.418;stroke-dashoffset:0";
		private static readonly SvgStyle s_NormalLineStyle = "fill:none;stroke:#000000;stroke-opacity:1;stroke-width:0.70866142;stroke-miterlimit:4;stroke-dasharray:none";
		private static readonly SvgStyle s_MajorLineStyle = "fill:none;stroke:#000000;stroke-opacity:1;stroke-width:1.41732284;stroke-miterlimit:4;stroke-dasharray:none";

		public Page Page { get; private set; }
		public PointF Origin { get; set; }
		public SvgSvgElement Root { get; private set; }

		public GridBuilder(Page page, float originOffset)
		{
			this.Page = page;

			this.Origin = new PointF(this.Page.Width / 2, originOffset);
			this.Root = new SvgSvgElement(
				GetSvgLength(this.Page.Width),
				GetSvgLength(this.Page.Height));
		}

		public void AddHorizontalLines(float distance)
		{
			SvgGroupElement horizontalLinesGroup = new SvgGroupElement("HorizontalLines");
			InsertStyledElements(horizontalLinesGroup, GetHorizontalLines(distance));
			this.Root.AddChild(horizontalLinesGroup);
		}

		private IEnumerable<SvgStyledTransformedElement> GetHorizontalLines(float distance)
		{
			for (float y = this.Origin.Y; y < this.Page.Height; y += distance)
			{
				SvgLineElement lineElement = new SvgLineElement(
					GetSvgLength(0), GetSvgLength(y),
					GetSvgLength(this.Page.Width), GetSvgLength(y));

				yield return lineElement;
			}
		}

		public void AddVerticalLines(float distance)
		{
			SvgGroupElement verticalLinesGroup = new SvgGroupElement("VerticalLines");
			InsertStyledElements(verticalLinesGroup, GetVerticalLines(distance));
			this.Root.AddChild(verticalLinesGroup);
		}

		private IEnumerable<SvgStyledTransformedElement> GetVerticalLines(float distance)
		{
			int halfCount = (int)((this.Page.Width - this.Origin.X) / distance);
			for (int i = -halfCount; i <= halfCount; i++)
			{
				float x = this.Origin.X + i * distance;
				SvgLineElement lineElement = new SvgLineElement(
					GetSvgLength(x), GetSvgLength(0),
					GetSvgLength(x), GetSvgLength(this.Page.Height));

				yield return lineElement;
			}
		}

		public void AddAngleLines(float deltaAngleDegree, float innerRadius)
		{
			SvgGroupElement angleLinesGroup = new SvgGroupElement("AngleLines");
			InsertStyledElements(angleLinesGroup, GetAngleLines(deltaAngleDegree, innerRadius));
			this.Root.AddChild(angleLinesGroup);
		}

		private IEnumerable<SvgStyledTransformedElement> GetAngleLines(double deltaAngleDegree, float innerRadius)
		{
			//float outerRadius = (float)Math.Sqrt(this.Page.Width * this.Page.Width + this.Page.Height * this.Page.Height);

			int halfCount = (int)(90 / deltaAngleDegree);
			for (int i = -halfCount; i <= halfCount; i++)
			{
				double angle = (i * deltaAngleDegree) / 180.0 * Math.PI;
				PointF startingPoint = new PointF(
					(float)(Math.Sin(angle) * innerRadius) + this.Origin.X,
					(float)(Math.Cos(angle) * innerRadius) + this.Origin.Y);

				double outerRadius = GetOuterRadius(angle);
				PointF endPoint = new PointF(
					(float)(Math.Sin(angle) * outerRadius) + this.Origin.X,
					(float)(Math.Cos(angle) * outerRadius) + this.Origin.Y);

				SvgLineElement lineElement = new SvgLineElement(
					GetSvgLength(startingPoint.X), GetSvgLength(startingPoint.Y),
					GetSvgLength(endPoint.X), GetSvgLength(endPoint.Y));

				yield return lineElement;
			}
		}

		private double GetOuterRadius(double angle)
		{
			double outerRadiusFor0Degree = this.Page.Height - this.Origin.Y;
			double outerRadiusFor90Degree = this.Page.Width / 2;

			double angleDegree = angle / Math.PI * 180;

			if (Math.Abs(angleDegree) < 10)
			{
				return outerRadiusFor0Degree / Math.Cos(angle);
			}
			if (Math.Abs(-90 - angleDegree) < 10 || Math.Abs(90 - angleDegree) < 10)
			{
				return Math.Abs(outerRadiusFor90Degree / Math.Sin(angle));
			}

			return Math.Min(
				Math.Abs(outerRadiusFor0Degree / Math.Cos(angle)),
				Math.Abs(outerRadiusFor90Degree / Math.Sin(angle)));
		}

		private void InsertStyledElements(SvgGroupElement linesGroup, IEnumerable<SvgStyledTransformedElement> styledElements)
		{
			SvgGroupElement minorGroup = new SvgGroupElement("MinorLines");
			minorGroup.Style = s_MinorLineStyle;
			linesGroup.AddChild(minorGroup);

			SvgGroupElement normalGroup = new SvgGroupElement("NormalLines");
			normalGroup.Style = s_NormalLineStyle;
			linesGroup.AddChild(normalGroup);

			SvgGroupElement majorGroup = new SvgGroupElement("MajorLines");
			majorGroup.Style = s_MajorLineStyle;
			linesGroup.AddChild(majorGroup);

			int i = -1;
			foreach (SvgStyledTransformedElement styledElement in styledElements)
			{
				i++;
				if (i % 10 == 0)
				{
					majorGroup.AddChild(styledElement);
					continue;
				}
				if (i % 2 == 0)
				{
					normalGroup.AddChild(styledElement);
					continue;
				}
				minorGroup.AddChild(styledElement);
			}
		}

		public void AddCircles(float distance)
		{
			SvgGroupElement circlesGroup = new SvgGroupElement("Circles");
			InsertStyledElements(circlesGroup, GetCircles(distance));
			this.Root.AddChild(circlesGroup);
		}

		private IEnumerable<SvgStyledTransformedElement> GetCircles(float distance)
		{
			float maxRadius = (float)Math.Sqrt(this.Page.Width * this.Page.Width / 4 + (this.Page.Height - this.Origin.Y) * (this.Page.Height - this.Origin.Y));
			int count = (int)(maxRadius / distance);
			for (int i = 0; i <= count; i++)
			{
				float radius = i * distance;
				SvgEllipseElement circleElement = new SvgEllipseElement(
					GetSvgLength(this.Origin.X), GetSvgLength(this.Origin.Y),
					GetSvgLength(radius), GetSvgLength(radius));

				yield return circleElement;
			}
		}

		private SvgLength GetSvgLength(float length)
		{
			return new SvgLength(length, this.Page.Unit);
		}

		public void Save(string filePath)
		{
			string svgText = this.Root.WriteSVGString(true, false);
			using (StreamWriter writer = new StreamWriter(filePath, false))
			{
				writer.Write(svgText);
			}
		}
	}
}
