using GameArchitect.DependencyInjection;
using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration
{
    /* Create and return a template most suited to the input metadata */
    public interface ITemplateFactory<in TTypeInfo> : IServiceConfiguration
        where TTypeInfo : class, ITypeInfo
    {
        TTemplate Create<TTemplate>(TTypeInfo typeInfo) where TTemplate : ITemplate;
        ITemplate Create(TTypeInfo typeInfo);
    }
}