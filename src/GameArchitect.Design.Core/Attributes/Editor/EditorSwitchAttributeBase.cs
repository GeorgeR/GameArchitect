namespace GameArchitect.Design.Attributes.Editor
{
    public abstract class EditorSwitchAttributeBase : SwitchAttributeBase
    {
        protected override string Key { get; } = "Editor";
    }
}