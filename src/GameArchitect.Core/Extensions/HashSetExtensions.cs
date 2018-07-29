using System.Collections.Generic;

namespace GameArchitect.Extensions
{
    public static class HashSetExtensions
    {
        public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> collection)
        {
            foreach (var item in collection)
                source.Add(item);
        }
    }
}