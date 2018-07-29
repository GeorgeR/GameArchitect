using System;
using GameArchitect.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks
{
    public interface ITaskParameters
    {
        ILogger Log { get; }
        IServiceCollection Services { get; }
        ExportCatalog Exports { get; } 
        ITaskOptions Options { get; }
        
        IServiceProvider GetServiceProvider();
        TOptions GetOptionsAs<TOptions>() where TOptions : ITaskOptions;
    }

    public class TaskParameters : ITaskParameters
    {
        public ILogger Log { get; }
        public IServiceCollection Services { get; }
        public ExportCatalog Exports { get; }
        public ITaskOptions Options { get; }
        
        public TaskParameters(ILogger logger, IServiceCollection services, ExportCatalog exports, ITaskOptions options)
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
    }
}