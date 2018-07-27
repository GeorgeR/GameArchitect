using GameArchitect.Design.Metadata;

namespace GameArchitect.Tasks.CodeGeneration
{
    public interface ITypeTransformer
    {
        string TransformType(IMemberInfo member);
        string TransformType(TypeInfo type);
    }
}