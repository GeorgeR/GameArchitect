using System;
using GameArchitect.Design.Support;

namespace GameArchitect.Design.Metadata
{
    public sealed class ProxyMemberInfo : MemberInfoBase<Type>
    {
        public override string TypeName { get; } = "Proxy";

        public ProxyMemberInfo(TypeInfo type, string name) 
            : base(null, type.Native)
        {
            Name = name;
            Type = type;

            Mutability = Mutability.Immutable;
            Permission = Permission.ReadWrite;
        }

        public override string GetPath()
        {
            return Name;
        }
    }
}