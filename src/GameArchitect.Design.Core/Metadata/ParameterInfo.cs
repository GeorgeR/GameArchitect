using System.Linq;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IParameterInfo : IMemberInfo<System.Reflection.ParameterInfo>
    {
        IMemberInfo DeclaringMember { get; }
    }

    public class ParameterInfo : MemberInfoBase<System.Reflection.ParameterInfo>, IParameterInfo
    {
        public override string TypeName { get; } = "Parameter";

        public IMemberInfo DeclaringMember { get; }

        public ParameterInfo(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo native) 
            : base(declaringType, native)
        {
            DeclaringMember = declaringMember;

            Name = Native.Name;
            Type = ResolveType(Native.ParameterType, Native);
        }

        public override string GetPath()
        {
            return $"{DeclaringMember.GetPath()}.{Name} : {Type.GetPath()}";
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