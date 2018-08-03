using System;
using GameArchitect.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks
{
    public interface ITaskParameters
    {
        ILogger<ITaskParameters> Log { get; }
        ExportCatalog Exports { get; } 
        ITaskOptions Options { get; }

        void Inject(IServiceCollection services);
        T GetService<T>();
        TOptions GetOptions<TOptions>() where TOptions : ITaskOptions;
    }

    public class TaskParameters : ITaskParameters, IValidatable
    {
        public ILogger<ITaskParameters> Log { get; }

        private IServiceProvider ServiceProvider { get; }
        
        public ExportCatalog Exports { get; }
        public ITaskOptions Options { get; }
        
        public TaskParameters(
            ILogger<ITaskParameters> logger, 
            IServiceCollection services,
            ExportCatalog exports, 
            ITaskOptions options)
        {
            Log = logger;

            ServiceProvider = services.BuildServiceProvider();

            Exports = exports;
            Options = options;
        }

        public virtual void Inject(IServiceCollection services) { }

        public T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public TOptions GetOptions<TOptions>() where TOptions : ITaskOptions
        {
            return (TOptions)Options;
        }

        public bool IsValid(ILogger<IValidatable> logger)
        {
            if(Log == null)
                logger.LogError("Log is null.");

            if(ServiceProvider == null)
                logger.LogError("ServiceProvider is null");

            if(Exports == null)
                logger.LogError("Exports is null");

            if(Options == null)
                logger.LogError("Options is null");

            return true;
        }
    }
}