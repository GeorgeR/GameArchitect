using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using GameArchitect.DependencyInjection;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.Registration
{
    public sealed class TaskCatalog : IEnumerable<ITask>
    {
        private bool _isComposed = false;

        private ILogger<TaskCatalog> Log { get; }

        private AssemblyLoader Loader { get; set; }
        private TaskComparer TaskComparer { get; set; }

        private readonly IDictionary<string, ITask> _tasks = new Dictionary<string, ITask>();
        public ITask this[string taskName]
        {
            get
            {
                if(!_isComposed)
                    throw new Exception($"TaskCatalog.Compose() not called.");

                return _tasks[taskName.ToLower()];
            }
        }

        public TaskCatalog(ILogger<TaskCatalog> logger)
        {
            Log = logger;
            Loader = new AssemblyLoader(AppDomain.CurrentDomain);
        }

        public bool HasNamedTask(string taskName)
        {
            if (!_isComposed)
                throw new Exception($"TaskCatalog.Compose() not called.");

            taskName = taskName.ToLower();

            if (_tasks.ContainsKey(taskName))
            {
                if(_tasks[taskName] == null)
                    throw new NullReferenceException($"The task {taskName} was null for some reason.");

                return true;
            }

            return false;
        }

        public void Compose(IServiceCollection services, params string[] taskAssemblyPaths)
        {
            if(taskAssemblyPaths.Length <= 0)
                throw new Exception("Task assembly paths was empty.");

            //var a = Assembly.LoadFile(taskAssemblyPaths[0]);
            //var types = a.GetExportedTypes()
            //    .Where(o => typeof(ITask).IsAssignableFrom(o))
            //    .Select(o => (ITask)Activator.CreateInstance(o));

            //var t = types.FirstOrDefault();

            //var catalog = new AssemblyCatalog(a);
            //var container = new CompositionContainer(catalog);
            //var e = container.GetExports<ITask>().ToList()[0].Value;
            //var x = e;

            var catalog = new AggregateCatalog();
            taskAssemblyPaths.ForEach(o =>
            {
                if (File.Exists(o))
                {
                    var assembly = Loader.GetOrLoadAssembly(o);
                    var assemblyCatalog = new AssemblyCatalog(assembly);
                    catalog.Catalogs.Add(assemblyCatalog);

                    Log.LogInformation($"Registering {Path.GetFileName(o)} in TaskCatalog.");

                    o = Path.GetDirectoryName(o);
                }

                if (Directory.Exists(o))
                {
                    Log.LogInformation($"Registering directory {o} in TaskCatalog.");

                    //var directoryCatalog = new DirectoryCatalog(o);
                    //catalog.Catalogs.Add(directoryCatalog);
                }
            });

            using (var compositionContainer = new CompositionContainer(catalog))
            {
                var uniqueTasks = compositionContainer
                    .GetExports<ITask>()
                    .Distinct(TaskComparer)
                    .ToList();

                Log.LogInformation($"Found {uniqueTasks.Count} tasks.");
                foreach (var export in uniqueTasks)
                {
                    var e = export.Value;

                    try
                    {
                        var hat = e.Name;
                    }
                    catch (Exception exception)
                    {
                        Log.LogError(exception, "");
                    }

                    var taskName = string.IsNullOrEmpty(e.Name)
                        ? e.GetType().Name.Replace("Task", "").ToLower()
                        : e.Name.ToLower();

                    if (!_tasks.ContainsKey(taskName))
                    {
                        Log.LogInformation($"Found task: {taskName}.");
                        e.Name = taskName;
                        if (IsValid(e))
                            _tasks.Add(taskName, e);
                    }
                }

                services.AddConfigurations(uniqueTasks.Select(o => o.Value.GetType().Assembly).ToArray());
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

        private bool IsValid(ITask task)
        {
            var result = true;

            if (task.ParameterType != null)
            {
                if (!task.ParameterType.ImplementsInterface<ITaskParameters>())
                {
                    Log.LogError($"Parameters for task {task.Name} was set but did not implement ITaskParameters.");
                    result = false;
                }
            }

            if (task.OptionsType != null)
            {
                if (!task.OptionsType.ImplementsInterface<ITaskOptions>())
                {
                    Log.LogError($"Options for task {task.Name} was set but did not implement ITaskOptions.");
                    result = false;
                }
            }

            return result;
        }
    }
}