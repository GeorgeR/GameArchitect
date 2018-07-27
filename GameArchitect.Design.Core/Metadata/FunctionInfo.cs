using System.Linq;
using System.Reflection;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Support;

namespace GameArchitect.Design.Metadata
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
            Permission = Permission.ReadWrite;
        }

        private IQueryable<ParameterInfo> _parameters;
        public IQueryable<ParameterInfo> GetParameters()
        {
            if (_parameters != null)
                return _parameters;

            _parameters = Native
                .GetParameters()
                .Select(o => new ParameterInfo(this, DeclaringType, o))
                .AsQueryable();
            
            return GetParameters();
        }

        public override string GetPath()
        {
            return $"{DeclaringType.Name}.{Name}() : {GetTypeString()}";
        }
    }
}