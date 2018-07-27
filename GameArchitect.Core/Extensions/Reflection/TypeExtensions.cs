using System;

namespace GameArchitect.Extensions.Reflection
{
    public static class TypeExtensions
    {
        public static bool ImplementsInterface<TInterface>(this Type source)
        {
            return typeof(TInterface).IsAssignableFrom(source);
        }
    }
}