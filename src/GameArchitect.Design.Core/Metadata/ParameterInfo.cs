using System.Linq;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public sealed class ParameterInfo : MemberInfoBase<System.Reflection.ParameterInfo>
    {
        public override string TypeName { get; } = "Parameter";

        public FunctionInfo DeclaringFunction { get; }

        public ParameterInfo(FunctionInfo declaringFunction, TypeInfo declaringType, System.Reflection.ParameterInfo native) 
            : base(declaringType, native)
        {
            DeclaringFunction = declaringFunction;

            Name = Native.Name;
            Type = ResolveType(Native.ParameterType, Native);
        }

        public override string GetPath()
        {
            return $"{DeclaringFunction.GetPath()}.{Name} : {Type.GetPath()}";
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