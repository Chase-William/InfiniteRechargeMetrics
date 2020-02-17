using System;
using System.Globalization;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.Converters
{
    /// <summary>
    ///     Converts the bool for the login so when the user is not logged in, the button will be visible.
    /// </summary>
    class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
