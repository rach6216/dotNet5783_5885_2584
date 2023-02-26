using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class Extentions
{
    public static U GenericParse<U, T>(this U toObj, T fromObj)
    {
        object from = fromObj;
        object to = toObj;
        Type fromObjectType = from.GetType();
        Type toObjectType = to.GetType();

        foreach (System.Reflection.PropertyInfo fromProperty in
            fromObjectType.GetProperties())
        {
            if (fromProperty.CanRead)
            {
                string propertyName = fromProperty.Name;
                Type propertyType = fromProperty.PropertyType;

                System.Reflection.PropertyInfo toProperty =
                    toObjectType.GetProperty(propertyName);


                Type toPropertyType = toProperty.PropertyType;

                if (toProperty != null && toProperty.CanWrite)
                {
                    object? fromValue = fromProperty.GetValue(from, null);
                    if (toPropertyType == propertyType)
                        toProperty.SetValue(to, fromValue);
                }
            }
        }
        toObj = (U)to;
        return toObj;
    }
    public static string ToStringG<U>(this U obj)
    {


        Type objectType = obj.GetType();
        String str = "";
        foreach (System.Reflection.PropertyInfo propName in
            objectType.GetProperties())
        {
            if (propName.CanRead)
            {
                str += propName.Name + ": " + propName.GetValue(obj) + "\n";
            }
        }
        return str;
    }
}
