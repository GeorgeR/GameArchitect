using System;

namespace GameArchitect.Design.Support.Attributes.Net
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
    public sealed class NetTypeAttribute : Attribute
    {
        public int Id { get; } = -1;

        public NetTypeAttribute(int? id)
        {
            if (id.HasValue)
                Id = id.Value;
        }
    }
}