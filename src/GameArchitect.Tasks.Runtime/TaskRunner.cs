using System;
using System.Threading.Tasks;
using GameArchitect.Design;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.Runtime
{
    public sealed class TaskRunner
    {
        private IServiceCollection Services { get; }
        private IServiceProvider ServiceProvider { get; set; }

        public TaskRunner(IServiceCollection services = null)
        {
            Console.WriteLine(services);
            Services = services ?? new ServiceCollection();
            Services.AddSingleton(provider => Services);
            Services.AddScoped<ITaskParameters, TaskParameters>();
            Services.AddScoped<TaskParameters>();
            //Services.AddLogging();

            ServiceProvider = Services.BuildServiceProvider();
        }

        public async Task<bool> Run(ITask task, ExportCatalog exports, ITaskOptions options)
        {
            if(options == null)
                Console.WriteLine("Options provided to Task.Run was null.");

            Services.AddScoped(typeof(ITaskOptions), provider => options);
            ServiceProvider = Services.BuildServiceProvider();

            var taskParameterType = task.ParameterType ?? typeof(TaskParameters);
            var parameters = (ITaskParameters) ServiceProvider.GetService(taskParameterType);

            return await task.Run(parameters);
        }
    }
}