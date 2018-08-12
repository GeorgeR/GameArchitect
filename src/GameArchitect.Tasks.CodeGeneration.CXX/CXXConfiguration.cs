using GameArchitect.Design.CXX.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration.CXX
{
    public class CXXConfiguration : ICodeGenerationConfiguration
    {
        public virtual void Setup(IServiceCollection services)
        {
            services.AddSingleton(this);

            services.AddSingleton<CXXMetadataProvider>();
            services.AddSingleton<CXXNameTransformer>();
            services.AddSingleton<CXXTypeTransformer>();
        }
    }
}