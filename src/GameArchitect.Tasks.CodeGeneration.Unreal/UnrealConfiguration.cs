using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealConfiguration : ICodeGenerationConfiguration
    {
        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<ICodeGenerationConfiguration, UnrealConfiguration>(o => this);

            services.AddSingleton<IMetadataProvider, UnrealMetadataProvider>();
            services.AddSingleton<INameTransformer, INameTransformer>();
            services.AddSingleton<ITypeTransformer, ITypeTransformer>();
        }
    }
}