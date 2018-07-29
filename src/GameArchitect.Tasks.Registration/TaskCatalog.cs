using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Composition.Hosting;
using GameArchitect.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Registration
{
    public sealed class TaskCatalog
    {
        private bool _isComposed = false;

        private ILogger<TaskCatalog> Log { get; }

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

            var catalog = new AggregateCatalog();
            taskAssemblyPaths.ForEach(o =>
            {
                var directoryCatalog = new DirectoryCatalog(o);
                catalog.Catalogs.Add(directoryCatalog);
            });
            
            using (var container = new CompositionContainer(catalog))
                foreach (var export in container.GetExports<ITask>())
                {
                    var e = export.Value;
                    _tasks.Add(string.IsNullOrEmpty(e.Name) ? e.Name.ToLower() : e.GetType().Name.ToLower(), e);
                }

            _isComposed = true;
        }
    }
}