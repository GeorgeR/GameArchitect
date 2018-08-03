using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Support;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IFunctionInfo : IMemberInfo<System.Reflection.MethodInfo>
    {
        IList<IParameterInfo> Parameters { get; }
    }

    public class FunctionInfo : MemberInfoBase<System.Reflection.MethodInfo>, IFunctionInfo
    {
        public override string TypeName => "Function";

        public FunctionInfo(ITypeInfo declaringType, System.Reflection.MethodInfo native)
            : base(declaringType, native)
        {
            Name = native.Name;
            Type = ResolveType(Native.ReturnType, Native);

            Mutability = Native.GetCustomAttribute<ImmutableAttribute>() != null ? Mutability.Immutable : Mutability.Mutable;
            Permission = Permission.ReadWrite;
        }

        private IList<IParameterInfo> _parameters;
        public IList<IParameterInfo> Parameters
        {
            get
            {
                if (_parameters != null)
                    return _parameters;

                _parameters = new List<IParameterInfo>();
                _parameters.AddRange(
                    Native
                        .GetParameters()
                        .Select(o => new ParameterInfo(this, DeclaringType, o)));

                return _parameters;
            }
        }

        public override string GetPath()
        {
            return $"{DeclaringType.Name}.{Name}() : {GetTypeString()}";
        }

        public override bool IsValid(ILogger<IValidatable> logger)
        {
            var result = base.IsValid(logger);

            return result && GetAttributes().Where(o => o.GetType().ImplementsInterface<IDelegatedValidatable>())
                       .Cast<IDelegatedValidatable>()
                       .All(a => a.IsValid(logger, this));
        }
    }
}