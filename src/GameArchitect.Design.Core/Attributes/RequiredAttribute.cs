using System;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Indicates the property is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class RequiredAttribute : ValidatableAttribute
    {
        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            if(info.HasAttribute<OptionalAttribute>())
                logger.LogError($"Property {info.GetPath()} was marked as Required but also has an Optional attribute.");

            return true;
        }
    }
}