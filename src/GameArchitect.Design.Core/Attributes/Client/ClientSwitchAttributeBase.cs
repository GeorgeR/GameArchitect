namespace GameArchitect.Design.Attributes.Client
{
    public abstract class ClientSwitchAttributeBase : SwitchAttributeBase
    {
        protected override string Key { get; } = "Client";
    }
}