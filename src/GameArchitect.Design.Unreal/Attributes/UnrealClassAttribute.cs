using System;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class UnrealClassAttribute : UnrealTypeAttributeBase
    {
        public override string BaseClassName { get; } = "Object";
    }
}