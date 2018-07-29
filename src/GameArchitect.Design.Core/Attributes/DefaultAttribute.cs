using System;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Specify a default value for this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DefaultAttribute : ValidatableAttribute
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

        public override bool IsValid<TMeta>(TMeta info)
        {
            base.IsValid(info);

            ForMeta<PropertyInfo>(info, o =>
            {
                if (o.Type.Native != Value.GetType())
                    throw new Exception($"A Default attribute is specified for property {o.GetPath()} which uses an incompatible type. Ensure the attribute value and property type match.");
            });

            return true;
        }
    }
}