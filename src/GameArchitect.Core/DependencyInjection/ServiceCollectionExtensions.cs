using System;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using GameArchitect.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfiguration<TConfiguration>(this IServiceCollection source)
            where TConfiguration : IServiceConfiguration
        {
            var instance = Activator.CreateInstance<TConfiguration>();
            instance.Setup(source);
        }

        public static void AddConfigurations(this IServiceCollection source, params Assembly[] assemblies)
        {
            var serviceConfigurations = assemblies.SelectMany(o =>
                o.GetTypes().Where(_ => typeof(IServiceConfiguration).IsAssignableFrom(_))
                    .Select(_ =>
                    {
                        if(_.GetConstructors().All(c => c.GetParameters().Length != 0))
                            throw new MissingMethodException($"The type {_.Name} doesn't contain a parameterless constructor.");

                        return (IServiceConfiguration) Activator.CreateInstance(_);
                    }));

            serviceConfigurations.ForEach(o => o.Setup(source));
        }
    }
}