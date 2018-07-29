using System;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

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

        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            ForMeta<PropertyInfo>(info, o =>
            {
                if (o.Type.Native != Value.GetType())
                    logger.LogError($"A Default attribute (of type {Value.GetType()}) is specified for property {o.GetPath()} which uses an incompatible type ({o.Type.GetPath()}). Ensure the attribute value and property type match.");
            });

            return true;
        }
    }
}