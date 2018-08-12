using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public sealed class CXXParameterInfo : ParameterInfoBase<CXXTypeInfo>
    {
        public CXXParameterInfo(CXXMetadataProvider metadataProvider, IMemberInfo declaringMember, CXXTypeInfo declaringType, System.Reflection.ParameterInfo native) 
            : base(metadataProvider, declaringMember, declaringType, native) { }
    }
}