using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.Unreal.Metadata
{
    public sealed class UnrealEventInfo : EventInfoBase<UnrealTypeInfo, UnrealParameterInfo>
    {
        public UnrealEventInfo(UnrealMetadataProvider metadataProvider, UnrealTypeInfo declaringType, System.Reflection.EventInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}