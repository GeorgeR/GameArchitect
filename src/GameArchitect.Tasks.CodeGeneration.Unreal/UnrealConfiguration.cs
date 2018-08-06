using GameArchitect.Design;
using GameArchitect.Design.Metadata;
using GameArchitect.Design.Unreal.Metadata;
using GameArchitect.Tasks.CodeGeneration.CXX;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration.Unreal
{
    public class UnrealConfiguration : CXXConfiguration, ICodeGenerationConfiguration
    {
        public override void Setup(IServiceCollection services)
        {
            base.Setup(services);

            services.AddSingleton<ICodeGenerationConfiguration, UnrealConfiguration>(o => this);

            services.AddSingleton<IMetadataProvider, UnrealMetadataProvider>();
            services.AddSingleton<INameTransformer, UnrealNameTransformer>();
            services.AddSingleton<ITypeTransformer, UnrealTypeTransformer>();
        }
    }
}