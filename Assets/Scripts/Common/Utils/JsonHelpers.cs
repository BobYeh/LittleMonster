using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using MiniJSON;

public class JsonHelpers
{
    public static List<T> GetObjectList<T>(string jsonData) where T : new()
    {
        var objectArray = (IList<object>)Json.Deserialize(jsonData);
        List<T> list = new List<T>();

        foreach(var obj in objectArray)
        {
            list.Add(DictionaryToObject<T>(obj as Dictionary<string, object>));
        }

        return list;
    }


    public static T DictionaryToObject<T>(IDictionary<string, object> dict) where T : new()
    {
        var t = new T();
        PropertyInfo[] properties = t.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                continue;

            KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

            // Find which property type (int, string, double? etc) the CURRENT property is...
            Type tPropertyType = t.GetType().GetProperty(property.Name).PropertyType;

            // Fix nullables...
            Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;

            // ...and change the type
            object newA = Convert.ChangeType(item.Value, newT);
            t.GetType().GetProperty(property.Name).SetValue(t, newA, null);
        }
        return t;
    }
}
