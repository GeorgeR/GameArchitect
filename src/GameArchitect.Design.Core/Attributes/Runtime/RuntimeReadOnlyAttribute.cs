namespace GameArchitect.Design.Attributes.Runtime
{
    public class RuntimeReadOnlyAttribute : PermissionAttributeBase
    {
        protected override Permission Permission { get; } = Permission.Read;
        protected override string Key { get; } = "Runtime";
    }
}