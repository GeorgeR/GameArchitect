using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Runtime
{
    public sealed class TaskCatalog
    {
        private bool _isComposed = false;

        private ILogger<TaskCatalog> Log { get; }
        private AssemblyCatalog AssemblyCatalog { get; set; }
        
        private readonly IDictionary<string, ITask> _tasks = new Dictionary<string, ITask>();
        public ITask this[string taskName]
        {
            get
            {
                if(!_isComposed)
                    throw new Exception($"TaskCatalog.Compose() not called.");

                return _tasks[taskName];
            }
        }

        public TaskCatalog(ILogger<TaskCatalog> logger)
        {
            Log = logger;
        }

        public bool HasNamedTask(string taskName)
        {
            if (!_isComposed)
                throw new Exception($"TaskCatalog.Compose() not called.");

            return _tasks.ContainsKey(taskName);
        }

        public void Compose(params string[] taskAssemblyPaths)
        {
            if(taskAssemblyPaths.Length <= 0)
                throw new Exception("Task assembly paths was empty.");

            AssemblyCatalog = new AssemblyCatalog(taskAssemblyPaths);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(AssemblyCatalog);

            using (var container = configuration.CreateContainer())
                foreach(var export in container.GetExports<ITask>())
                    _tasks.Add(string.IsNullOrEmpty(export.Name) ? export.Name.ToLower() : export.GetType().Name.ToLower(), export);

            _isComposed = true;
        }
    }
}