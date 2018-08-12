using System.Linq;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IParameterInfo : IMemberInfo<System.Reflection.ParameterInfo>
    {
        IMemberInfo DeclaringMember { get; }
    }

    public abstract class ParameterInfoBase<TTypeInfo> 
        : MemberInfoBase<TTypeInfo, System.Reflection.ParameterInfo>, 
        IParameterInfo
        where TTypeInfo : class, ITypeInfo
    {
        public IMemberInfo DeclaringMember { get; }

        protected ParameterInfoBase(IMetadataProvider metadataProvider, IMemberInfo declaringMember, TTypeInfo declaringType, System.Reflection.ParameterInfo native)
            : base(metadataProvider, declaringType, native)
        {
            DeclaringMember = declaringMember;

            Name = native.Name;
            Type = ResolveType(Native.ParameterType, native);
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

    public sealed class ParameterInfo : ParameterInfoBase<TypeInfo>
    {
        public ParameterInfo(IMetadataProvider metadataProvider, IMemberInfo declaringMember, TypeInfo declaringType, System.Reflection.ParameterInfo native) 
            : base(metadataProvider, declaringMember, declaringType, native) { }
    }
}