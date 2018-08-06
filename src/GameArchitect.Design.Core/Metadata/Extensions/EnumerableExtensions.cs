using System;
using System.Collections.Generic;
using System.Linq;

namespace GameArchitect.Design.Metadata.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IMemberInfo> WithAttribute<TAttribute>(this IEnumerable<IMemberInfo> source)
            where TAttribute : Attribute
        {
            return source.Where(o => o.HasAttribute<TAttribute>());
        }
    }
}
