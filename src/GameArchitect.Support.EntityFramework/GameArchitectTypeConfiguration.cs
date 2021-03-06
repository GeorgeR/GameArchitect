﻿using System;
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
using Microsoft.EntityFrameworkCore.Storage;

namespace GameArchitect.Support.EntityFramework
{
    public class GameArchitectTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        private IMetadataProvider MetadataProvider { get; }
        private ITypeMappingSource TypeMappingSource { get; }
        
        public GameArchitectTypeConfiguration(IMetadataProvider metadataProvider, ITypeMappingSource typeMappingSource)
        {
            MetadataProvider = metadataProvider;
            TypeMappingSource = typeMappingSource;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if(!typeof(T).HasAttribute<ExportAttribute>())
                throw new Exception($"Type {typeof(T).Name} must contain an [Export] attribute to be configured here.");

            var typeInfo = MetadataProvider.Create(typeof(T));

            var properties = typeInfo
                .Properties
                .WithAttribute<DbPropertyAttribute>()
                .OrderBy(o => o.GetAttribute<DbPropertyAttribute>().Index)
                .ToList();

            //MethodInfo propertyGenericMethod = null;

            properties.OfType<IPropertyInfo>().ForEach(p =>
            {
                if (p.HasAttribute<DbReferenceAttribute>())
                {
                    var attr = p.GetAttribute<DbReferenceAttribute>();
                    if (p.CollectionType != CollectionType.None)
                    {
                        var collectionNavigationBuilder = builder.HasMany(p.Type.Native, p.Name);
                        collectionNavigationBuilder.WithOne();
                    }
                    else
                    {
                        var referenceNavigationBuilder = builder.HasOne(p.Type.Native, p.Name);
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
                    var propertyTypeSupported = TypeMappingSource.FindMapping(p.Type.Native) != null;
                    if(!propertyTypeSupported)
                    {
                        var referenceOwnershipBuilder = builder.OwnsOne(p.Type.Native, p.Name);
                        
                        // TODO: Optional, range

                        //propertyBuilders.Add(builder.Property(p.Name));
                        //if (propertyGenericMethod == null)
                        //{
                        //    propertyGenericMethod = builder
                        //        .GetType()
                        //        .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        //        .FirstOrDefault(m => m.Name == "Property" && m.GetGenericArguments().Length == 1 && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(string));
                        //}

                        //var deconstructed = p.Deconstruct();
                        //if(deconstructed == null || !deconstructed.Any())
                        //    throw new Exception($"Attempted to deconstruct unsupported type {p.GetPath()} but deconstruction failed.");

                        //foreach (var d in deconstructed)
                        //{
                        //    var propertyConcreteMethod = propertyGenericMethod.MakeGenericMethod(d.Type.Native);
                        //    propertyConcreteMethod.Invoke(builder, new[] {d.Name});
                        //}
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
                }
            });

            var keyProperties = properties.WithAttribute<DbKeyAttribute>().ToList();
            if(!keyProperties.Any())
                throw new Exception($"Type {typeInfo.GetPath()} doesn't have any properties marked [DbKey].");
            
            builder.HasKey(keyProperties.Select(o => o.Name).ToArray());
        }
    }
}