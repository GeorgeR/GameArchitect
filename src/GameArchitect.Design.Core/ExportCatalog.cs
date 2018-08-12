using System;
using System.Collections;
using System.Collections.Concurrent;
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
        private IEnumerable<System.Reflection.Assembly> Assemblies { get; set; }

        private IList<IMetadataProvider> MetadataProviders { get; } = new List<IMetadataProvider>();
        private DefaultMetadataProvider DefaultMetadataProvider { get; }
        
        private ConcurrentDictionary<Type, IList<ITypeInfo>> Types { get; set; } = new ConcurrentDictionary<Type, IList<ITypeInfo>>();

        public ExportCatalog(DefaultMetadataProvider defaultMetadataProvider)
        {
            MetadataProviders.Add(defaultMetadataProvider);
            DefaultMetadataProvider = defaultMetadataProvider;

            Types.TryAdd(defaultMetadataProvider.GetType(), new List<ITypeInfo>());
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

        public void RegisterMetadataProvider(IMetadataProvider provider)
        {
            if(!Types.ContainsKey(provider.GetType()))
            {
                MetadataProviders.Add(provider);
                Types.TryAdd(provider.GetType(), new List<ITypeInfo>());
            }
        }

        public ITypeInfo Get<T>()
        {
            return Get<T, DefaultMetadataProvider, TypeInfo>();
        }

        public TTypeInfo Get<T, TProvider, TTypeInfo>()
            where TProvider : class, IMetadataProvider
            where TTypeInfo : class, ITypeInfo
        {
            if (Assemblies == null || !Assemblies.Any())
                throw new Exception($"No assemblies specified for ExportCatalog.");

            var types = GetTypes<TProvider>();
            var result = types.FirstOrDefault(o => o.Native.AssemblyQualifiedName.Equals(typeof(T).AssemblyQualifiedName));
            if (result == null)
                throw new NullReferenceException($"Tried to get type {typeof(T).Name} from ExportCatalog but it was not found.");

            return (TTypeInfo) result;
        }

        private IEnumerable<ITypeInfo> GetTypes<TProvider>() where TProvider : class, IMetadataProvider
        {
            var metadataProviderType = typeof(TProvider);
            if (!Types.ContainsKey(metadataProviderType))
                throw new NotImplementedException($"Provider {metadataProviderType.Name} not registered.");

            var providerTypes = Types[metadataProviderType];
            if (providerTypes.Count == 0)
            {
                var metadataProvider = MetadataProviders.FirstOrDefault(o => o.GetType() == metadataProviderType);

                var exportedTypes = Assemblies.SelectMany(o => o.GetExportedTypes().Where(t => t.HasAttribute<ExportAttribute>()));
                var projectedTypes = exportedTypes.Select(o => metadataProvider.Create(o));
                foreach(var t in projectedTypes)
                    providerTypes.Add(t);
            }

            return providerTypes;
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return GetTypes<DefaultMetadataProvider>().Select(o => o.Native).GetEnumerator();
        }

        public IEnumerator<ITypeInfo> GetEnumerator()
        {
            return GetTypes<DefaultMetadataProvider>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool IsValid(ILogger<IValidatable> logger)
        {
            return GetTypes<DefaultMetadataProvider>().All(o => o.IsValid(logger));
        }
    }
}