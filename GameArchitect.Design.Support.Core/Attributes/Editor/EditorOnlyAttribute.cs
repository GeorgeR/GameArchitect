using System;

namespace GameArchitect.Design.Support.Attributes.Editor
{
    [AttributeUsage(AttributeTargets.All)]
    public class EditorOnlyAttribute : EditorSwitchAttributeBase
    {
        protected override SwitchType Type { get; } = SwitchType.Exclusive;
    }
}