using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using CommandLine;
using GameArchitect.DependencyInjection;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks.Registration;
using GameArchitect.Tasks.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Runner
{
    public sealed class Application : IServiceConfiguration
    {
        private ILogger<Application> Log { get; }

        public static void Main(string[] args)
        {
            var resolver = new AssemblyResolver();
            AssemblyLoadContext.Default.Resolving += resolver.Resolve;
            
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var app = new Application(o);
                });

            Console.Read();
        }

        public Application() { }

        public Application(Options options)
        {
            var entityPaths = options.EntityPaths.Split(',');

            var services = new ServiceCollection();
            services.AddConfigurations(typeof(Application).Assembly);
            services.AddSingleton(p =>
            {
                var result = new ExportCatalog(p.GetService<DefaultMetadataProvider>());
                result.FindInAssemblies(entityPaths);
                return result;
            });

            var serviceProvider = services.BuildServiceProvider();
            var exportCatalog = serviceProvider.GetService<ExportCatalog>();

            Log = serviceProvider.GetService<ILogger<Application>>();
            Log.LogInformation($"{exportCatalog.ToList<Type>().Count} entities found in {entityPaths.FirstOrDefault()}.");

            if (string.IsNullOrEmpty(options.TaskPaths))
            {
                Log.LogWarning("No task or task assemblies specified, running validation only.");

                exportCatalog.IsValid(serviceProvider.GetService<ILogger<IValidatable>>());
            }
            else
            {
                var taskCatalog = serviceProvider.GetService<TaskCatalog>();
                taskCatalog.Compose(services, options.TaskPaths.Split(','));

                var taskRunner = new TaskRunner(services);
                var taskBootstrap = new TaskBootstrap(taskCatalog, exportCatalog, options.Task, options.TaskOptions);

                taskBootstrap.Run(taskRunner).Wait();
            }
        }

        public void Setup(IServiceCollection services)
        {
            services.AddSingleton(provider => services);
            services.AddScoped<ITaskParameters, TaskParameters>();
            services.AddSingleton<TaskCatalog>();
            services.AddSingleton<DefaultMetadataProvider>();
            services.AddSingleton<IMetadataProvider>(provider => provider.GetService<DefaultMetadataProvider>());
            services.AddSingleton<ExportCatalog>();
            services.AddLogging(_ => _.AddConsole());
        }
    }
}