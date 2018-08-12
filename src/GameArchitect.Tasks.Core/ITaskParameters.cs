using System;
using GameArchitect.DependencyInjection;
using GameArchitect.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks
{
    public interface ITaskParameters : IServiceConfiguration
    {
        ILogger<ITaskParameters> Log { get; }
        ExportCatalog Exports { get; } 
        ITaskOptions Options { get; }

        T GetService<T>();
        TOptions GetOptions<TOptions>() where TOptions : ITaskOptions;
    }

    public class TaskParameters : ITaskParameters, IValidatable
    {
        private IServiceProvider ServiceProvider { get; }
        
        public ILogger<ITaskParameters> Log { get; }
        
        public ExportCatalog Exports { get; }
        public ITaskOptions Options { get; }

        public TaskParameters() { }

        public TaskParameters(
            IServiceProvider serviceProvider,
            ILogger<ITaskParameters> logger, 
            ExportCatalog exports, 
            ITaskOptions options)
        {
            ServiceProvider = serviceProvider;
            
            Log = logger;
            
            Exports = exports;
            Options = options;
        }

        public virtual void Setup(IServiceCollection services) { }

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

            if(Exports == null)
                logger.LogError("Exports is null");

            if(Options == null)
                logger.LogError("Options is null");

            return true;
        }
    }
}