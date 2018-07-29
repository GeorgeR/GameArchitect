using System;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public abstract class UnrealTypeAttributeBase : Attribute
    {
        public virtual string BaseClassName { get; } = string.Empty;
        public bool AsBaseClass { get; } = false;
    }
}