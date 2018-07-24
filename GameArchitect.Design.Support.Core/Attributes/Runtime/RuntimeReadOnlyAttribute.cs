namespace GameArchitect.Design.Support.Attributes.Runtime
{
    public class RuntimeReadOnlyAttribute : AccessAttributeBase
    {
        protected override Permission Permission => Permission.Read;
    }
}