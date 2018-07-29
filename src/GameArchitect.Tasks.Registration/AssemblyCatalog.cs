using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using GameArchitect.Extensions;

namespace GameArchitect.Tasks.Registration
{
    public class AssemblyCatalog : IEnumerable<Assembly>
    {
        private DllComparer DllComparer { get; } = new DllComparer();
        private IList<Assembly> Assemblies { get; }

        public AssemblyCatalog(params string[] paths)
        {
            paths.ForEach(o =>
            {
                Console.WriteLine(o);
                if(!Directory.Exists(o))
                    throw new DirectoryNotFoundException($"Directory not found {o}.");
            });

            var loadedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(o => o.Name);

            var thisPath = Assembly.GetExecutingAssembly().Location;
            var gameArchitectPath = string.Empty;
            var split = thisPath.Split('\\');
            for (var i = 0; i < split.Length; i++)
            {
                if (split[i].Equals("GameArchitect.Tasks.Runner"))
                    break;

                gameArchitectPath += split[i] + '\\';
            }

            var p = paths.ToList();
            //p.Add(gameArchitectPath);
            
            Assemblies = p.SelectMany(o => Directory.GetFiles(o, "*.dll", SearchOption.AllDirectories))
                .Where(IsCompatibleDll)
                .Where(o => !loadedAssemblies.Contains(AssemblyLoadContext.GetAssemblyName(o).Name))
                .Distinct(DllComparer)
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                .Where(o => o != null)
                .GroupBy(o => Path.GetFileNameWithoutExtension(o.Location))
                .Select(g => { return g.OrderBy(a => DllComparer).FirstOrDefault(); })
                .Where(o => o != null)
                .ToList();
        }

        public AssemblyCatalog(params Assembly[] assemblies)
        {
            Assemblies = assemblies.ToList();
        }

        private bool IsCompatibleDll(string assemblyPath)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(assemblyPath);
                return true;
            }
            catch(Exception e) {  }

            return false;
        }

        public IEnumerator<Assembly> GetEnumerator()
        {
            return Assemblies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}