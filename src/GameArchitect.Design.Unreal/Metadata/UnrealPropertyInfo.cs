﻿using GameArchitect.Design.Attributes.Editor;
using GameArchitect.Design.Attributes.Runtime;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;

namespace GameArchitect.Design.Unreal.Metadata
{
    public sealed class UnrealPropertyInfo : PropertyInfoBase<UnrealTypeInfo>
    {
        public bool ForBlueprint { get; private set; } = true;
        public UnrealPropertyFlags BlueprintFlag { get; private set; } = UnrealPropertyFlags.BlueprintReadOnly;
        public UnrealPropertyFlags EditorFlag { get; private set; } = UnrealPropertyFlags.VisibleAnywhere;

        public UnrealPropertyInfo(UnrealMetadataProvider metadataProvider, UnrealTypeInfo declaringType, System.Reflection.PropertyInfo native) 
            : base(metadataProvider, declaringType, native)
        {
            ForAttribute<UnrealPropertyAttribute>(attr =>
            {
                ForBlueprint = attr.ForBlueprint;
            });

            if (Permission.HasFlag(Permission.Write))
            {
                BlueprintFlag = UnrealPropertyFlags.BlueprintReadWrite;
                EditorFlag = UnrealPropertyFlags.EditAnywhere;
            }

            ForAttribute<EditorReadOnlyAttribute>(attr =>
            {
                EditorFlag = UnrealPropertyFlags.VisibleAnywhere;
            });

            ForAttribute<EditorReadWriteAttribute>(attr =>
            {
                EditorFlag = UnrealPropertyFlags.EditAnywhere;
            });

            ForAttribute<RuntimeReadOnlyAttribute>(attr =>
            {
                BlueprintFlag = UnrealPropertyFlags.BlueprintReadOnly;
            });

            ForAttribute<RuntimeReadWriteAttribute>(attr =>
            {
                BlueprintFlag = UnrealPropertyFlags.BlueprintReadWrite;
            });
        }
    }
}