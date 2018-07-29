using System;

namespace GameArchitect.Design.Attributes.Editor
{
    [AttributeUsage(AttributeTargets.All)]
    public class EditorOnlyAttribute : EditorSwitchAttributeBase
    {
        protected override SwitchType Type { get; } = SwitchType.Exclusive;
    }
}