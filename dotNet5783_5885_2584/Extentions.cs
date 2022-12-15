using System;

public static class Extentions
{
    public static U GenericParse<T, U>(this U to, T from)
    {
        FieldInfo[] fields = from?.GetType().GetFields() ?? throw new Exception("no fields");
        for (int i = 0; i < fields?.Length; ++i)
        {
            FieldInfo field = from.GetType().GetField(fields[i].Name);
            FieldInfo toField = to.GetType().GetField(fields[i].Name);
            if (field != null && toField != null)
            {
                toField.SetValue(to, field.GetValue(from));
            }
        }
        return to;


    }
    public static U Copy<U,T>(this U  toObj,T fromObj)
    {
        Type fromObjectType = fromObj.GetType();
        Type toObjectType = toObj.GetType();

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
                    object fromValue = fromProperty.GetValue(fromObj, null);
                    toProperty.SetValue(toObj, fromValue, null);
                }
            }
        }
    }
}
