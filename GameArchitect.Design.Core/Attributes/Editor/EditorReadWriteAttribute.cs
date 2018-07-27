namespace GameArchitect.Design.Attributes.Editor
{
    public class EditorReadWriteAttribute : PermissionAttributeBase
    {
        protected override Permission Permission { get; } = Permission.ReadWrite;
        protected override string Key { get; } = "Editor";
    }
}