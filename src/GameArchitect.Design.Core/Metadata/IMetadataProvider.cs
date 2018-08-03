using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IMetadataProvider
    {
        ITypeInfo Create(Type type);
        IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        IFunctionInfo Create(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo);
        IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);
    }

    public abstract class MetadataProviderBase : IMetadataProvider
    {
        protected ILoggerFactory LogFactory { get; }

        protected INameTransformer NameTransformer { get; }
        protected ITypeTransformer TypeTransformer { get; }

        protected MetadataProviderBase(
            ILoggerFactory logFactory, 
            INameTransformer nameTransformer, 
            ITypeTransformer typeTransformer)
        {
            LogFactory = logFactory;

            NameTransformer = nameTransformer;
            TypeTransformer = typeTransformer;
        }

        public abstract ITypeInfo Create(Type type);
        public abstract IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        public abstract IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        public abstract IFunctionInfo Create(ITypeInfo declaringType, MethodInfo methodInfo);
        public abstract IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);
    }
}