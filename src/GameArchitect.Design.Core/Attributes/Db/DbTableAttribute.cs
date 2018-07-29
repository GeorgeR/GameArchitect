using System;
using System.Linq;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes.Db
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class DbTableAttribute : ValidatableAttribute
    {
        public string Name { get; }

        public DbTableAttribute(string name)
        {
            Name = name;
        }

        public override bool IsValid<TMeta>(ILogger<IValidatable> logger, TMeta info)
        {
            base.IsValid(logger, info);

            ForMeta<TypeInfo>(info, o =>
            {
                if (!o.GetProperties().Any(p => p.HasAttribute<DbKeyAttribute>()))
                    logger.LogError($"The type {o.GetPath()} has a DbTable attribute but no DbKey was specified.");
            });

            return true;
        }
    }
}