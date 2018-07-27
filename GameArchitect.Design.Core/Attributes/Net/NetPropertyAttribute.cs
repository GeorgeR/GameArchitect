using System;

namespace GameArchitect.Design.Attributes.Net
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class NetPropertyAttribute : Attribute
    {
        public int? Index { get; }

        public NetPropertyAttribute() { }

        public NetPropertyAttribute(int index)
        {
            Index = index;

            if (Index <= 0)
                throw new Exception($"NetProperty index must be 1 or greater.");
        }
    }
}