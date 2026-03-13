using System.Data;
using System.Collections;

namespace Ground.Utilities.Extensions
{
    /// <summary>
    /// Provides extension methods for objects, including a method to convert an object's properties to a query string format.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts the properties of an object to a query string format.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>A query string representation of the object's properties.</returns>
        public static string ToQueryString(this object obj)
        {
            if (obj is null) throw new ArgumentNullException("Object");

            var properties = obj.GetType().GetProperties()
                .Where(x => x.CanWrite)
                .Where(x => x.GetValue(obj, null) is not null)
                .Select(x => KeyValuePair.Create(x.Name, x.GetValue(obj, null))).ToList();

            var propertyNames = properties
                .Where(x => x.Value is not string && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            foreach (var key in propertyNames)
            {
                var valueType = properties.FirstOrDefault(x => x.Key == key).Value.GetType();

                var valueElemType = valueType.IsGenericType
                    ? valueType.GetGenericArguments()[0]
                    : valueType.GetElementType();

                if (valueElemType.IsPrimitive || valueElemType == typeof(string) || valueElemType == typeof(Guid))
                {
                    var enumerable = properties.FirstOrDefault(c => c.Key == key).Value as IEnumerable;

                    properties.RemoveAll(x => x.Key == key);

                    foreach (var item in enumerable)
                    {
                        properties.Add(KeyValuePair.Create(key, item));
                    }
                }
            }

            return string.Join("&", properties.Where(x => x.Value is not null)
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
