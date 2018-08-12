using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Design.Metadata
{
    public sealed class DefaultMetadataProvider 
        : MetadataProviderBase<TypeInfo, PropertyInfo, EventInfo, FunctionInfo, ParameterInfo>
    {
        public DefaultMetadataProvider(IServiceProvider container) 
            : base(container) { }

        public override void Setup(IServiceCollection services)
        {
            base.Setup(services);

            services.AddSingleton<IMetadataProvider, DefaultMetadataProvider>();
        }

        public override TypeInfo Create(Type type)
        {
            return TypeCache.GetOrAdd(type, t => new TypeInfo(this, t));
        }

        public override PropertyInfo Create(TypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return PropertyCache.GetOrAdd(propertyInfo, p => new PropertyInfo(this, declaringType, p));
        }

        public override EventInfo Create(TypeInfo declaringType, System.Reflection.EventInfo eventInfo)
        {
            return EventCache.GetOrAdd(eventInfo, e => new EventInfo(this, declaringType, e));
        }

        public override FunctionInfo Create(TypeInfo declaringType, MethodInfo methodInfo)
        {
            return FunctionCache.GetOrAdd(methodInfo, m => new FunctionInfo(this, declaringType, m));
        }

        public override ParameterInfo Create(IMemberInfo declaringMember, TypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo)
        {
            return ParameterCache.GetOrAdd(parameterInfo, p => new ParameterInfo(this, declaringMember, declaringType, p));
        }
    }
}