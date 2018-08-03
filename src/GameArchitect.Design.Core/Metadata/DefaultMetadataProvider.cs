using System;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Metadata
{
    public class DefaultMetadataProvider : MetadataProviderBase
    {
        public DefaultMetadataProvider(
            ILoggerFactory logFactory, 
            INameTransformer nameTransformer,
            ITypeTransformer typeTransformer) 
            : base(logFactory, nameTransformer, typeTransformer) { }

        public override ITypeInfo Create(Type type)
        {
            return new TypeInfo(type);
        }

        public override IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return new PropertyInfo(declaringType, propertyInfo);
        }

        public override IEventInfo Create(ITypeInfo declaringType, System.Reflection.EventInfo eventInfo)
        {
            return new EventInfo(declaringType, eventInfo);
        }

        public override IFunctionInfo Create(ITypeInfo declaringType, MethodInfo methodInfo)
        {
            return new FunctionInfo(declaringType, methodInfo);
        }

        public override IParameterInfo Create(IMemberInfo declaringMember, ITypeInfo declaringType, System.Reflection.ParameterInfo parameterInfo)
        {
            return new ParameterInfo(declaringMember, declaringType, parameterInfo);
        }
    }
}