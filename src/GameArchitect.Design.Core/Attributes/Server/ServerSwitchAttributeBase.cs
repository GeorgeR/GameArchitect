namespace GameArchitect.Design.Attributes.Server
{
    public abstract class ServerSwitchAttributeBase : SwitchAttributeBase
    {
        protected override string Key { get; } = "Server";
    }
}