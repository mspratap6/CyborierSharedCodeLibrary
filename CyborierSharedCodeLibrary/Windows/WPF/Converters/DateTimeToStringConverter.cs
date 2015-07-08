using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Cyborier.Shared.Windows.WPF.Converters          
{
    /// <summary>
    /// Converter to convert datetime to string.
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter
    {

        /// <summary>
        /// Convert DateTime to string using Current Culture.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return ((DateTime)value).ToString(culture.DateTimeFormat);
            }
            catch (Exception ex)
            {
                return "";        
            }
        }


        /// <summary>
        /// Convert String to DateTime using Current Culture.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return DateTime.Parse((string)value, culture.DateTimeFormat);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
