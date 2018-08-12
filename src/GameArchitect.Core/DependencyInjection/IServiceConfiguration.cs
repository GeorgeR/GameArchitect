using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.DependencyInjection
{
    public interface IServiceConfiguration
    {
        void Setup(IServiceCollection services);
    }
}