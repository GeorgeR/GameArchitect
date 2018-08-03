using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameArchitect.Design.Attributes;
using GameArchitect.Extensions.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace GameArchitect.Support.EntityFramework
{
    public abstract class GameArchitectDbContextBase : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typeMappingSource = this.GetService<ITypeMappingSource>();
            
            var configurationGenericType = typeof(GameArchitectTypeConfiguration<>);
            var applyGenericMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(o => o.Name == "ApplyConfiguration");
            
            var entityTypes = modelBuilder.Model.GetEntityTypes().Select(o => o.ClrType).Where(o => o.HasAttribute<ExportAttribute>());
            var configs = new List<Tuple<MethodInfo, object>>();
            foreach (var entityType in entityTypes)
            {
                var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(entityType);
                var configurationInstance = Activator.CreateInstance(
                    configurationGenericType.MakeGenericType(entityType), typeMappingSource);

                configs.Add(new Tuple<MethodInfo, object>(applyConcreteMethod, configurationInstance));                
            }

            foreach (var config in configs)
                config.Item1.Invoke(modelBuilder, new[] {config.Item2});
        }
    }
}