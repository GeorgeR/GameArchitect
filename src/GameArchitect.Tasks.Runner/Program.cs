using System;
using CommandLine;
using GameArchitect.Tasks.Registration;
using GameArchitect.Tasks.Runtime;
using Microsoft.Extensions.DependencyInjection;

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
                    serviceCollection.AddScoped<ITaskParameters, TaskParameters>();
                    serviceCollection.AddSingleton<TaskCatalog>();
                    serviceCollection.AddLogging();

                    var serviceProvider = serviceCollection.BuildServiceProvider();

                    var taskCatalog = serviceProvider.GetService<TaskCatalog>();
                    taskCatalog.Compose(o.AssemblyPaths.Split(','));

                    var taskRunner = new TaskRunner(serviceCollection);
                    var taskBootstrap = new TaskBootstrap(taskCatalog, o.Task, o.TaskOptions);

                    taskBootstrap.Run(taskRunner).Wait();
                });

            Console.Read();
        }
    }
}