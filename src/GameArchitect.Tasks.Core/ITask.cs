using System;
using System.Threading.Tasks;

namespace GameArchitect.Tasks
{
    public interface ITask
    {
        string Name { get; set; } // Optional, uses class name if not set
        Type ParameterType { get; }
        Type OptionsType { get; }

        Task PreTask(ITaskParameters parameters);
        
        /* Returns true if the task was successful. */
        Task<bool> Run(ITaskParameters parameters);

        Task PostTask(ITaskParameters parameters);
    }
}