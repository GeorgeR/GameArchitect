using System;
using GameArchitect.Design.Support;

namespace GameArchitect.Design.Metadata
{
    public sealed class ProxyMemberInfo<TTypeInfo> : MemberInfoBase<TTypeInfo, Type>
        where TTypeInfo : class, ITypeInfo
    {
        public ProxyMemberInfo(IMetadataProvider metadataProvider, ITypeInfo type, string name) 
            : base(metadataProvider, null, type.Native)
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