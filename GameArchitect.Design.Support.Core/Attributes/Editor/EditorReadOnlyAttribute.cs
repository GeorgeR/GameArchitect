namespace GameArchitect.Design.Support.Attributes.Editor
{
    public class EditorReadOnlyAttribute : AccessAttributeBase
    {
        protected override Permission Permission => Permission.Read;
    }
}