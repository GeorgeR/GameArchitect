using System.Collections.Generic;

namespace GameArchitect.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> items)
        {
            foreach (var item in source)
                source.Add(item);
        }
    }
}