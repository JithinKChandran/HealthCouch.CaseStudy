using System;
using System.Globalization;
using System.Windows.Data;

namespace HealthCouch.CaseStudy.Common.Converters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string gender)
            {
                return gender.Equals(parameter.ToString(), StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
            {
                return parameter.ToString();
            }
            return null;
        }
    }
}
