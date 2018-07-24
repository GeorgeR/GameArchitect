using System;

namespace GameArchitect.Design.Support.Attributes.Editor
{
    [AttributeUsage(AttributeTargets.All)]
    public class EditorExcludeAttribute : EditorSwitchAttributeBase
    {
        protected override BooleanOperation Operation { get; } = BooleanOperation.Subtract;
    }
}