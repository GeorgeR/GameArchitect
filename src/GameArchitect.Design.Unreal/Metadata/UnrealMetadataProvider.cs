using System;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Unreal.Metadata
{
    public class UnrealMetadataProvider : DefaultMetadataProvider
    {
        public UnrealMetadataProvider(
            ILoggerFactory logFactory, 
            INameTransformer nameTransformer,
            ITypeTransformer typeTransformer) 
            : base(logFactory, nameTransformer, typeTransformer)
        {
        }

        public override ITypeInfo Create(Type type)
        {
            return new UnrealTypeInfo(type);
        }

        public override IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return new UnrealPropertyInfo(declaringType, propertyInfo);
        }
    }
}