using System;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealTemplateFactory : ITemplateFactory
    {
        private IServiceCollection Services { get; }
        private IServiceProvider ServiceProvider => Services.BuildServiceProvider();

        private ILogger<ITemplate> Log { get; }
        private IAttachedMetadataProvider AttachedMetadata { get; }
        
        public UnrealTemplateFactory(IServiceCollection services)
        {
            Services = services;
            
            Log = ServiceProvider.GetService<ILogger<ITemplate>>();
            AttachedMetadata = ServiceProvider.GetService<UnrealMetadataProvider>();
        }

        public ITemplate Create(TypeInfo typeInfo)
        {
            if (typeInfo.HasAttribute<UnrealStructAttribute>())
                return CreateForStruct(typeInfo);

            return CreateForClass(typeInfo);
        }

        private ICXXTemplate CreateForStruct(TypeInfo typeInfo)
        {
            return new UnrealStructTemplate(Log, typeInfo);
        }

        private ICXXTemplate CreateForClass(TypeInfo typeInfo)
        {
            return new UnrealClassTemplate(Log, typeInfo);
        }
    }
}