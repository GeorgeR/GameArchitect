using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameArchitect.Design.Attributes;
using GameArchitect.Extensions.Reflection;

using TypeInfo = GameArchitect.Design.Metadata.TypeInfo;

namespace GameArchitect.Design
{
    public class ExportCatalog : IEnumerable<TypeInfo>, IEnumerable<Type>, IValidatable
    {
        private AssemblyCatalog AssemblyCatalog { get; }
        private IList<TypeInfo> Types { get; set; }

        public ExportCatalog(string path)
        {
            AssemblyCatalog = new AssemblyCatalog(path);
        }

        public ExportCatalog(params Assembly[] assemblies)
        {
            AssemblyCatalog = new AssemblyCatalog(assemblies);
        }

        private IEnumerable<TypeInfo> GetTypes()
        {
            if (Types == null)
            {
                Types = AssemblyCatalog
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