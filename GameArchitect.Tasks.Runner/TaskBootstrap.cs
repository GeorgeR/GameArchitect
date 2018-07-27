using System;
using System.IO;
using System.Threading.Tasks;
using GameArchitect.Tasks.Runtime;
using Newtonsoft.Json;

namespace GameArchitect.Tasks.Runner
{
    internal class TaskBootstrap
    {
        private ITask Task { get; } = null;
        private ITaskOptions Options { get; } = null;

        public TaskBootstrap(TaskCatalog taskCatalog, string taskName, string taskOptionsFile)
        {
            if (!taskCatalog.HasNamedTask(taskName))
                throw new Exception($"No task found with name {taskName}.");
            
            if(!File.Exists(taskOptionsFile))
                throw new Exception($"Task options were not defined or the file was not found.");

            var optionsStr = File.ReadAllText(taskOptionsFile);
            var taskOptions = JsonConvert.DeserializeObject(optionsStr, Task.OptionsType) as ITaskOptions;
            if(taskOptions == null)
                throw new Exception($"TaskOptions not parsed.");
        }

        public async Task<bool> Run(TaskRunner runner)
        {
            return await runner.Run(Task, Options);
        }
    }
}