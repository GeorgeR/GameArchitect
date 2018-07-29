using System;
using System.Linq;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design.Attributes.Db
{
    /// <summary>
    /// Is the type refererred to associated with anything other than this?
    /// If so it's Many, if not it's One
    /// </summary>
    public enum DbReferenceMultiplicity
    {
        One,
        Many
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class DbReferenceAttribute : DbPropertyAttribute, IDelegatedValidatable
    {
        public DbReferenceMultiplicity Multiplicity { get; }

        public DbReferenceAttribute(DbReferenceMultiplicity multiplicity = DbReferenceMultiplicity.Many)
        {
            Multiplicity = multiplicity;
        }

        public PropertyInfo GetReferencedKey(PropertyInfo property)
        {
            //IsValid(property);
            return property.Type.GetProperties().FirstOrDefault(p => p.HasAttribute<DbKeyAttribute>()); 
        }

        public bool IsValid<TMeta>(ILogger<IValidatable> logger,TMeta info) where TMeta : IMetaInfo
        {
            // NOTE: This currently assumes a singular (non composite) key
            if (info is PropertyInfo)
            {
                var propertyInfo = info as PropertyInfo;
                if (!propertyInfo.Type.GetProperties().Any(p => p.HasAttribute<DbKeyAttribute>()))
                    logger.LogError($"A DbReference attribute was specified for type {propertyInfo.Type.GetPath()}, however this type doesn't have and DbKey attribute so it doesn't know how to reference it!");
            }

            return true;
        }
    }
}