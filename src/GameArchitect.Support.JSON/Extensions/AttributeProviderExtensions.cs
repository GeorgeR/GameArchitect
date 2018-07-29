using System;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace GameArchitect.Support.JSON.Extensions
{
    internal static class AttributeProviderExtensions
    {
        public static bool HasAttribute<TAttribute>(this IAttributeProvider source)
            where TAttribute : Attribute
        {
            return source.GetAttributes(typeof(TAttribute), true).Count > 0;
        }

        public static TAttribute GetAttribute<TAttribute>(this IAttributeProvider source)
            where TAttribute : Attribute
        {
            var attributes = source.GetAttributes(typeof(TAttribute), true);
            if (!attributes.Any())
                return null;

            return (TAttribute) attributes.FirstOrDefault();
        }
    }
}
