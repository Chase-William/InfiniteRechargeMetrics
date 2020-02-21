using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.Converters
{
    public class DetermineFrameVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // If the parameter passed is >= to the current number to display, then return true so the trigger will display the calling frame
            if ((byte)value >= byte.Parse((string)parameter))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
