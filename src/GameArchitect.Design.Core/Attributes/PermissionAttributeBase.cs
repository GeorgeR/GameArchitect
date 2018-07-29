using System;
using System.Linq;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    public abstract class PermissionAttributeBase : ValidatableAttribute
    {
        protected virtual Permission Permission { get; }
        protected abstract string Key { get; } // Case insensitive

        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            ForMeta<PropertyInfo>(info, o =>
            {
                var permissionAttributes = o.GetAttributes().OfType<PermissionAttributeBase>();
                if(permissionAttributes.Count() > 1)
                    logger.LogError($"More than one Permission attribute is specified with key {Key} for property {o.GetPath()}.");
            });

            return true;
        }
    }
}