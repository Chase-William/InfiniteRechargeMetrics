using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace InfiniteRechargeMetrics.ViewModels.HomeVM.TemplateVM.Converters
{
    public class RandevuValueConverter : IValueConverter
    {
        public const string DROID_ONE = "droid_one";
        public const string DROID_TWO = "droid_two";
        public const string DROID_THREE = "droid_three";
        public const string IS_RANDEVU_LEVEL = "is_randevu_level";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool randevuStatus = (bool)value;

            switch ((string)parameter)
            {
                case DROID_ONE when randevuStatus == false:
                    return 0;
                case DROID_ONE when randevuStatus == true:
                    return 25;

                case DROID_TWO when randevuStatus == false:
                    return 0;
                case DROID_TWO when randevuStatus == true:
                    return 25;

                case DROID_THREE when randevuStatus == false:
                    return 0;
                case DROID_THREE when randevuStatus == true:
                    return 5;

                case IS_RANDEVU_LEVEL when randevuStatus == false:
                    return 0;
                case IS_RANDEVU_LEVEL when randevuStatus == false:
                    return 65;
                default:
                    return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
