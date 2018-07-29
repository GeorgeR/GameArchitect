using System;
using GameArchitect.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks
{
    public interface ITaskParameters
    {
        ILogger<ITaskParameters> Log { get; }
        IServiceCollection Services { get; }
        ExportCatalog Exports { get; } 
        ITaskOptions Options { get; }
        
        IServiceProvider GetServiceProvider();
        TOptions GetOptionsAs<TOptions>() where TOptions : ITaskOptions;
    }

    public class TaskParameters : ITaskParameters, IValidatable
    {
        public ILogger<ITaskParameters> Log { get; }
        public IServiceCollection Services { get; }
        public ExportCatalog Exports { get; }
        public ITaskOptions Options { get; }
        
        public TaskParameters(ILogger<ITaskParameters> logger, IServiceCollection services, ExportCatalog exports, ITaskOptions options)
        {
            Log = logger;
            Services = services;
            Exports = exports;
            Options = options;
        }

        public IServiceProvider GetServiceProvider()
        {
            return Services.BuildServiceProvider();
        }

        public TOptions GetOptionsAs<TOptions>() where TOptions : ITaskOptions
        {
            return (TOptions)Options;
        }

        public bool IsValid(ILogger<IValidatable> logger)
        {
            if(Log == null)
                logger.LogError("Log is null.");

            if(Services == null)
                logger.LogError("Services is null");

            if(Exports == null)
                logger.LogError("Exports is null");

            if(Options == null)
                logger.LogError("Options is null");

            return true;
        }
    }
}