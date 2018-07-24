using System;

namespace GameArchitect.Design.Support.Attributes
{
    /// <summary>
    /// Indicates that the function or parameter does not alter it's parent object.
    /// This makes it const in C++ for example.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
    public sealed class ImmutableAttribute : Attribute
    {

    }
}