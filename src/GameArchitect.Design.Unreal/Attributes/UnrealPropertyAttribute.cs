using System;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class UnrealPropertyAttribute : Attribute
    {
        public bool ForBlueprint { get; } = true;
    }
}