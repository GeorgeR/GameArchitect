using System;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// A description that can then be emitted to generated code files.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class DescriptionAttribute : Attribute
    {
        public string Text { get; }

        public DescriptionAttribute(string text)
        {
            Text = text;
        }
    }
}