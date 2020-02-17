using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.Converters
{
    class IsStageCompleteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? StageConstants.STAGE_COMPLETE : StageConstants.STAGE_INCOMPLETE;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == StageConstants.STAGE_COMPLETE ? true : false;
        }
    }
}
