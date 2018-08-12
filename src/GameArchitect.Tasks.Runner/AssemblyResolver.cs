using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace GameArchitect.Tasks.Runner
{
    public class AssemblyResolver
    {
        private IList<AssemblyName> Assemblies { get; }

        public AssemblyResolver(params string[] hintPaths)
        {
            Assemblies = hintPaths.SelectMany(o => Directory.GetFiles(o, "*.dll", SearchOption.AllDirectories))
                .Select(AssemblyName.GetAssemblyName)
                .OrderBy(o => o.Name)
                .ToList();
        }

        public Assembly Resolve(AssemblyLoadContext context, AssemblyName name)
        {
            var match = Assemblies.FirstOrDefault(o => o.FullName.Equals(name.FullName));
            return match != null ? Assembly.Load(match) : null;
        }
    }
}