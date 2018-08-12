using System;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;

namespace GameArchitect.Design.Unreal.Metadata
{
    public sealed class UnrealTypeInfo : TypeInfoBase<UnrealTypeInfo, UnrealPropertyInfo, UnrealEventInfo, UnrealFunctionInfo>
    {
        public bool AsBaseClass { get; private set; }
        public bool ForBlueprint { get; private set; } = true;

        public UnrealTypeInfo(UnrealMetadataProvider metadataProvider, Type native) 
            : base(metadataProvider, native)
        {
            ForAttribute<UnrealTypeAttributeBase>(attr =>
            {
                ForBlueprint = attr.ForBlueprint;

                //BaseTypes.Add(new UnrealTypeInfo(null, null, attr.BaseClassName));
                AsBaseClass = attr.AsBaseClass;
            });
        }
    }
}