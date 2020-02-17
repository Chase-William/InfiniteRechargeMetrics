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
            var state = (StageState)value;
            if (StageState.Autononmous == state)
                return StageConstants.STAGE_STAGE_AUTONOMOUS;
            else
                return StageConstants.STAGE_STATE_MANUAL;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((string)value).Substring(0, 6) == "Manual")
                return StageState.Manual;
            else
                return StageState.Autononmous;
        }
    }
}
