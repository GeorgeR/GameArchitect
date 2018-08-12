using System;
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
            //var provider = source.BuildServiceProvider();
            var serviceConfigurations = assemblies.SelectMany(o =>
                o.GetTypes().Where(_ => typeof(IServiceConfiguration).IsAssignableFrom(_))
                    .Select(_ =>
                    {
                        var constructor = _.GetConstructors().FirstOrDefault();
                        var parameters = new object[constructor.GetParameters().Length];

                        var result = (IServiceConfiguration)Activator.CreateInstance(_, parameters);

                        return result;
                    }));

            serviceConfigurations.ForEach(o => o.Setup(source));
        }
    }
}