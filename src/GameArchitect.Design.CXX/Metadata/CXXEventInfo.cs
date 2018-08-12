using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.CXX.Metadata
{
    public sealed class CXXEventInfo : EventInfoBase<CXXTypeInfo, CXXParameterInfo>
    {
        public CXXEventInfo(CXXMetadataProvider metadataProvider, CXXTypeInfo declaringType, System.Reflection.EventInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}