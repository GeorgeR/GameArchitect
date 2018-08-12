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

    public interface IFunctionInfo<TParamterInfo> : IFunctionInfo
        where TParamterInfo : IParameterInfo
    {
        new IList<TParamterInfo> Parameters { get; }
    }

    public abstract class FunctionInfoBase<TTypeInfo, TParameterInfo>
        : MemberInfoBase<TTypeInfo, System.Reflection.MethodInfo>,
        IFunctionInfo<TParameterInfo>
        where TTypeInfo : class, ITypeInfo
        where TParameterInfo : class, IParameterInfo
    {
        protected FunctionInfoBase(IMetadataProvider metadataProvider, TTypeInfo declaringType, System.Reflection.MethodInfo native)
            : base(metadataProvider, declaringType, native)
        {
            Name = native.Name;
            Type = ResolveType(native.ReturnType, native);

            Mutability = native.GetCustomAttribute<ImmutableAttribute>() != null ? Mutability.Immutable : Mutability.Mutable;
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
                        .Select(o => MetadataProvider.Create(this, DeclaringType, o)));

                return _parameters;
            }
        }
        IList<TParameterInfo> IFunctionInfo<TParameterInfo>.Parameters => Parameters.Cast<TParameterInfo>().ToList();

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

    public sealed class FunctionInfo : FunctionInfoBase<TypeInfo, ParameterInfo>
    {
        public FunctionInfo(IMetadataProvider metadataProvider, TypeInfo declaringType, MethodInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}