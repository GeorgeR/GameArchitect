using System.Reflection;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Design.Unreal.Metadata
{
    public sealed class UnrealFunctionInfo : FunctionInfoBase<UnrealTypeInfo, UnrealParameterInfo>
    {
        public UnrealFunctionInfo(UnrealMetadataProvider metadataProvider, UnrealTypeInfo declaringType, MethodInfo native) 
            : base(metadataProvider, declaringType, native) { }
    }
}