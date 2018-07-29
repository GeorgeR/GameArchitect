using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using GameArchitect.Extensions;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Registration
{
    public sealed class TaskCatalog : IEnumerable<ITask>
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
                if (File.Exists(o))
                {
                    var assembly = Assembly.LoadFile(o);
                    var assemblyCatalog = new AssemblyCatalog(assembly);
                    catalog.Catalogs.Add(assemblyCatalog);

                    Console.WriteLine($"Registering {Path.GetFileName(o)} in TaskCatalog.");

                    o = Path.GetDirectoryName(o);
                }

                if (Directory.Exists(o))
                {
                    Console.WriteLine($"Registering directory {o} in TaskCatalog.");

                    var directoryCatalog = new DirectoryCatalog(o);
                    catalog.Catalogs.Add(directoryCatalog);
                }
            });

            using (var container = new CompositionContainer(catalog))
            {
                Console.WriteLine($"Found {container.GetExports<ITask>().Count()} tasks.");
                foreach (var export in container.GetExports<ITask>().Distinct())
                {
                    var e = export.Value;
                    var taskName = string.IsNullOrEmpty(e.Name) ? e.GetType().Name.ToLower() : e.Name.ToLower();
                    if (!_tasks.ContainsKey(taskName))
                    {
                        Console.WriteLine($"Found task: {taskName}.");
                        _tasks.Add(taskName, e);
                    }
                }
            }
            
            _isComposed = true;
        }

        public IEnumerator<ITask> GetEnumerator()
        {
            return _tasks.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}