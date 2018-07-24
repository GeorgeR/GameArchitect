using System;

namespace GameArchitect.Design.Support.Attributes
{
    /// <summary>
    /// Indicates that the property is optional.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class OptionalAttribute : Attribute
    {

    }
}