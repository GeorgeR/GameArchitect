namespace GameArchitect.Design.Support.Attributes.Editor
{
    public abstract class EditorSwitchAttributeBase : SwitchAttributeBase
    {
        protected override string Key { get; } = "Editor";
    }
}