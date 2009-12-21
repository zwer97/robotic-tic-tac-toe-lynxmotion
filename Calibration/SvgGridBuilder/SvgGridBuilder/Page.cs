using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SvgNet.SvgTypes;

namespace SvgGridBuilder
{
	public class Page
	{
		public float Width { get; private set; }
		public float Height { get; private set; }
		public SvgLengthType Unit { get; private set; }

		public Page(float width, float height, SvgLengthType unit)
		{
			this.Width = width;
			this.Height = height;
			this.Unit = unit;
		}
	}
}
