namespace GameArchitect.Design.Support.Attributes.Editor
{
    public class EditorReadWriteAttribute : AccessAttributeBase
    {
        protected override Permission Permission => Permission.ReadWrite;
    }
}