using System;
using System.Reflection;
using GameArchitect.Design.Metadata;
using EventInfo = System.Reflection.EventInfo;
using ParameterInfo = System.Reflection.ParameterInfo;
using PropertyInfo = System.Reflection.PropertyInfo;

namespace GameArchitect.Design.Unreal.Metadata
{
    public sealed class UnrealMetadataProvider : MetadataProviderBase<UnrealTypeInfo, UnrealPropertyInfo, UnrealEventInfo, UnrealFunctionInfo, UnrealParameterInfo>
    {
        public UnrealMetadataProvider(IServiceProvider container) 
            : base(container) { }

        public override UnrealTypeInfo Create(Type type)
        {
            return TypeCache.GetOrAdd(type, t => new UnrealTypeInfo(this, t));
        }

        public override UnrealPropertyInfo Create(UnrealTypeInfo declaringType, PropertyInfo propertyInfo)
        {
            return PropertyCache.GetOrAdd(propertyInfo, p => new UnrealPropertyInfo(this, declaringType, p));
        }

        public override UnrealEventInfo Create(UnrealTypeInfo declaringType, EventInfo eventInfo)
        {
            return EventCache.GetOrAdd(eventInfo, e => new UnrealEventInfo(this, declaringType, e));
        }

        public override UnrealFunctionInfo Create(UnrealTypeInfo declaringType, MethodInfo methodInfo)
        {
            return FunctionCache.GetOrAdd(methodInfo, m => new UnrealFunctionInfo(this, declaringType, m));
        }

        public override UnrealParameterInfo Create(IMemberInfo declaringMember, UnrealTypeInfo declaringType, ParameterInfo parameterInfo)
        {
            return ParameterCache.GetOrAdd(parameterInfo, p => new UnrealParameterInfo(this, declaringMember, declaringType, p));
        }
    }
}