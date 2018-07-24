using System;

namespace GameArchitect.Design.Support.Attributes
{
    /// <summary>
    /// Indicates the property is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RequiredAttribute : Attribute
    {

    }
}