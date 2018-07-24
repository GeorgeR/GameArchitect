using System;

namespace GameArchitect.Design.Support.Attributes.Client
{
    [AttributeUsage(AttributeTargets.All)]
    public class ClientExcludeAttribute : ClientSwitchAttributeBase
    {
        protected override BooleanOperation Operation { get; } = BooleanOperation.Subtract;
    }
}