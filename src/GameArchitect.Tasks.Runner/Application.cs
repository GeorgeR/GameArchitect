using System;
using System.Linq;
using System.Reflection;
using CommandLine;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Tasks.Registration;
using GameArchitect.Tasks.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Runner
{
    public sealed class Application
    {
        private ILogger<Application> Log { get; }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var app = new Application(o);
                });

            Console.Read();
        }

        public Application(Options options)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IServiceCollection>(provider => serviceCollection);
            serviceCollection.AddScoped<ITaskParameters, TaskParameters>();
            serviceCollection.AddSingleton<TaskCatalog>();
            serviceCollection.AddSingleton<DefaultMetadataProvider>();
            serviceCollection.AddSingleton<ExportCatalog>();
            serviceCollection.AddLogging(_ => _.AddConsole());

            var serviceProvider = serviceCollection.BuildServiceProvider();
            Log = serviceProvider.GetService<ILogger<Application>>();

            var entityPaths = options.EntityPaths.Split(',');
            var exportCatalog = serviceProvider.GetService<ExportCatalog>();
            exportCatalog.FindInAssemblies(entityPaths);

            Log.LogInformation($"{exportCatalog.ToList<Type>().Count} entities found in {entityPaths.FirstOrDefault()}.");

            serviceCollection.AddSingleton(provider => exportCatalog);

            serviceProvider = serviceCollection.BuildServiceProvider();

            if (string.IsNullOrEmpty(options.TaskPaths))
            {
                Log.LogWarning("No task or task assemblies specified, running validation only.");

                exportCatalog.IsValid(serviceProvider.GetService<ILogger<IValidatable>>());
            }
            else
            {
                var taskCatalog = serviceProvider.GetService<TaskCatalog>();
                taskCatalog.Compose(options.TaskPaths.Split(','));

                var taskRunner = new TaskRunner(serviceCollection);
                var taskBootstrap = new TaskBootstrap(taskCatalog, exportCatalog, options.Task, options.TaskOptions);

                taskBootstrap.Run(taskRunner).Wait();
            }
        }
    }
}