using InfiniteRechargeMetrics.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.MetricsPages.Converters
{
    class MatchesToPickerStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> names = new List<string>();
            foreach (var match in (List<Match>)value)
            {
                names.Add($"Id: {match.MatchId} || Name: {match.MatchName}");
            }

            return names;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
