using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks
{
    public interface ITaskParameters
    {
        ILogger Log { get; }

        TOptions GetOptionsAs<TOptions>() where TOptions : ITaskOptions;
    }

    public class TaskParameters : ITaskParameters
    {
        public ILogger Log { get; }
        public ITaskOptions Options { get; }

        public TaskParameters(ILogger logger, ITaskOptions options)
        {
            Log = logger;
            Options = options;
        }

        public TOptions GetOptionsAs<TOptions>() where TOptions : ITaskOptions
        {
            return (TOptions)Options;
        }
    }
}