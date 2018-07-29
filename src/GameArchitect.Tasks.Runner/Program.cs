using System;
using System.Linq;
using CommandLine;
using GameArchitect.Design;
using GameArchitect.Tasks.Registration;
using GameArchitect.Tasks.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Runner
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var serviceCollection = new ServiceCollection();
                    serviceCollection.AddSingleton<IServiceCollection>(provider => serviceCollection);
                    serviceCollection.AddScoped<ITaskParameters, TaskParameters>();
                    serviceCollection.AddSingleton<TaskCatalog>();
                    serviceCollection.AddLogging(_ => _.AddConsole());

                    var entityPaths = o.EntityPaths.Split(',');
                    var exportCatalog = new ExportCatalog(entityPaths);
                    Console.WriteLine($"{exportCatalog.ToList<Type>().Count} entities found in {entityPaths.FirstOrDefault()}.");

                    serviceCollection.AddSingleton(provider => exportCatalog);

                    var serviceProvider = serviceCollection.BuildServiceProvider();
                    
                    if (string.IsNullOrEmpty(o.TaskPaths))
                    {
                        Console.WriteLine("No task or task assemblies specified, running validation only.");

                        exportCatalog.IsValid(serviceProvider.GetService<ILogger<IValidatable>>());
                    }
                    else
                    {
                        var taskCatalog = serviceProvider.GetService<TaskCatalog>();
                        taskCatalog.Compose(o.TaskPaths.Split(','));
                        
                        var taskRunner = new TaskRunner(serviceCollection);
                        var taskBootstrap = new TaskBootstrap(taskCatalog, exportCatalog, o.Task, o.TaskOptions);

                        taskBootstrap.Run(taskRunner).Wait();
                    }
                });

            Console.Read();
        }
    }
}