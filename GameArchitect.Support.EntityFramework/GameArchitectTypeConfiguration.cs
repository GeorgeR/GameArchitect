using System;
using System.Linq;
using GameArchitect.Design;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Attributes.Db;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Metadata.Extensions;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameArchitect.Support.EntityFramework
{
    public class GameArchitectTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if(!typeof(T).HasAttribute<ExportAttribute>())
                throw new Exception($"Type {typeof(T).Name} must contain an [Export] attribute to be configured here.");

            var typeInfo = new TypeInfo(typeof(T));

            var properties = typeInfo
                .GetProperties()
                .WithAttribute<DbPropertyAttribute>()
                .OrderBy(o => o.GetAttribute<DbPropertyAttribute>().Index);

            properties.OfType<PropertyInfo>().ForEach(p =>
            {
                if (p.HasAttribute<DbReferenceAttribute>())
                {
                    var attr = p.GetAttribute<DbReferenceAttribute>();
                    if (p.CollectionType != CollectionType.None)
                    {
                        var collectionNavigationBuilder = builder.HasMany(p.Type.Name);
                        collectionNavigationBuilder.WithOne();
                    }
                    else
                    {
                        var referenceNavigationBuilder = builder.HasOne(p.Name);
                        if (attr.Multiplicity == DbReferenceMultiplicity.Many)
                        {
                            var manyBuilder = referenceNavigationBuilder.WithMany();
                            if (!p.IsOptional)
                                manyBuilder.IsRequired();
                        }
                        else if (attr.Multiplicity == DbReferenceMultiplicity.One)
                        {
                            var oneBuilder = referenceNavigationBuilder.WithOne();
                            if (!p.IsOptional)
                                oneBuilder.IsRequired();
                        }
                    }
                }
                else
                {
                    var propertyBuilder = builder.Property(p.Name);
                    if (!p.IsOptional)
                        propertyBuilder.IsRequired();

                    if (p.HasAttribute<RangeAttribute>() && p.Type.Native == typeof(string))
                    {
                        var attr = p.GetAttribute<RangeAttribute>();
                        if (attr.HasMaximum())
                            propertyBuilder.HasMaxLength(attr.GetMaximum<int>());
                    }
                }
            });

            var keyProperties = properties.WithAttribute<DbKeyAttribute>();
            if(!keyProperties.Any())
                throw new Exception($"Type {typeInfo.GetPath()} doesn't have any properties marked [DbKey].");
            
            builder.HasKey(keyProperties.Select(o => o.Name).ToArray());
        }
    }
}