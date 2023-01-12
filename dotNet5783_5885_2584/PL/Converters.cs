using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PL
{
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        bool boolValue = (bool)value;
        //        if (boolValue)
        //        {
        //            return Visibility.Visible;
        //        }
        //        else
        //        {
        //            return Visibility.Collapsed;
        //        }
        //    }

        //    public object ConvertBack(
        //      object value,
        //      Type targetType,
        //      object parameter,
        //      CultureInfo culture)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible;
            try
            {
                 isVisible = (bool)value;

            }
            catch
            {
                if((int)value==0)
                    isVisible = false;
                else isVisible = true;
            }
            if (isVisible)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}