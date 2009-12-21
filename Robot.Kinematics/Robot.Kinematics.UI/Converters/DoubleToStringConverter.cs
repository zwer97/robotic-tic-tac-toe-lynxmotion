using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Robot.Kinematics.UI.Converters
{
	[ValueConversion(typeof(double), typeof(string))]
	public class DoubleToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string format = parameter as string;
			if (string.IsNullOrEmpty(format))
			{
				format = "{0:#0.0##}";
			}
			return string.Format(culture, format, value);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}