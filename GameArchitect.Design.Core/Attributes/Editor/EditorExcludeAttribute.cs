using System;

namespace GameArchitect.Design.Attributes.Editor
{
    [AttributeUsage(AttributeTargets.All)]
    public class EditorExcludeAttribute : EditorSwitchAttributeBase
    {
        protected override BooleanOperation Operation { get; } = BooleanOperation.Subtract;
    }
}