namespace GameArchitect.Design.Attributes.Editor
{
    public class EditorReadOnlyAttribute : PermissionAttributeBase
    {
        protected override Permission Permission { get; } = Permission.Read;
        protected override string Key { get; } = "Editor";
    }
}