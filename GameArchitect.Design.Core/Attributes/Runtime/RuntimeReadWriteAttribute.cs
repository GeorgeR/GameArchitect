namespace GameArchitect.Design.Attributes.Runtime
{
    public class RuntimeReadWriteAttribute : PermissionAttributeBase
    {
        protected override Permission Permission { get; } = Permission.ReadWrite;
        protected override string Key { get; } = "Runtime";
    }
}