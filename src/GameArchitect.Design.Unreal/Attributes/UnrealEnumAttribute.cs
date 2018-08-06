using System;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Unreal.Attributes
{
    [AttributeUsage(AttributeTargets.Enum)]
    public class UnrealEnumAttribute : ValidatableAttribute
    {
        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            ForMeta<ITypeInfo>(info, e =>
            {
                if(!e.Inherits<byte>())
                    logger.LogError($"An enum marked as UnrealEnum ({info.GetPath()}) must inherit from byte (uint8)");
            });

            return true;
        }
    }
}