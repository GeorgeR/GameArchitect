using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace GameArchitect.Support.EntityFramework
{
    public abstract class GameArchitectDbContextBase : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            var configurationGenericType = typeof(GameArchitectTypeConfiguration<>);
            var applyGenericMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public).First(o => o.Name == "ApplyConfiguration");

            var entityTypes = Model.GetEntityTypes().Select(o => o.ClrType);
            foreach (var entityType in entityTypes)
            {
                var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(entityType);
                var configurationInstance = Activator.CreateInstance(configurationGenericType.MakeGenericType(entityType));

                applyConcreteMethod.Invoke(modelBuilder, new[] {configurationInstance});
            }
        }
    }
}