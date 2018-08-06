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
            : base(logFactory, nameTransformer, typeTransformer) { }

        public override TTypeInfo Create<TTypeInfo>(Type type)
        {
            return new UnrealTypeInfo(type) as TTypeInfo;
        }

        public override TPropertyInfo Create<TPropertyInfo>(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return new UnrealPropertyInfo(declaringType, propertyInfo) as TPropertyInfo;
        }
    }
}