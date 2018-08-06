using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GameArchitect.Design.Attributes;
using GameArchitect.Design.Metadata;
using GameArchitect.Extensions;
using GameArchitect.Extensions.Reflection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Design
{
    public class ExportCatalog : IEnumerable<ITypeInfo>, IEnumerable<Type>, IValidatable
    {
        private DefaultMetadataProvider DefaultMetadataProvider { get; }
        private IEnumerable<System.Reflection.Assembly> Assemblies { get; set; }
        private IDictionary<Type, IList<ITypeInfo>> Types { get; set; }

        public ExportCatalog(DefaultMetadataProvider defaultMetadataProvider)
        {
            DefaultMetadataProvider = defaultMetadataProvider;

            Types = new Dictionary<Type, IList<ITypeInfo>>
            {
                {defaultMetadataProvider.GetType(), new List<ITypeInfo>()}
            };
        }

        public void FindInAssemblies(params string[] paths)
        {
            var a = new List<System.Reflection.Assembly>();

            paths.ForEach(p =>
            {
                if (File.Exists(p))
                    a.Add(System.Reflection.Assembly.LoadFile(p));
                else if (Directory.Exists(p))
                    a.AddRange(Directory.GetFiles(p, "*.dll").Select(System.Reflection.Assembly.LoadFile));
                else
                    throw new FileNotFoundException($"File or folder not found: {p}");
            });

            Assemblies = a;
        }

        public void FindInAssemblies(params System.Reflection.Assembly[] assemblies)
        {
            Assemblies = assemblies;
        }

        public TTypeInfo Get<T, TTypeInfo>() where TTypeInfo : class, ITypeInfo
        {
            return (TTypeInfo) Get<T>();
        }

        public ITypeInfo Get<T>()
        {
            var result = GetTypes().FirstOrDefault(o => o.Native.AssemblyQualifiedName.Equals(typeof(T).AssemblyQualifiedName));
            if (result == null)
                throw new NullReferenceException($"Tried to get type {typeof(T).Name} from ExportCatalog but it was not found.");

            return result;
        }

        private IEnumerable<ITypeInfo> GetTypes(IMetadataProvider metadataProvider = null)
        {
            if (metadataProvider == null)
                metadataProvider = DefaultMetadataProvider;

            if (Types == null)
            {
                if (!Types.ContainsKey(metadataProvider.GetType()))
                    Types.Add(metadataProvider.GetType(), new List<ITypeInfo>());

                Types[metadataProvider.GetType()].AddRange(Assemblies
                    .SelectMany(o => o.GetExportedTypes().Where(t => t.HasAttribute<ExportAttribute>()))
                    .Select(o => metadataProvider.Create<ITypeInfo>(o)));
            }

            return Types[metadataProvider.GetType()];
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return GetTypes().Select(o => o.Native).GetEnumerator();
        }

        public IEnumerator<ITypeInfo> GetEnumerator()
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