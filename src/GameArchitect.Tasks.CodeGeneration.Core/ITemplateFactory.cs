using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration
{
    /* Create and return a template most suited to the input metadata */
    public interface ITemplateFactory
    {
        ITemplate Create(TypeInfo typeInfo);
    }
}