using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GameArchitect.Design;
using GameArchitect.Tasks.Runtime;
using Newtonsoft.Json;
using GameArchitect.Tasks.Registration;

namespace GameArchitect.Tasks.Runner
{
    /* Responsible for resolving, creating and injecting options into a task. */
    internal class TaskBootstrap
    {
        private ITask Task { get; } = null;
        private ExportCatalog Exports { get; }
        private ITaskOptions Options { get; } = null;

        public TaskBootstrap(TaskCatalog taskCatalog, ExportCatalog exports, string taskName, string taskOptionsFile)
        {
            if (!string.IsNullOrEmpty(taskName) && !taskCatalog.HasNamedTask(taskName))
                throw new Exception($"No task found with name {taskName}.");
            
            if(!File.Exists(taskOptionsFile))
                throw new Exception($"Task options were not defined or the file was not found.");

            Exports = exports;

            Task = taskCatalog.FirstOrDefault();
            if(Task == null)
                throw new NullReferenceException($"Default task not found. Do the referenced dll's have an [Export] attribute?");

            var optionsStr = File.ReadAllText(taskOptionsFile);
            var taskOptions = JsonConvert.DeserializeObject(optionsStr, Task.OptionsType) as ITaskOptions;
            if(taskOptions == null)
                throw new Exception($"TaskOptions not parsed.");

            Options = taskOptions;
        }

        public async Task<bool> Run(TaskRunner runner)
        {
            return await runner.Run(Task, Exports, Options);
        }
    }
}