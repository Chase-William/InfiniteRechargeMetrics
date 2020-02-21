using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM.TemplateVM.Converters
{
    public class TeamTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var test = (string)value;
            return string.IsNullOrEmpty((string)value) ? "No Name Assigned" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
