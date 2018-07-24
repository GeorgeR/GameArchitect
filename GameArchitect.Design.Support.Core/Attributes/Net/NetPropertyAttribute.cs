using System;

namespace GameArchitect.Design.Support.Attributes.Net
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class NetPropertyAttribute : Attribute
    {
        public int Order { get; } = -1;

        public NetPropertyAttribute(int? order)
        {
            if (order.HasValue)
                Order = order.Value;
        }
    }
}