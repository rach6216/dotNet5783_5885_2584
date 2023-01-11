using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL;

public class StringToSourceConvertor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string status = (string)value;
        if (status == "order created")
        {
            return "Images/orderConfirmed.png";
        }
        if (status == "order shiped")
        {
            return "Images/orderShipped.png";
        }
        return "Images/orderDelivered.png";
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}