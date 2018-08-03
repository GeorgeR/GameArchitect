using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Design.Metadata
{
    public class AttachedMetadataProvider : IAttachedMetadataProvider
    {
        protected IServiceCollection Services { get; }
        protected IServiceProvider ServiceProvider => Services.BuildServiceProvider();

        private IDictionary<IMetaInfo, IAttachedMetadata> AttachedMetadata { get; } = new Dictionary<IMetaInfo, IAttachedMetadata>();
        private IDictionary<Type, Func<IMetaInfo, IAttachedMetadata>> Factories { get; } = new Dictionary<Type, Func<IMetaInfo, IAttachedMetadata>>();
        
        public AttachedMetadataProvider(IServiceCollection services)
        {
            Services = services;
        }

        public TAttachedMetadata GetOrAdd<TAttachedMetadata>(IMetaInfo info) 
            where TAttachedMetadata : IAttachedMetadata
        {
            if (AttachedMetadata.ContainsKey(info))
                return (TAttachedMetadata) AttachedMetadata[info];

            var metaType = info.GetType();
            if (!Factories.ContainsKey(metaType))
                throw new NotImplementedException($"No factory registered for metadata of type {info.TypeName}");

            var attachedMetadata = Factories[metaType](info);
            AttachedMetadata.Add(info, attachedMetadata);

            return (TAttachedMetadata) attachedMetadata;
        }

        protected void RegisterFactory<TMeta>(Func<TMeta, IAttachedMetadata> factory) where TMeta : IMetaInfo
        {
            var metaInfoType = typeof(TMeta);
            if (Factories.ContainsKey(metaInfoType))
                Factories[metaInfoType] = info => factory((TMeta) info);
            else
                Factories.Add(metaInfoType, info => factory((TMeta)info));
        }
    }
}