using System;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Marks a function or property as being asynchronous. Depending on the generated language,
    /// this could mean it returns a future/promise or provides a callback.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    public sealed class AsyncAttribute : Attribute
    {

    }
}