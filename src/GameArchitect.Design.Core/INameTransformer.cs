using GameArchitect.Design.Metadata;

namespace GameArchitect.Design
{
    public enum NameContext
    {
        Type,
        Namespace,
        Interface,
        Method,
        Property,
        Event,
        LocalVariable,
        LocalConstant,
        Parameter,
        Field,
        StaticField,
        ConstantField,
        Enum,
        EnumMember,
        LocalFunction,
        EverythingElse
    }

    public interface INameTransformer
    {
        string TransformName(IMetaInfo info, string name, NameContext context);
    }
}