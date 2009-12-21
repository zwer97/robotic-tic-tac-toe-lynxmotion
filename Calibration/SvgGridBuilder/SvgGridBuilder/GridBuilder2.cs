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
	public class GridBuilder2
	{
		private const SvgLengthType s_LengthType = SvgLengthType.SVG_LENGTHTYPE_MM;

		private static readonly SvgStyle s_NormalLineStyle = "fill:none;stroke:#000000;stroke-opacity:1;stroke-width:7;stroke-miterlimit:4;stroke-dasharray:none";
		private static readonly SvgStyle s_AlignmentLineStyle = "fill:none;stroke:#000000;stroke-opacity:0.2;stroke-width:0.5;stroke-miterlimit:4;stroke-dasharray:none";

		public Page Page { get; private set; }
		public PointF Origin { get; set; }
		public SvgSvgElement Root { get; private set; }

		public GridBuilder2(Page page, float originOffset)
		{
			this.Page = page;

			this.Origin = new PointF(this.Page.Width / 2, originOffset);
			this.Root = new SvgSvgElement(
				GetSvgLength(this.Page.Width),
				GetSvgLength(this.Page.Height));
		}

		public void AddSquares(float yOffset, float xOffset, float sideLength, int xCount, int yCount)
		{
			SvgGroupElement squaresGroup = new SvgGroupElement("Squares");
			squaresGroup.Style = s_NormalLineStyle;

			float xStart = this.Origin.X + xOffset - xCount / 2.0F * sideLength;
			float yStart = this.Origin.Y + yOffset;

			for (int i = 0; i < xCount; i++)
			{
				for (int j = 0; j < yCount; j++)
				{
					SvgRectElement rectElement = new SvgRectElement(
						GetSvgLength(xStart + i * sideLength), GetSvgLength(yStart + j * sideLength),
						GetSvgLength(sideLength), GetSvgLength(sideLength));
					
					squaresGroup.AddChild(rectElement);
				}
			}

			this.Root.AddChild(squaresGroup);
		}

		public void AddAlignmentLines()
		{
			SvgGroupElement alinmentLinesGroup = new SvgGroupElement("AlignmentLines");
			alinmentLinesGroup.Style = s_AlignmentLineStyle;

			SvgLineElement line1 = new SvgLineElement(
				GetSvgLength(this.Origin.X), GetSvgLength(0),
				GetSvgLength(this.Origin.X), GetSvgLength(this.Page.Width));
			alinmentLinesGroup.AddChild(line1);

			SvgLineElement line2 = new SvgLineElement(
				GetSvgLength(0), GetSvgLength(this.Origin.Y),
				GetSvgLength(this.Page.Width), GetSvgLength(this.Origin.Y));
			alinmentLinesGroup.AddChild(line2);

			float radius = 65;
			SvgEllipseElement cutOutCircle = new SvgEllipseElement(
				GetSvgLength(this.Origin.X), GetSvgLength(this.Origin.Y),
				GetSvgLength(radius), GetSvgLength(radius));
			alinmentLinesGroup.AddChild(cutOutCircle);

			this.Root.AddChild(alinmentLinesGroup);
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
