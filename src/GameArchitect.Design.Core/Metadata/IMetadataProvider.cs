using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public interface IMetadataProvider
    {
        TTypeInfo Create<TTypeInfo>(Type type) where TTypeInfo : class, ITypeInfo;
        TPropertyInfo Create<TPropertyInfo>(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo) where TPropertyInfo : class, IPropertyInfo;
        TEventInfo Create<TEventInfo>(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo) where TEventInfo : class, IEventInfo;
        TFunctionInfo Create<TFunctionInfo>(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo) where TFunctionInfo : class, IFunctionInfo;
        TParameterInfo Create<TParameterInfo>(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo) where TParameterInfo : class, IParameterInfo;

        //ITypeInfo Create(Type type);
        //IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        //IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        //IFunctionInfo Create(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo);
        //IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);
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

        //public abstract ITypeInfo Create(Type type);
        //public abstract IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        //public abstract IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        //public abstract IFunctionInfo Create(ITypeInfo declaringType, MethodInfo methodInfo);
        //public abstract IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);

        public abstract TTypeInfo Create<TTypeInfo>(Type type) where TTypeInfo : class, ITypeInfo;
        public abstract TPropertyInfo Create<TPropertyInfo>(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo) where TPropertyInfo : class, IPropertyInfo;
        public abstract TEventInfo Create<TEventInfo>(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo) where TEventInfo : class, IEventInfo;
        public abstract TFunctionInfo Create<TFunctionInfo>(ITypeInfo declaringType, MethodInfo methodInfo) where TFunctionInfo : class, IFunctionInfo;
        public abstract TParameterInfo Create<TParameterInfo>(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo) where TParameterInfo : class, IParameterInfo;
    }
}