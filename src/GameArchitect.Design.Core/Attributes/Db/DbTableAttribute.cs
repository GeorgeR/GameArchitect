using System;
using System.Linq;
using GameArchitect.Design.Metadata;

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

        public override bool IsValid<TMeta>(TMeta info)
        {
            base.IsValid(info);

            ForMeta<TypeInfo>(info, o =>
            {
                if (!o.GetProperties().Any(p => p.HasAttribute<DbKeyAttribute>()))
                    throw new Exception($"The type {o.GetPath()} has a DbTable attribute but no DbKey was specified.");
            });

            return true;
        }
    }
}