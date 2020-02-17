using InfiniteRechargeMetrics.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.Pages.Converters
{
    public class RobotsMovedFromSpawnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{StageConstants.ROBOTS_MOVED_FROM_SPAWN_HEADER} {(int)value / StageConstants.ROBOTS_MOVED_FROM_SPAWN_MULTIPLIER}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Gettings the number after the space
            return int.Parse(((string)value).Substring(StageConstants.ROBOTS_MOVED_FROM_SPAWN_HEADER.Length, ((string)value).Length - StageConstants.ROBOTS_MOVED_FROM_SPAWN_HEADER.Length)) * StageConstants.ROBOTS_MOVED_FROM_SPAWN_MULTIPLIER;
        }
    }
}
