using System;

namespace GameArchitect.Design.Attributes.Server
{
    [AttributeUsage(AttributeTargets.All)]
    public class ServerExcludeAttribute : ServerSwitchAttributeBase
    {
        protected override BooleanOperation Operation { get; } = BooleanOperation.Subtract;
    }
}