namespace GameArchitect.Design.Support.Attributes.Runtime
{
    public class RuntimeReadWriteAttribute : AccessAttributeBase
    {
        protected override Permission Permission => Permission.ReadWrite;
    }
}