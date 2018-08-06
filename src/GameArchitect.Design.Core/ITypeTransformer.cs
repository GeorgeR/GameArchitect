using GameArchitect.Design.Metadata;

namespace GameArchitect.Design
{
    public interface ITypeTransformer
    {
        string TransformType(IMemberInfo member);
        string TransformType(ITypeInfo type);
    }
}