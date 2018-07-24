using System;

namespace GameArchitect.Design.Support.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class AccessAttributeBase : Attribute
    {
        protected virtual Permission Permission { get; }
    }
}