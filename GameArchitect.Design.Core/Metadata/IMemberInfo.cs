using GameArchitect.Design.Support;

namespace GameArchitect.Design.Metadata
{
    public interface IMemberInfo : IMetaInfo
    {
        Mutability Mutability { get; }
        bool IsOptional { get; }
        CollectionType CollectionType { get; }

        TypeInfo Type { get; } // If this is null, the type is void
        TypeInfo DeclaringType { get; }
    }

    public interface IMemberInfo<out TNative> : IMemberInfo, IMetaInfo<TNative>
    {

    }
}