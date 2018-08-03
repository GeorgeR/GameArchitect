using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration
{
    public interface ICodeGenerationConfiguration
    {
        void Setup(IServiceCollection services);
    }
}