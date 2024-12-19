using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HealthCouch.CaseStudy.Common.Converters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string gender)
            {
                return gender.Equals("M", StringComparison.OrdinalIgnoreCase) ? "Male" :
                       gender.Equals("F", StringComparison.OrdinalIgnoreCase) ? "Female" : "Other";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}