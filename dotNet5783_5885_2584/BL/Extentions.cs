using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;


internal static class Extentions
{
    public static U GenericParse<U, T>(this U toObj, T fromObj)
    {
        object from = fromObj!;
        object to = toObj!;
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


                Type toPropertyType = toProperty!.PropertyType;

                if (toProperty != null && toProperty.CanWrite)
                {
                    object? fromValue = fromProperty.GetValue(from, null);
                    object? toValue = toProperty.GetValue(to, null);
                    if (toPropertyType == propertyType)
                        toProperty.SetValue(to, fromValue, null);
                    //else
                    //    try
                    //    {
                    //        toProperty.SetValue(to, Convert.ChangeType(fromValue, toPropertyType), null);

                    //    }
                    //    catch
                    //    {

                    //         Type t = Nullable.GetUnderlyingType(toPropertyType) ?? toPropertyType;
                    //        toProperty.SetValue(to, Convert.ChangeType(fromValue,t), null);
                    //        //toProperty.SetValue(to, Convert.ChangeType(fromValue, typeof(int)), null);



                    //    }


                }
            }
        }
        toObj = (U)to;
        
        return toObj;
    }
    public static void ToStringG<U>(this U obj)
    {

        Type objectType = obj!.GetType();

        foreach (System.Reflection.PropertyInfo propName in
            objectType.GetProperties())
        {
            if (propName.CanRead)
            {
                Console.WriteLine(propName.Name + ": ");
                Console.WriteLine(propName.GetValue(obj));
            }
        }
    }
}
