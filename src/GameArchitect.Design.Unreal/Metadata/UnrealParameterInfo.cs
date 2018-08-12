using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.Unreal.Metadata
{
    public sealed class UnrealParameterInfo : ParameterInfoBase<UnrealTypeInfo>
    {
        public UnrealParameterInfo(UnrealMetadataProvider metadataProvider, IMemberInfo declaringMember, UnrealTypeInfo declaringType, System.Reflection.ParameterInfo native) 
            : base(metadataProvider, declaringMember, declaringType, native) { }
    }
}