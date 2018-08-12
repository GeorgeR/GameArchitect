using System;
using System.Reflection;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public sealed class CXXMetadataProvider : MetadataProviderBase<CXXTypeInfo, CXXPropertyInfo, CXXEventInfo, CXXFunctionInfo, CXXParameterInfo>
    {
        public CXXMetadataProvider(IServiceProvider container) 
            : base(container) { }

        public override CXXTypeInfo Create(Type type)
        {
            return TypeCache.GetOrAdd(type, t => new CXXTypeInfo(this, t));
        }

        public override CXXPropertyInfo Create(CXXTypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return PropertyCache.GetOrAdd(propertyInfo, p => new CXXPropertyInfo(this, declaringType, p));
        }

        public override CXXEventInfo Create(CXXTypeInfo declaringType, System.Reflection.EventInfo eventInfo)
        {
            return EventCache.GetOrAdd(eventInfo, e => new CXXEventInfo(this, declaringType, e));
        }

        public override CXXFunctionInfo Create(CXXTypeInfo declaringType, MethodInfo methodInfo)
        {
            return FunctionCache.GetOrAdd(methodInfo, m => new CXXFunctionInfo(this, declaringType, m));
        }

        public override CXXParameterInfo Create(IMemberInfo declaringMember, CXXTypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo)
        {
            return ParameterCache.GetOrAdd(parameterInfo, p => new CXXParameterInfo(this, declaringMember, declaringType, p));
        }
    }
}