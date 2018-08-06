using System;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealTemplateFactory : ITemplateFactory
    {
        private ILogger<ITemplate> Log { get; }
        private IMetadataProvider MetadataProvider { get; set; }

        public UnrealTemplateFactory(ILogger<ITemplate> log)
        {
            Log = log;
        }

        public void Setup(IServiceCollection services)
        {
            services.AddTransient<UnrealStructTemplate>();
            services.AddTransient<UnrealClassTemplate>();

            var serviceProvider = services.BuildServiceProvider();

            MetadataProvider = serviceProvider.GetService<IMetadataProvider>();
        }

        public ITemplate Create(ITypeInfo typeInfo)
        {
            if (typeInfo.HasAttribute<UnrealStructAttribute>())
                return CreateForStruct(typeInfo);

            return CreateForClass(typeInfo);
        }

        private ICXXTemplate CreateForStruct(ITypeInfo typeInfo)
        {
            throw new NotImplementedException();
            //return MetadataProvider.Create<Unreal>()
            //return new UnrealStructTemplate(Log, typeInfo);
        }

        private ICXXTemplate CreateForClass(ITypeInfo typeInfo)
        {
            throw new NotImplementedException();
            //return new UnrealClassTemplate(Log, typeInfo);
        }
    }
}