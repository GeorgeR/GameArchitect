using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameArchitect.Extensions.Reflection
{
    public static class CustomAttributeProviderExtensions
    {
        public static bool HasAttribute<TAttribute>(this ICustomAttributeProvider source)
            where TAttribute : Attribute
        {
            return source.GetCustomAttributes(typeof(TAttribute), true).Length > 0;
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider source)
            where TAttribute : Attribute
        {
            return source.GetCustomAttributes(true).OfType<TAttribute>().Select(o => o);
        }

        public static IEnumerable<Attribute> GetAttributes(this ICustomAttributeProvider source)
        {
            return source.GetCustomAttributes(true).Select(o => (Attribute) o);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider source)
            where TAttribute : Attribute
        {
            var attributes = source.GetAttributes<TAttribute>().ToList();
            if (!attributes.Any())
                return null;

            return attributes.FirstOrDefault();
        }
    }
}