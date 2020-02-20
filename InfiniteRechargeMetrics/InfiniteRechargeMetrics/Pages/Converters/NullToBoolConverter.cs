using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.Converters
{
    class NullToBoolConverter : IValueConverter
    {
        object inputObject;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            inputObject = value;
            return value == null ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value == true ? null : inputObject;
        }
    }
}
