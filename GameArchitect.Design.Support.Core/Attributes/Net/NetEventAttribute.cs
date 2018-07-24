using System;

namespace GameArchitect.Design.Support.Attributes.Net
{
    [AttributeUsage(AttributeTargets.Event | AttributeTargets.Delegate | AttributeTargets.Method)]
    public sealed class NetEventAttribute : Attribute
    {

    }
}