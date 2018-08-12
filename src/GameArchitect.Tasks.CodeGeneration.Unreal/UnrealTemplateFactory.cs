using System;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Attributes;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX.Templates;
using GameArchitect.Tasks.CodeGeneration.Unreal.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealTemplateFactory : ITemplateFactory
    {
        private UnrealMetadataProvider MetadataProvider { get; }

        public UnrealTemplateFactory() { }

        public UnrealTemplateFactory(UnrealMetadataProvider metadataProvider)
        {
            MetadataProvider = metadataProvider;
        }

        public void Setup(IServiceCollection services)
        {
            services.AddTransient<UnrealStructTemplate>();
            services.AddTransient<UnrealClassTemplate>();
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