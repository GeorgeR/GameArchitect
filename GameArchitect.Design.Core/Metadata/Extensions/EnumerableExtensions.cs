using System;
using System.Linq;

namespace GameArchitect.Design.Metadata.Extensions
{
    public static class EnumerableExtensions
    {
        public static IQueryable<IMemberInfo> WithAttribute<TAttribute>(this IQueryable<IMemberInfo> source)
            where TAttribute : Attribute
        {
            return source.Where(o => o.HasAttribute<TAttribute>());
        }
    }
}
