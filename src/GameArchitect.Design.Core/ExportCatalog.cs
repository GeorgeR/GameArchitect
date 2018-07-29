using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GameArchitect.Design.Attributes;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;
using TypeInfo = GameArchitect.Design.Metadata.TypeInfo;

namespace GameArchitect.Design
{
    public class ExportCatalog : IEnumerable<TypeInfo>, IEnumerable<Type>, IValidatable
    {
        private IEnumerable<Assembly> Assemblies { get; }
        private IList<TypeInfo> Types { get; set; }

        public ExportCatalog(params string[] paths)
        {
            var a = new List<Assembly>();

            paths.ForEach(p =>
            {
                if(File.Exists(p))
                    a.Add(Assembly.LoadFile(p));
                else if (Directory.Exists(p))
                    a.AddRange(Directory.GetFiles(p, "*.dll").Select(Assembly.LoadFile));
                else
                    throw new FileNotFoundException($"File or folder not found: {p}");
            });

            Assemblies = a;
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

        public bool IsValid(ILogger<IValidatable> logger)
        {
            return GetTypes().All(o => o.IsValid(logger));
        }
    }
}