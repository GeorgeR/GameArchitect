using System;
using GameArchitect.Design.CXX.Metadata;
using GameArchitect.Design.Unreal.Attributes;

namespace GameArchitect.Design.Unreal.Metadata
{
    public class UnrealTypeInfo : CXXTypeInfo
    {
        public bool ForBlueprint { get; private set; } = true;

        public UnrealTypeInfo(Type native) : base(native)
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