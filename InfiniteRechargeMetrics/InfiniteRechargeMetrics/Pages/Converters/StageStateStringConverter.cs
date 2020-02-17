using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.Converters
{
    class StageStateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (StageState)value == StageState.Autononmous ? StageConstants.STAGE_STAGE_AUTONOMOUS : StageConstants.STAGE_STATE_MANUAL;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).Substring(0, 6) == "Manual" ? StageState.Manual : StageState.Autononmous;
        }
    }
}
