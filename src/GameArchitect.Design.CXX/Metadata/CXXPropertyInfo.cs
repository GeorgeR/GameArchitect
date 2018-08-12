using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public sealed class CXXPropertyInfo : PropertyInfoBase<CXXTypeInfo>
    {
        public CXXPropertyInfo(CXXMetadataProvider metadataProvider, CXXTypeInfo declaringType, System.Reflection.PropertyInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}