using GameArchitect.Design.Metadata;
using Microsoft.Extensions.DependencyInjection;

namespace GameArchitect.Tasks.CodeGeneration
{
    /* Create and return a template most suited to the input metadata */
    public interface ITemplateFactory
    {
        void Setup(IServiceCollection services);

        ITemplate Create(ITypeInfo typeInfo);
    }
}