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
            : base(logFactory, nameTransformer, typeTransformer)
        {
        }

        public override ITypeInfo Create(Type type)
        {
            return new CXXTypeInfo(type);
        }

        public override IPropertyInfo Create(ITypeInfo declaringType, System.Reflection.PropertyInfo propertyInfo)
        {
            return new CXXPropertyInfo(declaringType, propertyInfo);
        }
    }
}