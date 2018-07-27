using System;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// An internal development note that can be emitted to generated code, 
    /// for example to remind you of an implementation detail.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class NoteAttribute : Attribute
    {
        public string Text { get; }

        public NoteAttribute(string text)
        {
            Text = text;
        }
    }
}