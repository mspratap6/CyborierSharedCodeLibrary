using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Cyborier.Shared.Windows.WPF.Converters
{
    public class IntegerToStringConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int retval;

            if (int.TryParse((string)value, out retval))
            {
                if (retval != 0)
                {
                    return retval;
                }

            }
            return null;

        }
    }
}
