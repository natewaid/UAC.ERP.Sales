using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace UAC.ERP.Sales.Models
{
    public static class Extensions
    { 
        public static ObservableCollection<KeyValuePair<TKey, TValue>> ToObservableCollectionOfKvp<TData, TKey, TValue>(this IEnumerable<TData> data, string keyProp, string valueProp)
        {
            var items = new ObservableCollection<KeyValuePair<TKey, TValue>>();

            var type = typeof(TData);

            foreach (var d in data)
            {
                items.Add(new KeyValuePair<TKey, TValue>(
                    (TKey)type.GetProperty(keyProp).GetValue(d),
                    (TValue)type.GetProperty(valueProp).GetValue(d)
                    ));
            }

            return items;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            var coll = new ObservableCollection<T>();

            foreach (var i in items)
            {
                coll.Add(i);
            }

            return coll;
        }

        public static T2 CopyTo<T1, T2>(this T1 from, T2 to) where T1 : class where T2 : class
        {
            foreach (var fromProp in from.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var toProp = to.GetType().GetProperty(fromProp.Name);

                if (toProp == null)
                {
                    continue;
                }

                if (toProp.DetermineType().IsAssignableFrom(fromProp.DetermineType()))
                {
                    toProp.SetValue(to, fromProp.GetValue(from, null), null);
                }
            }

            return to;
        }

        public static Type DetermineType(this PropertyInfo prop)
        {
            return Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        }
    }
}
