using System;

namespace GameArchitect.Design.Support.Attributes.Client
{
    [AttributeUsage(AttributeTargets.All)]
    public class ClientOnlyAttribute : ClientSwitchAttributeBase
    {
        protected override SwitchType Type { get; } = SwitchType.Exclusive;
    }
}