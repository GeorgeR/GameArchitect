using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#if NETCORE
using System.Runtime.Loader;
#endif

namespace GameArchitect
{
    public class AssemblyLoader
    {
        private AppDomain Domain { get; }
        private ISet<Assembly> LoadedAssemblies { get; } = new HashSet<Assembly>();

        public AssemblyLoader(AppDomain domain = null)
        {
            Domain = domain ?? AppDomain.CurrentDomain;
            Domain.AssemblyLoad += OnAssemblyLoaded;

#if NETCORE
            AssemblyLoadContext.Default.Resolving += Resolve;
#endif
        }

        private void OnAssemblyLoaded(object sender, AssemblyLoadEventArgs args)
        {
            LoadedAssemblies.Add(args.LoadedAssembly);
        }
        
        public Assembly GetOrLoadAssembly(string filePath)
        {
            if(!File.Exists(filePath))
                throw new FileNotFoundException($"Dll at path {filePath} doesn't exist.");

            var name = AssemblyName.GetAssemblyName(filePath);
            var result = GetLoadedAssembly(name);
            if (result == null)
                result = Assembly.LoadFrom(filePath);

            return result;
        }

        public Assembly GetOrLoadAssembly(AssemblyName name)
        {
            return GetLoadedAssembly(name);
        }

#if NETCORE
        public Assembly Resolve(AssemblyLoadContext context, AssemblyName name)
        {
            return GetOrLoadAssembly(name);
        }
#endif

        private Assembly GetLoadedAssembly(AssemblyName name)
        {
            return LoadedAssemblies.FirstOrDefault(o => o.GetName().FullName.Equals(name.FullName));
        }
    }
}