using System;
using System.Collections.Concurrent;
using System.Reflection;
using GameArchitect.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Design.Metadata
{
    //public interface IMetadataProvider
    //{
    //    TTypeInfo Create<TTypeInfo>(Type type) where TTypeInfo : class, ITypeInfo;
    //    TPropertyInfo Create<TPropertyInfo>(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo) where TPropertyInfo : class, IPropertyInfo;
    //    TEventInfo Create<TEventInfo>(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo) where TEventInfo : class, IEventInfo;
    //    TFunctionInfo Create<TFunctionInfo>(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo) where TFunctionInfo : class, IFunctionInfo;
    //    TParameterInfo Create<TParameterInfo>(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo) where TParameterInfo : class, IParameterInfo;

    //ITypeInfo Create(Type type);
    //IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
    //IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
    //IFunctionInfo Create(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo);
    //IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);
    //}

    public interface IMetadataProvider
    {
        ITypeInfo Create(Type type);
        IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        IFunctionInfo Create(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo);
        IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);

        //TTypeInfo Create<TTypeInfo>(Type type) where TTypeInfo : class, ITypeInfo;
        //TPropertyInfo Create<TPropertyInfo>(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo) where TPropertyInfo : class, IPropertyInfo;
        //TEventInfo Create<TEventInfo>(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo) where TEventInfo : class, IEventInfo;
        //TFunctionInfo Create<TFunctionInfo>(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo) where TFunctionInfo : class, IFunctionInfo;
        //TParameterInfo Create<TParameterInfo>(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo) where TParameterInfo : class, IParameterInfo;
    }

    public interface IMetadataProvider<TTypeInfo, out TPropertyInfo, out TEventInfo, out TFunctionInfo, out TParameterInfo>
        : IMetadataProvider
        where TTypeInfo : class, ITypeInfo
        where TPropertyInfo : class, IPropertyInfo
        where TEventInfo : class, IEventInfo 
        where TFunctionInfo : class, IFunctionInfo
        where TParameterInfo : class, IParameterInfo
    {
        new TTypeInfo Create(Type type);
        TPropertyInfo Create(TTypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        TEventInfo Create(TTypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        TFunctionInfo Create(TTypeInfo declaringType, System.Reflection.MethodInfo methodInfo);
        TParameterInfo Create(IMemberInfo declaringMember, TTypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);

        //ITypeInfo Create(Type type);
        //IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        //IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        //IFunctionInfo Create(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo);
        //IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);
    }

    public abstract class MetadataProviderBase<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo, TParameterInfo> 
        : IMetadataProvider<TTypeInfo, TPropertyInfo, TEventInfo, TFunctionInfo, TParameterInfo>, 
        IServiceConfiguration
        where TTypeInfo : class, ITypeInfo
        where TPropertyInfo : class, IPropertyInfo
        where TEventInfo : class, IEventInfo
        where TFunctionInfo : class, IFunctionInfo
        where TParameterInfo : class, IParameterInfo
    {
        protected IServiceProvider Container { get; set; }

        protected ConcurrentDictionary<Type, TTypeInfo> TypeCache { get; } = new ConcurrentDictionary<Type, TTypeInfo>();
        protected ConcurrentDictionary<System.Reflection.PropertyInfo, TPropertyInfo> PropertyCache { get; } = new ConcurrentDictionary<System.Reflection.PropertyInfo, TPropertyInfo>();
        protected ConcurrentDictionary<System.Reflection.EventInfo, TEventInfo> EventCache { get; } = new ConcurrentDictionary<System.Reflection.EventInfo, TEventInfo>();
        protected ConcurrentDictionary<System.Reflection.MethodInfo, TFunctionInfo> FunctionCache { get; } = new ConcurrentDictionary<MethodInfo, TFunctionInfo>();
        protected ConcurrentDictionary<System.Reflection.ParameterInfo, TParameterInfo> ParameterCache { get; } = new ConcurrentDictionary<System.Reflection.ParameterInfo, TParameterInfo>();

        protected MetadataProviderBase(IServiceProvider container)
        {
            Container = container;
        }

        public virtual void Setup(IServiceCollection services) { }

        //public abstract ITypeInfo Create(Type type);
        //public abstract IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        //public abstract IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        //public abstract IFunctionInfo Create(ITypeInfo declaringType, MethodInfo methodInfo);
        //public abstract IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);

        public abstract TTypeInfo Create(Type type);
        public abstract TPropertyInfo Create(TTypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo);
        public abstract TEventInfo Create(TTypeInfo declaringType, System.Reflection.EventInfo eventInfo);
        public abstract TFunctionInfo Create(TTypeInfo declaringType, MethodInfo methodInfo);
        public abstract TParameterInfo Create(IMemberInfo declaringMember, TTypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo);

        ITypeInfo IMetadataProvider.Create(Type type)
        {
            return Create(type);
        }

        IPropertyInfo IMetadataProvider.Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return Create((TTypeInfo) declaringType, propertyInfo);
        }

        IEventInfo IMetadataProvider.Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo)
        {
            return Create((TTypeInfo) declaringType, eventInfo);
        }

        IFunctionInfo IMetadataProvider.Create(ITypeInfo declaringType, System.Reflection.MethodInfo methodInfo)
        {
            return Create((TTypeInfo) declaringType, methodInfo);
        }

        IParameterInfo IMetadataProvider.Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo)
        {
            return Create(declaringMember, (TTypeInfo) declaringType, parameterInfo);
        }
    }
}