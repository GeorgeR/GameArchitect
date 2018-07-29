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
            Services = services ?? new ServiceCollection();
            Services.AddScoped<ITaskParameters, TaskParameters>();
            Services.AddLogging();

            ServiceProvider = Services.BuildServiceProvider();
        }

        public async Task<bool> Run(ITask task, ExportCatalog exports, ITaskOptions options)
        {
            Services.AddScoped(typeof(ITaskOptions), provider => options);
            ServiceProvider = Services.BuildServiceProvider();

            var parameters = (ITaskParameters) ServiceProvider.GetService(task.ParameterType);
            return await task.Run(parameters);
        }
    }
}