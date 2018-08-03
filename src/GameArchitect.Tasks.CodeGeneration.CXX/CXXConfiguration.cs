using GameArchitect.Design;
using GameArchitect.Design.CXX.Metadata;
using GameArchitect.Design.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration.CXX
{
    public class CXXConfiguration : ICodeGenerationConfiguration
    {
        public void Setup(IServiceCollection services)
        {
            services.AddSingleton<ICodeGenerationConfiguration, CXXConfiguration>(o => this);

            services.AddSingleton<IMetadataProvider, CXXMetadataProvider>();
            services.AddSingleton<INameTransformer, INameTransformer>();
            services.AddSingleton<ITypeTransformer, ITypeTransformer>();
        }
    }
}