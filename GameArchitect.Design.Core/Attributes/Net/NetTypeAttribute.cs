using System;

namespace GameArchitect.Design.Attributes.Net
{
    /// <summary>
    /// Specifies a Class/Struct/Enum as being a network aware type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
    public sealed class NetTypeAttribute : Attribute
    {
        public int? Id { get; }

        public NetTypeAttribute() { }

        public NetTypeAttribute(int id)
        {
            Id = id;
        }
    }
}