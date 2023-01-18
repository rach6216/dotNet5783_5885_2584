using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
namespace PL;

public class BooleanToVisibilityConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isVisible;
        try
        {
            isVisible = (bool)value;

        }
        catch
        {
            if ((int)value == 0)
                isVisible = true;
            else isVisible = false;
        }
        if (isVisible)
            return Visibility.Collapsed;
        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return 0;
    }
}
