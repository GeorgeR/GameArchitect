using System;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    /// <summary>
    /// Indicates that the property or parameter is optional.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class OptionalAttribute : ValidatableAttribute
    {
        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            if (info.HasAttribute<RequiredAttribute>())
                logger.LogError($"Property {info.GetPath()} was marked as Optional but also has an Required attribute.");

            return true;
        }
    }
}