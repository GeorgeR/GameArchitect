using System.Reflection;
using GameArchitect.Design.Support.Attributes;

namespace GameArchitect.Design.Support.Metadata
{
    public sealed class FunctionInfo : MemberInfoBase<System.Reflection.MethodInfo>
    {
        public override string TypeName => "Function";

        public FunctionInfo(TypeInfo declaringType, MethodInfo native)
            : base(declaringType, native)
        {
            Name = native.Name;
            Type = ResolveType(Native.ReturnType, Native);

            Mutability = Native.GetCustomAttribute<ImmutableAttribute>() != null ? Mutability.Immutable : Mutability.Mutable;
            Access = Permission.ReadWrite;
        }
        
        public override string GetPath()
        {
            return $"{DeclaringType.Name}.{Name}() : {GetTypeString()}";
        }
    }
}