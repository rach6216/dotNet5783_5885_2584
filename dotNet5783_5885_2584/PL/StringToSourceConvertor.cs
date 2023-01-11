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
        if (status== "order created")
        {
            return "Orders/orderConfirmed.png";
        }
        if(status== "order shiped")
        {
            return "Orders/orderShipped.png";
        }
        return "Orders/orderDelivered.png";
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
