using System;
using System.Collections.Generic;

namespace GameArchitect.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> func)
        {
            foreach (var item in source)
                func(item);
        }
    }
}