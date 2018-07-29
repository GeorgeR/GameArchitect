using System;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnrealFunctionAttribute : Attribute
    {
        public bool NeedsWorldContext { get; } = false;
        public bool NeedsDeclarer { get; } = false;
        public bool IsStatic { get; } = false;
    }
}