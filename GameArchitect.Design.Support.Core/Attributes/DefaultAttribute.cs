using System;

namespace GameArchitect.Design.Support.Attributes
{
    /// <summary>
    /// Specify a default value for this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DefaultAttribute : Attribute
    {
        public object Value { get; }

        public DefaultAttribute(object value)
        {
            Value = value;
        }
        
        public bool ValueIsTypeOf<T>()
        {
            return Value.GetType() == typeof(T);
        }

        public T ValueAs<T>()
        {
            return (T)Value;
        }
    }
}