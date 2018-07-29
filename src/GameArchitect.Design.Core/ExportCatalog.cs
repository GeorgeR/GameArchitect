using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using GameArchitect.Design.Attributes;
using GameArchitect.Extensions.Reflection;

using TypeInfo = GameArchitect.Design.Metadata.TypeInfo;

namespace GameArchitect.Design
{
    public class ExportCatalog : IEnumerable<TypeInfo>, IEnumerable<Type>, IValidatable
    {
        private IEnumerable<Assembly> Assemblies { get; }
        private IList<TypeInfo> Types { get; set; }

        public ExportCatalog(string path)
        {
            Assemblies = Directory.GetFiles(path, "*.dll")
                .Select(o => AssemblyLoadContext.Default.LoadFromAssemblyPath(o));
        }

        public ExportCatalog(params Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }

        private IEnumerable<TypeInfo> GetTypes()
        {
            if (Types == null)
            {
                Types = Assemblies
                    .SelectMany(o => o.GetExportedTypes().Where(t => t.HasAttribute<ExportAttribute>()))
                    .Select(o => new TypeInfo(o))
                    .ToList();
            }

            return Types;
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return GetTypes().Select(o => o.Native).GetEnumerator();
        }

        public IEnumerator<TypeInfo> GetEnumerator()
        {
            return GetTypes().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsValid()
        {
            return GetTypes().All(o => o.IsValid());
        }
    }
}