using System;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.CXX.Metadata
{
    public class CXXMetadataProvider : DefaultMetadataProvider
    {
        public CXXMetadataProvider(
            ILoggerFactory logFactory,
            INameTransformer nameTransformer,
            ITypeTransformer typeTransformer)
            : base(logFactory, nameTransformer, typeTransformer) { }

        public override TTypeInfo Create<TTypeInfo>(Type type)
        {
            return new CXXTypeInfo(type) as TTypeInfo;
        }

        public override TPropertyInfo Create<TPropertyInfo>(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return new CXXPropertyInfo(declaringType, propertyInfo) as TPropertyInfo;
        }
    }
}