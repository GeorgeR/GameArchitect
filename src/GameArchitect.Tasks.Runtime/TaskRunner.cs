using System;
using System.Threading.Tasks;
using GameArchitect.DependencyInjection;
using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.Runtime
{
    public sealed class TaskRunner : IServiceConfiguration
    {
        private IServiceCollection ServiceCollection { get; }
        private IServiceProvider ServiceProvider { get; set; }

        public TaskRunner() { }
        
        public TaskRunner(IServiceCollection services = null)
        {
            ServiceCollection = services ?? new ServiceCollection();
            ServiceCollection.AddConfigurations(typeof(TaskRunner).Assembly);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<DefaultMetadataProvider>();
            services.AddSingleton<IMetadataProvider>(provider => provider.GetService<DefaultMetadataProvider>());
            services.AddScoped<ITaskParameters, TaskParameters>();
            services.AddScoped<TaskParameters>();
            services.AddConfigurations();
            services.AddLogging();
        }

        public async Task<bool> Run(ITask task, ExportCatalog exports, ITaskOptions options)
        {
            if (options != null)
            {
                ServiceCollection.AddSingleton(options);
                ServiceCollection.AddSingleton(typeof(ITaskOptions), options);
            }
            else
                ServiceCollection.AddSingleton<ITaskOptions, EmptyTaskOptions>();

            if (task.ParameterType != null)
            {
                ServiceCollection.AddSingleton(task.ParameterType);
                ServiceCollection.AddSingleton(typeof(ITaskParameters), task.ParameterType);
            }
            
            ServiceCollection.AddConfigurations(task.GetType().Assembly);
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            
            var taskParameterType = task.ParameterType ?? typeof(TaskParameters);
            var parameters = (ITaskParameters) ServiceProvider.GetService(taskParameterType);
            parameters.PostSetup(ServiceProvider);

            return await task.Run(parameters);
        }
    }
}