using System;

namespace GameArchitect.Design.Support.Attributes.Server
{
    [AttributeUsage(AttributeTargets.All)]
    public class ServerOnlyAttribute : ServerSwitchAttributeBase
    {
        protected override SwitchType Type { get; } = SwitchType.Exclusive;
    }
}